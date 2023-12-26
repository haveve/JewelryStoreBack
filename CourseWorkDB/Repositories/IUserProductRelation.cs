using CourseWorkDB.Model.UserProduct;
using CourseWorkDB.ViewModel.History;
using System.Data;
using TimeTracker.Repositories;

namespace CourseWorkDB.Repositories
{
    public interface IUserProductRelation
    {
        Task<IEnumerable<SelectedProductsStatus>> GetSelectedProductsStatusAsync();
        Task<SelectedProductsStatus> AddSelectedProductsStatusAsync(string name);
        Task<SelectedProductsStatus> UpdateSelectedProductsStatusAsync(SelectedProductsStatus data);
        Task<int> RemoveSelectedProductsStatusAsync(int Id);

        Task<IEnumerable<SelectedProduct>> GetSelectedProductsAsync(int userId, int statuId);
        Task<SelectedProduct> AddSelectedProductAsync(SelectedProduct selectedProduct, bool minSize = false);
        Task<SelectedProduct> UpdateProductAsync(SelectedProduct selectedProduct);
        Task<Guid> RemoveSelectedProductAsync(Guid Id,int userId);

        Task<IEnumerable<History>> GetUserHistoryAsync(int userId, UserHistorySort userHistorySort);
        Task<UpdateUserHistory> UpdateUserHistoryAsync(UpdateUserHistory data);

        Task<string> DeclineOrderAsync(Guid orderId, int userId, bool userRollBack);
        Task<string> CreateOrderAsync(IEnumerable<Guid> ProductIds,string address);
    }
}
