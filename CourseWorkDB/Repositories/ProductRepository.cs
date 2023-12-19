using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.Discount;
using CourseWorkDB.ViewModel.Material;
using CourseWorkDB.ViewModel.Product;
using CourseWorkDB.ViewModel.Size;
using Dapper;
using System.Numerics;
using System.Text;
using TimeTracker.Repositories;
using TimeTracker.Services;

namespace CourseWorkDB.Repositories
{
    public class ProductRepository : IProductRepository
    {
        DapperContext _dapperContext;

        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }


        public async Task<DetailsProductInfo> AddProductAsync(AddProduct product)
        {
            string query = "INSERT INTO Products(name,description,category_id,creator_id,image,specific_product_info_id) OUTPUT inserted.id VALUES(@Name,@Description,@CategoryId,@CreatorId,@Image,@SpecificProductInfoId)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, product).ConfigureAwait(false);

            return new DetailsProductInfo(product, id);
        }

        public async Task<ProductState> ChangeProductStateAsync(int productId, bool disabled)
        {
            string query = "UPDATE Products SET disabled = @disabled Where id = @productId";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { productId, disabled }).ConfigureAwait(false);

            return new() { Id = productId, Disabled = disabled };
        }

        public async Task<DetailsProductInfo> UpdateProductAsync(DetailsProductInfo product)
        {
            string query = "UPDATE Products SET name = @Name, description = @Description, image = @Image, specific_product_info_id = @SpecificProductInfoId OUTPUT deleted.image  Where id = @Id";
            using var connection = _dapperContext.CreateConnection();
            product.Image = await connection.QuerySingleAsync<string>(query, product).ConfigureAwait(false);

            return product;
        }

        public async Task<DetailsProductInfo> GetProductDefailsInfo(int productId)
        {
            string query = @" DECLARE @DateNow DATE = GETDATE() SELECT p.id,p.image, p.name,p.creator_id,p.category_id,p.description,p.specific_product_info_id, d.[percent] as discount FROM Products as p 
            LEFT JOIN Discount  as d
            ON p.id = d.product_id AND start <= @DateNow AND @DateNow <= [end]
			WHERE p.id = @productId";

            using var connection = _dapperContext.CreateConnection();

            return await connection.QuerySingleAsync<DetailsProductInfo>(query,new {productId }).ConfigureAwait(false);
        }

        public async Task<ProductPagination> GetProductAsync(ProductSort productSort)
        {
            string Left = productSort.OnlyDiscount ? string.Empty : "LEFT";
            string LeftSize = productSort.Sizes is null ? "Left" : string.Empty;
            
            string selectForPagination = @"SELECT p.id,p.image, p.name,p.category_id, Min(COALESCE(s.cost,0)) as MinCost,d.[percent] as discount_percent";

            string from = " FROM Products as p ";

            string count = " ,null as Count, null as Minimum, null as Maximum ";

            string selectForCount = @"
            UNION ALL
            SELECT null,null,null,null,null,null, COUNT(DISTINCT p.image) as Count, Min(s.cost) as Minimum, Max(s.cost) as Maximum FROM Products as p";

            StringBuilder query = new StringBuilder(@$"
            {Left} JOIN Discount  as d
            ON p.id = d.product_id AND start <= @DateNow AND @DateNow <= [end]
            {LeftSize} JOIN SizeInfo as s ON p.id = s.product_id");

            StringBuilder forParams = new StringBuilder();
            char trimSymbol = ',';

            if (productSort.Sizes is not null)
            {
                foreach (var size in productSort.Sizes)
                {
                    forParams.AppendFormat("{0},", size);
                }

                query.AppendFormat(@" AND s.size_id in ({0}) ",
                            forParams.ToString().TrimEnd(trimSymbol));
                forParams.Clear();
            }

            if (productSort.MaterialColors is not null
                || productSort.Material is not null)
            {

                query.Append(" JOIN MaterialInfo as m ON p.id = m.product_id AND");

                if (productSort.Material is not null)
                {
                    foreach (var material in productSort.Material)
                    {
                        forParams.AppendFormat("{0},", material);
                    }

                    query.AppendFormat(@" m.material_id in ({0}) {1}"
                    , forParams.ToString().TrimEnd(trimSymbol)
                    , productSort.MaterialColors is not null ? "AND " : string.Empty);

                    forParams.Clear();
                }


                if (productSort.MaterialColors is not null)
                {
                    foreach (var color in productSort.MaterialColors)
                    {
                        forParams.AppendFormat("{0},", color);
                    }

                    query.AppendFormat(@" m.material_color_id in ({0})"
                    , forParams.ToString().TrimEnd(trimSymbol));
                    forParams.Clear();
                }

            }

            if (productSort.StoneShapes is not null
            || productSort.StoneColors is not null
            || productSort.StoneTypes is not null)
            {
                query.Append(@" JOIN StoneInfo as si
                            ON p.id = si.product_id AND 
                         ");

                if (productSort.StoneTypes is not null)
                {
                    foreach (var stoneType in productSort.StoneTypes)
                    {
                        forParams.AppendFormat("{0},", stoneType);
                    }

                    query.AppendFormat(@" si.stone_type_id in ({0}) {1}"
                    , forParams.ToString().TrimEnd(trimSymbol)
                    , productSort.StoneShapes is not null || productSort.StoneColors is not null ? "AND " : string.Empty);

                    forParams.Clear();
                }

                if (productSort.StoneShapes is not null)
                {
                    foreach (var stoneShape in productSort.StoneShapes)
                    {
                        forParams.AppendFormat("{0},", stoneShape);
                    }

                    query.AppendFormat(@" si.stone_type_id in ({0}) {1}"
                    , forParams.ToString().TrimEnd(trimSymbol)
                    , productSort.StoneColors is not null ? "AND " : string.Empty);

                    forParams.Clear();
                }


                if (productSort.StoneColors is not null)
                {
                    foreach (var stoneColor in productSort.StoneColors)
                    {
                        forParams.AppendFormat("{0},", stoneColor);
                    }

                    query.AppendFormat(@" si.stone_type_id in ({0}) "
                    , forParams.ToString().TrimEnd(trimSymbol));
                    forParams.Clear();
                }
            }


            if (productSort.LockTypes is not null
               || productSort.ShapeTypes is not null)
            {
                query.Append(" JOIN SpecificProductInfo as spi ON p.specific_product_info_id = spi.id AND  ");

                if (productSort.LockTypes is not null)
                {
                    foreach (var lockType in productSort.LockTypes)
                    {
                        forParams.AppendFormat("{0},", lockType);
                    }

                    query.AppendFormat(@" spi.lock_type_id in ({0}) {1}"
                    , forParams.ToString().TrimEnd(trimSymbol)
                    , productSort.ShapeTypes is not null ? "AND " : string.Empty);

                    forParams.Clear();
                }

                if (productSort.ShapeTypes is not null)
                {
                    foreach (var shapeType in productSort.ShapeTypes)
                    {
                        forParams.AppendFormat("{0},", shapeType);
                    }

                    query.AppendFormat(@" spi.shape_type_id in ({0}) "
                    , forParams.ToString().TrimEnd(trimSymbol));

                    forParams.Clear();
                }

            }

            query.Append(" WHERE p.disabled is distinct from 1");

            if (productSort.CategoryId is not null)
            {
                query.AppendFormat(" AND category_id = {0} ", productSort.CategoryId);
            }

            if (productSort.Creators is not null)
            {
                foreach (var creator in productSort.Creators)
                {
                    forParams.AppendFormat("{0},", creator);
                }

                query.AppendFormat(" AND creator_id in ({0}) ",
                            forParams.ToString().TrimEnd(trimSymbol));
                forParams.Clear();
            }

            if (productSort.Search is not null)
            {
                query.AppendFormat(" AND (CONTAINS(p.name,'{0}') OR CONTAINS(p.description,'{0}') )", productSort.Search);
            }

            if (productSort.SpecificData is not null)
            {
                query.AppendFormat(" AND s.cost between {0} and {1} ", productSort.SpecificData.Minimum,
                    productSort.SpecificData.Maximum);
            }

            string queryForCount = selectForCount + query.ToString();

            string queryPagination = string.Empty;

            if (productSort.IsCheaper is not null)
            {
                query.AppendFormat(" GROUP By p.id,p.image,p.name,p.category_id,d.[percent] ORDER BY minCost {0} ", (bool)productSort.IsCheaper ? "ASC" : "DSC");
            }
            else
            {
                query.AppendFormat(" GROUP By p.id,p.image,p.name,p.category_id,d.[percent] ORDER BY minCost ASC ");
            }

            string pagination = string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", productSort.Pagination.Skip, productSort.Pagination.Take);

            queryPagination = string.Format(" DECLARE @DateNow DATE = GETDATE() SELECT id,image,category_id,name,MinCost, discount_percent {1} FROM ({0} {2}) as p ", 
                selectForPagination + from + query.ToString(), count,pagination);


            using var connection = _dapperContext.CreateConnection();

            ProductPagination result = new ProductPagination();

            string queryStr = queryPagination + queryForCount;

            result.Products = (await connection.QueryAsync<Product?, SpecificData?, Product?>(queryStr, (p, specInfo) =>
            {
                if (specInfo is not null)
                {
                    result.SpecificData = specInfo;
                }

                return p;
            }, splitOn: "Count").ConfigureAwait(false)).SkipLast(1)!;

            return result;
        }

        //CREATORS


        public async Task<Creator> AddCreatorAsync(string name)
        {
            string query = "INSERT INTO ProductCreator (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM ProductCreator),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<Creator> UpdateCreatorAsync(Creator creator)
        {
            string query = "UPDATE ProductCreator SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, creator).ConfigureAwait(false);

            return creator;
        }

        public async Task<CreatorState> ChangeCreatorStateAsync(int creatorId, bool disabled)
        {
            string query = "UPDATE ProductCreator SET disabled = @disabled Where id = @creatorId";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { creatorId, disabled }).ConfigureAwait(false);

            return new() { Id = creatorId, Disabled = disabled };
        }

        public async Task<IEnumerable<Creator>> GetCreatorsAsync()
        {
            string query = "SELECT id,name FROM ProductCreator WHERE disabled is distinct from 1";
            using var connection = _dapperContext.CreateConnection();

            return await connection.QueryAsync<Creator>(query).ConfigureAwait(false);
        }



        //CATEGORIES



        public async Task<Category> AddCategoryAsync(string name)
        {
            string query = "INSERT INTO ProductCategory (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM ProductCategory),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            string query = "UPDATE ProductCategory SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, category).ConfigureAwait(false);

            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            string query = "SELECT id,name FROM ProductCategory";
            using var connection = _dapperContext.CreateConnection();

            return await connection.QueryAsync<Category>(query).ConfigureAwait(false);
        }

        //SIZE

        public async Task<IEnumerable<Size>> GetSizesAsync()
        {
            string query = "SELECT id,name FROM Size";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<Size>(query);
        }

        public async Task<Size> AddSizeAsync(string name)
        {
            string query = "INSERT INTO Size (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM Size),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<Size> UpdateSizeAsync(Size size)
        {
            string query = "UPDATE Size SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, size).ConfigureAwait(false);

            return size;
        }


        //SIZE INFO

        public async Task<IEnumerable<SizeInfo>> GetSizeInfosAsync(int productId)
        {
            string query = @"SELECT sf.cost,sf.count,s.name,s.id FROM SizeInfo as sf
                             JOIN Size as s
                             ON sf.product_id = @productId AND sf.size_id = s.id";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<SizeInfo, Size, SizeInfo>(query, (sizeInf, size) =>
            {
                sizeInf.Size = size;
                return sizeInf;
            }, new { productId }, splitOn: "name");
        }

        public async Task<IEnumerable<AddSizeInfo>> AddSizeInfosAsync(IEnumerable<AddSizeInfo> sizeInfo)
        {
            using var connection = _dapperContext.CreateConnection();
            await connection.BulkInsertAsync<AddSizeInfo>(sizeInfo, (sizeF) =>
            {
                return $"({sizeF.ProductId},{sizeF.SizeId},{sizeF.Cost},{sizeF.Count},{sizeF.WeightGram})";
            }, "(product_id,size_id,cost,count,weight_gram)", "SizeInfo").ConfigureAwait(false);

            return sizeInfo;
        }

        public async Task<AddSizeInfo> UpdateSizeInfoAsync(AddSizeInfo sizeInfo)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "UPDATE SizeInfo SET cost = @Cost, count = @Count WHERE product_id = @ProductId AND size_id = @SizeId";

            await connection.ExecuteAsync(query, sizeInfo).ConfigureAwait(false);
            return sizeInfo;
        }

        //MATERIAL 


        public async Task<IEnumerable<Material>> GetMaterialsAsync()
        {
            string query = "SELECT id,name FROM Material";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<Material>(query);
        }

        public async Task<Material> AddMaterialAsync(string name)
        {
            string query = "INSERT INTO Material (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM Material),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<Material> UpdateMaterialAsync(Material material)
        {
            string query = "UPDATE Material SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, material).ConfigureAwait(false);

            return material;
        }


        //MATERIAL COLOR 


        public async Task<IEnumerable<MaterialColor>> GetMaterialsColorsAsync()
        {
            string query = "SELECT id,name FROM MaterialColor";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<MaterialColor>(query);
        }

        public async Task<MaterialColor> AddMaterialColorAsync(string name)
        {
            string query = "INSERT INTO MaterialColor (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT Max(id) FROM MaterialColor),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<MaterialColor> UpdateMaterialColorAsync(MaterialColor materialColor)
        {
            string query = "UPDATE MaterialColor SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, materialColor).ConfigureAwait(false);

            return materialColor;
        }

        //Material Info

        public async Task<IEnumerable<MaterialInfo>> GetMaterialInfosAsync(int productId)
        {
            string query = @"SELECT mf.[percent],m.id, m.name,mc.id,mc.name FROM MaterialInfo as mf
                            JOIN Material as m 
                            ON mf.material_id = m.id
                            JOIN MaterialColor as mc
                            ON mf.material_color_id = mc.id
                            WHERE mf.product_id = @productId";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<MaterialInfo, Material, MaterialColor, MaterialInfo>(query, (mInf, m, mColor) =>
            {
                mInf.Material = m;
                mInf.MaterialColor = mColor;
                return mInf;
            }, new { productId }, splitOn: "id");
        }

        public async Task<IEnumerable<AddMaterialInfo>> AddMaterialInfosAsync(IEnumerable<AddMaterialInfo> materialInfos)
        {
            using var connection = _dapperContext.CreateConnection();
            await connection.BulkInsertAsync<AddMaterialInfo>(materialInfos, (sizeF) =>
            {
                return $"({sizeF.ProductId},{sizeF.MaterialId},{sizeF.MaterialColorId},{sizeF.Percent})";
            }, "(product_id,material_id,material_color_id,[percent])", "MaterialInfo").ConfigureAwait(false);

            return materialInfos;
        }

        public async Task<AddMaterialInfo> UpdateMaterialInfoAsync(AddMaterialInfo materialInfo)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "UPDATE MaterialInfo SET [percent] = @Percent WHERE product_id = @ProductId AND material_id = @MaterialId AND material_color_id = @MaterialColorId";

            await connection.ExecuteAsync(query, materialInfo).ConfigureAwait(false);
            return materialInfo;
        }


        public async Task<int> RemoveProductAsync(int productId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE Products OUTPUT deleted.id WHERE id = @productId";

            return await connection.QuerySingleAsync<int>(query, new { productId }).ConfigureAwait(false);
        }

        public async Task<int> RemoveCreatorAsync(int creatorId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE Creator OUTPUT deleted.id WHERE id = @creatorId";

            return await connection.QuerySingleAsync<int>(query, new { creatorId }).ConfigureAwait(false);
        }

        public async Task<int> RemoveCategoryAsync(int categoryId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE Category OUTPUT deleted.id WHERE id = @categoryId";

            return await connection.QuerySingleAsync<int>(query, new { categoryId }).ConfigureAwait(false);
        }

        public async Task<int> RemoveSizeAsync(int sizeId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE Size OUTPUT deleted.id WHERE id = @sizeId";

            return await connection.QuerySingleAsync<int>(query, new { sizeId }).ConfigureAwait(false);
        }

        public async Task<AddSizeInfo> RemoveSizeInfoAsync(AddSizeInfo size)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE SizeInfo WHERE product_id = @ProductId AND size_id = @SizeId";

            await connection.ExecuteAsync(query, size).ConfigureAwait(false);
            return size;
        }

        public async Task<int> RemoveMaterialAsync(int materialId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE Material OUTPUT deleted.id WHERE id = @materialId";

            return await connection.QuerySingleAsync<int>(query, new { materialId }).ConfigureAwait(false);
        }

        public async Task<int> RemoveMaterialColorAsync(int materialColorId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE MaterialColor OUTPUT deleted.id WHERE id = @materialColorId";

            return await connection.QuerySingleAsync<int>(query, new { materialColorId }).ConfigureAwait(false);
        }

        public async Task<AddMaterialInfo> RemoveMaterialInfoAsync(AddMaterialInfo materialInfo)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE MaterialInfo WHERE product_id = @ProductId AND material_id = @MaterialId AND material_color_id = @MaterialColorId";

            await connection.ExecuteAsync(query, materialInfo).ConfigureAwait(false);
            return materialInfo;
        }


        //STONE COLOR

        public async Task<IEnumerable<StoneColor>> GetStoneColorsAsync()
        {
            string query = "SELECT id,name FROM StoneColor";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<StoneColor>(query);
        }

        public async Task<StoneColor> AddStoneColorAsync(string name)
        {
            string query = "INSERT INTO StoneColor (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM StoneColor),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<StoneColor> UpdateStoneColorAsync(StoneColor stoneColor)
        {
            string query = "UPDATE StoneColor SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, stoneColor).ConfigureAwait(false);

            return stoneColor;
        }

        public async Task<int> RemoveStoneColorAsync(int stoneColorId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE StoneColor OUTPUT deleted.id WHERE id = @stoneColorId";

            return await connection.QuerySingleAsync<int>(query, new { stoneColorId }).ConfigureAwait(false);
        }


        //STONE SHAPE

        public async Task<IEnumerable<StoneShape>> GetStoneShapesAsync()
        {
            string query = "SELECT id,name FROM StoneShape";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<StoneShape>(query);
        }

        public async Task<StoneShape> AddStoneShapeAsync(string name)
        {
            string query = "INSERT INTO StoneShape (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM StoneShape),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<StoneShape> UpdateStoneShapeAsync(StoneShape stoneShape)
        {
            string query = "UPDATE StoneShape SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, stoneShape).ConfigureAwait(false);

            return stoneShape;
        }

        public async Task<int> RemoveStoneShapeAsync(int stoneShapeId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE StoneShape OUTPUT deleted.id WHERE id = @stoneShapeId";

            return await connection.QuerySingleAsync<int>(query, new { stoneShapeId }).ConfigureAwait(false);
        }

        //STONE TYPE

        public async Task<IEnumerable<StoneType>> GetStoneTypesAsync()
        {
            string query = "SELECT id,name FROM StoneType";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<StoneType>(query);
        }

        public async Task<StoneType> AddStoneTypeAsync(string name)
        {
            string query = "INSERT INTO StoneType (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM StoneType),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<StoneType> UpdateStoneTypeAsync(StoneType stoneType)
        {
            string query = "UPDATE StoneType SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, stoneType).ConfigureAwait(false);

            return stoneType;
        }

        public async Task<int> RemoveStoneTypeAsync(int stoneTypeId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE StoneType OUTPUT deleted.id WHERE id = @stoneTypeId";

            return await connection.QuerySingleAsync<int>(query, new { stoneTypeId }).ConfigureAwait(false);
        }

        //Stone Info

        public async Task<IEnumerable<StoneInfo>> GetStoneInfosAsync(int productId)
        {
            string query = @"select si.weight_carat,si.count,st.id,st.name ,ss.id,ss.name,sc.id,sc.name from StoneInfo as si
                            JOIN StoneType as st
                            ON si.stone_type_id = st.id
                            JOIN StoneShape as ss
                            ON si.stone_shape_id = ss.id
                            JOIN StoneColor as sc
                            ON si.stone_color_id = sc.id
                            WHERE si.product_id = @productId";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<StoneInfo, StoneType, StoneShape, StoneColor, StoneInfo>(query, (sInfo, sType, sShape, sColor) =>
            {
                sInfo.Type = sType;
                sInfo.Shape = sShape;
                sInfo.Color = sColor;
                return sInfo;
            }, new { productId });
        }

        public async Task<IEnumerable<AddStoneInfo>> AddStoneInfosAsync(IEnumerable<AddStoneInfo> stoneInfos)
        {
            using var connection = _dapperContext.CreateConnection();
            await connection.BulkInsertAsync<AddStoneInfo>(stoneInfos, (stoneI) =>
            {
                return $"({stoneI.ProductId},{stoneI.StoneColorId},{stoneI.StoneShapeId},{stoneI.StoneTypeId},{stoneI.WeightCarat},{stoneI.Count})";
            }, "(product_id,stone_color_id,stone_shape_id,stone_type_id,weight_carat,[count])", "StoneInfo").ConfigureAwait(false);

            return stoneInfos;
        }

        public async Task<AddStoneInfo> UpdateStoneInfoAsync(AddStoneInfo stoneInfo)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "UPDATE StoneInfo SET weight_carat = @WeighCarat,count = @Count WHERE product_id = @ProductId AND stone_type_id = @StoneTypeId AND stone_shape_id = @StoneShapeId AND stone_color_id = @StoneColorId";
            await connection.ExecuteAsync(query, stoneInfo).ConfigureAwait(false);
            return stoneInfo;
        }

        public async Task<AddStoneInfo> RemoveStoneInfoAsync(AddStoneInfo stoneInfoId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE StoneInfo WHERE product_id = @ProductId AND stone_type_id = @StoneTypeId AND stone_shape_id = @StoneShapeId AND stone_color_id = @StoneColorId";

            await connection.ExecuteAsync(query, new { stoneInfoId }).ConfigureAwait(false);
            return stoneInfoId;
        }

        //Lock Type

        public async Task<IEnumerable<LockType>> GetLockTypesAsync()
        {
            string query = "SELECT id,name FROM LockType";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<LockType>(query);
        }

        public async Task<LockType> AddLockTypeAsync(string name)
        {
            string query = "INSERT INTO LockType (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM LockType),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<LockType> UpdateLockTypeAsync(LockType lockType)
        {
            string query = "UPDATE LockType SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, lockType).ConfigureAwait(false);

            return lockType;
        }

        public async Task<int> RemoveLockTypeAsync(int lockTypeId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE LockType OUTPUT deleted.id WHERE id = @lockTypeId";

            return await connection.QuerySingleAsync<int>(query, new { lockTypeId }).ConfigureAwait(false);

        }


        //Shape Type

        public async Task<IEnumerable<ShapeType>> GetShapeTypesAsync()
        {
            string query = "SELECT id,name FROM ShapeType";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<ShapeType>(query);
        }

        public async Task<ShapeType> AddShapeTypeAsync(string name)
        {
            string query = "INSERT INTO ShapeType (id,name) OUTPUT inserted.id VALUES(COALESCE((SELECT  Max(id) FROM ShapeType),0) + 1,@name)";
            using var connection = _dapperContext.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, new { name }).ConfigureAwait(false);

            return new() { Name = name, Id = id };
        }

        public async Task<ShapeType> UpdateShapeTypeAsync(ShapeType shapeType)
        {
            string query = "UPDATE ShapeType SET name = @Name WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, shapeType).ConfigureAwait(false);

            return shapeType;
        }

        public async Task<int> RemoveShapeTypeAsync(int shapeTypeId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE ShapeType OUTPUT deleted.id WHERE id = @shapeTypeId";

            return await connection.QuerySingleAsync<int>(query, new { shapeTypeId }).ConfigureAwait(false);

        }


        //Specific Product Info

        public async Task<IEnumerable<SpecifictProductInfo>> GetSpecificProductInfosAsync()
        {
            string query = @"SELECT spi.id,lt.id,lt.name,st.id,st.name FROM SpecificProductInfo as spi
                            JOIN LockType as lt
                            ON spi.lock_type_id = lt.id
                            JOIN ShapeType as st
                            ON spi.shape_type_id = st.id";

            using var connection = _dapperContext.CreateConnection();
            return (await connection.QueryAsync<SpecifictProductInfo, LockType, ShapeType, SpecifictProductInfo>(query, (spi, lt, st) =>
            {
                spi.Shape = st;
                spi.Lock = lt;
                return spi;
            }));
        }

        public async Task<SpecifictProductInfo?> GetSpecificProductInfoAsync(int productId)
        {
            string query = @"SELECT null as empty,lt.id,lt.name,st.id,st.name FROM Products as p
                            JOIN SpecificProductInfo as spi
                            ON p.id = @productId AND p.specific_product_info_id = spi.id
                            JOIN LockType as lt
                            ON spi.lock_type_id = lt.id
                            JOIN ShapeType as st
                            ON spi.shape_type_id = st.id";

            using var connection = _dapperContext.CreateConnection();
            return (await connection.QueryAsync<SpecifictProductInfo, LockType, ShapeType, SpecifictProductInfo>(query, (spi, lt, st) =>
            {
                spi.Shape = st;
                spi.Lock = lt;
                return spi;
            }, new { productId })).SingleOrDefault();
        }

        public async Task<AddSpecificProductInfo> AddSpecificProductInfoAsync(AddSpecificProductInfo addSpecificProductInfo)
        {
            string query = "INSERT INTO SpecificProductInfo (shape_type_id,lock_type_id) OUTPUT inserted.id VALUES(@ShapeTypeId,@LockTypeId)";
            using var connection = _dapperContext.CreateConnection();
            addSpecificProductInfo.Id = await connection.QuerySingleAsync<int>(query, addSpecificProductInfo).ConfigureAwait(false);

            return addSpecificProductInfo;
        }

        public async Task<AddSpecificProductInfo> UpdateSpecificProductInfoAsync(AddSpecificProductInfo addSpecificProductInfo)
        {
            string query = "UPDATE SpecificProductInfo SET shape_type_id = @ShapeTypeId, lock_type_id = @LockTypeId WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, addSpecificProductInfo).ConfigureAwait(false);

            return addSpecificProductInfo;
        }

        public async Task<int> RemoveSpecificProductInfoAsync(int specificProductInfoId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE SpecificProductInfo OUTPUT deleted.id WHERE id = @specificProductInfoId";

            return await connection.QuerySingleAsync<int>(query, new { specificProductInfoId }).ConfigureAwait(false);

        }

        //Discount

        public async Task<IEnumerable<Discount>> GetDiscountsAsync()
        {
            string query = @"select start,[end],product_id,[percent],id from discount";

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<Discount>(query);
        }

        public async Task<Discount> AddDiscountAsync(AddDiscount discount)
        {
            string query = "INSERT INTO Discount (start,[end],product_id,[percent]) OUTPUT inserted.id VALUES(@Start,@End,@ProductId,@Percent)";
            using var connection = _dapperContext.CreateConnection();

            var id = await connection.QuerySingleAsync<int>(query, discount).ConfigureAwait(false);

            return new Discount(discount.Start, discount.End, discount.ProductId, discount.Percent, id);
        }

        public async Task<Discount> UpdateDiscountAsync(Discount discount)
        {
            string query = "UPDATE Discount SET start = @Start,[end] = @End,product_id = @ProductId,[percent] = @Percent WHERE id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, discount).ConfigureAwait(false);

            return discount;
        }

        public async Task<int> RemoveDiscountAsync(int discountId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = "DELETE Discount OUTPUT deleted.id WHERE id = @discountId";

            return await connection.QuerySingleAsync<int>(query, new { discountId }).ConfigureAwait(false);

        }
    }
}
