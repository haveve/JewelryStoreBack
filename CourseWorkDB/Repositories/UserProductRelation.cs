using CourseWorkDB.Model;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.History;
using Dapper;
using Microsoft.AspNetCore.Http;
using System.Data;
using TimeTracker.Repositories;

namespace CourseWorkDB
{
    public class UserProductRelation : IUserProductRelation
    {

        DapperContext _dapperContext;

        public UserProductRelation(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<SelectedProductsStatus>> GetSelectedProductsStatusAsync()
        {
            string query = "SELECT id,name FROM SelectedProductsStatus";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<SelectedProductsStatus>(query);
        }

        public async Task<SelectedProductsStatus> AddSelectedProductsStatusAsync(string name)
        {
            string query = "INSERT INTO SelectedProductsStatus (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM SelectedProductsStatus),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<SelectedProductsStatus> UpdateSelectedProductsStatusAsync(SelectedProductsStatus data)
        {
            string query = "UPDATE SelectedProductsStatus SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, data).ConfigureAwait(false);

            return data;
        }

        public async Task<IEnumerable<SelectedProduct>> GetSelectedProductsAsync(int userId, int statuId)
        {
            string query = @"SELECT sp.id,sp.count,sp.status_id,sp.user_id,sp.product_id,sp.size_id,
            IIF((sf.count - sp.count >= 0) or (sp.count = 0 and sp.count > 0),1,0) as present FROM SelectedProducts as sp
            LEFT JOIN SizeInfo AS sf ON sf.product_id = sp.product_id AND sf.size_id = sp.size_id
            where user_id = @userId AND status_id = @statuId";
                            
            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<SelectedProduct>(query, new {userId,statuId}).ConfigureAwait(false);
        }

        public async Task<Guid> RemoveSelectedProductAsync(Guid Id, int userId)
        {
            string query = $"DELETE FROM SelectedProducts OUTPUT deleted.id where id = @Id AND user_id = @userId AND status_id in ({(int)SelectedStatus.Beloved},{(int)SelectedStatus.InBucket})";
            using var connection = _dapperContext.CreateConnection();

            var data = Guid.Empty;
            try
            {
                data = await connection.QuerySingleAsync<Guid>(query, new { Id, userId }).ConfigureAwait(false);
            }
            catch
            {

            }

            return data; 
        }

        public async Task<SelectedProduct> AddSelectedProductAsync(SelectedProduct selectedProduct,bool minSize = false)
        {
            string size = minSize ? "SELECT MIN(size_id) From SizeInfo WHERE product_id = @ProductId":"@SizeId";

            string query = @$"
            INSERT INTO SelectedProducts(id,count,last_status_changed,status_id,user_id,product_id,size_id)
            VALUES(@Id,@Count,@now,@StatusId,@UserId,@ProductId,({size}))";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query,new
            {
                selectedProduct.Count,
                selectedProduct.Id,
                selectedProduct.ProductId,
                selectedProduct.SizeId,
                selectedProduct.UserId,
                selectedProduct.StatusId,
                now = DateTime.UtcNow
            } ).ConfigureAwait(false);

            return selectedProduct;
        }

        public async Task<SelectedProduct> UpdateProductAsync(SelectedProduct selectedProduct)
        {
            string query = @"
            UPDATE SET count = @Count,status_id = @StatusId WHERE id = @Id and product_id = @ProductId and size_id = @SizeId";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, selectedProduct).ConfigureAwait(false);

            return selectedProduct;
        }

        public async Task<int> RemoveSelectedProductsStatusAsync(int Id)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE SelectedProductsStatus OUTPUT deleted.id WHERE id = @Id";

            return await connection.QuerySingleAsync<int>(query, new { Id }).ConfigureAwait(false);

        }

        public async Task<IEnumerable<History>> GetUserHistoryAsync(int userId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = @"SELECT h.id,h.total_cost,h.address,h.date,sp.count,p.name,p.image,p.disabled,p.category_id FROM History as h
            JOIN SelectedProducts as sp
            ON sp.user_id = @userId AND h.id = sp.id
            JOIN Size as s
            ON sp.size_id = s.id
            JOIN Products as p
            ON sp.product_id = p.id
            ORDER BY h.date DESC";

            return await connection.QueryAsync<History>(query, new { userId }).ConfigureAwait(false);
        }

        public async Task<UpdateUserHistory> UpdateUserHistoryAsync(UpdateUserHistory data)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = @"UPDATE History SET address = @Address WHERE id = @Id";

            await connection.ExecuteAsync(query, data).ConfigureAwait(false);
            return data;
        }

        public async Task<string> DeclineOrderAsync(Guid orderId,int userId, bool userRollBack)
        {
            string procedure = "USP_RALLBACK_BOUGHT";
            using var connection = _dapperContext.CreateConnection();

            return await connection.QuerySingleAsync<string>(procedure, new
            {
                OrderId = orderId,
                UserRollBack = userRollBack,
                UserId = userId
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task<string> CreateOrderAsync(IEnumerable<Guid> ProductIds,string address)
        {
            string procedure = "USP_COMMIT_BOUGHT";
            using var connection = _dapperContext.CreateConnection();


            var dt = new DataTable();
            dt.Columns.Add("Id");

            foreach(var el in ProductIds)
            {
                dt.Rows.Add($"{el}");
            }

            return await connection.QuerySingleAsync<string>(procedure, new
            {
                OrderId = Guid.NewGuid(),
                OrderedStatusId = (int)SelectedStatus.Ordered,
                ProductIds = dt.AsTableValuedParameter("ProductStatusIds"),
                Address = address
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
