using CourseWorkDB.Model.DetailsInfo.Material;
using CourseWorkDB.Model.DetailsInfo.Size;
using CourseWorkDB.Model.DetailsInfo.SpecificInfo;
using CourseWorkDB.Model.DetailsInfo.Stone;
using CourseWorkDB.Model.ProductInfo;
using CourseWorkDB.ViewModel.Discount;
using CourseWorkDB.ViewModel.Material;
using CourseWorkDB.ViewModel.Product;
using CourseWorkDB.ViewModel.Size;
using TimeTracker.Repositories;

namespace CourseWorkDB.Repositories
{
    public interface IProductRepository
    {
        Task<ProductPagination> GetProductAsync(ProductSort productSort);
        Task<DetailsProductInfo> AddProductAsync(AddProduct product);
        Task<DetailsProductInfo> UpdateProductAsync(DetailsProductInfo product);
        Task<ProductState> ChangeProductStateAsync(int productId, bool disabled);
        Task<int> RemoveProductAsync(int productId);
        Task<DetailsProductInfo> GetProductDefailsInfo(int productId);


        Task<Creator> AddCreatorAsync(string name);
        Task<Creator> UpdateCreatorAsync(Creator creator);
        Task<CreatorState> ChangeCreatorStateAsync(int creatorId, bool disabled);
        Task<IEnumerable<Creator>> GetCreatorsAsync();
        Task<int> RemoveCreatorAsync(int creatorId);



        Task<Category> AddCategoryAsync(string name);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<int> RemoveCategoryAsync(int categoryId);



        //SIZE

        Task<IEnumerable<Size>> GetSizesAsync();
        Task<Size> AddSizeAsync(string name);
        Task<Size> UpdateSizeAsync(Size size);
        Task<int> RemoveSizeAsync(int sizeId);



        //SIZE INFO

        Task<IEnumerable<SizeInfo>> GetSizeInfosAsync(int productId);
        Task<IEnumerable<AddSizeInfo>> AddSizeInfosAsync(IEnumerable<AddSizeInfo> sizeInfo);
        Task<AddSizeInfo> UpdateSizeInfoAsync(AddSizeInfo sizeInfo);
        Task<AddSizeInfo> RemoveSizeInfoAsync(AddSizeInfo size);


        //MATERIAL 


        Task<IEnumerable<Material>> GetMaterialsAsync();
        Task<Material> AddMaterialAsync(string name);
        Task<Material> UpdateMaterialAsync(Material material);
        Task<int> RemoveMaterialAsync(int materialId);


        //MATERIAL COLOR 

        Task<IEnumerable<MaterialColor>> GetMaterialsColorsAsync();
        Task<MaterialColor> AddMaterialColorAsync(string name);
        Task<MaterialColor> UpdateMaterialColorAsync(MaterialColor materialColor);
        Task<int> RemoveMaterialColorAsync(int materialColorId);


        //Material Info

        Task<IEnumerable<MaterialInfo>> GetMaterialInfosAsync(int productId);
        Task<IEnumerable<AddMaterialInfo>> AddMaterialInfosAsync(IEnumerable<AddMaterialInfo> materialInfos);
        Task<AddMaterialInfo> UpdateMaterialInfoAsync(AddMaterialInfo materialInfo);
        Task<AddMaterialInfo> RemoveMaterialInfoAsync(AddMaterialInfo materialInfo);


        //STONE COLOR

        Task<IEnumerable<StoneColor>> GetStoneColorsAsync();
        Task<StoneColor> AddStoneColorAsync(string name);
        Task<StoneColor> UpdateStoneColorAsync(StoneColor stoneColor);
        Task<int> RemoveStoneColorAsync(int stoneColorId);

        //STONE SHAPE

        Task<IEnumerable<StoneShape>> GetStoneShapesAsync();
        Task<StoneShape> AddStoneShapeAsync(string name);
        Task<StoneShape> UpdateStoneShapeAsync(StoneShape stoneShape);
        Task<int> RemoveStoneShapeAsync(int stoneShapeId);

        //STONE TYPE

        Task<IEnumerable<StoneType>> GetStoneTypesAsync();
        Task<StoneType> AddStoneTypeAsync(string name);
        Task<StoneType> UpdateStoneTypeAsync(StoneType stoneType);
        Task<int> RemoveStoneTypeAsync(int stoneTypeId);

        //Stone Info

        Task<IEnumerable<StoneInfo>> GetStoneInfosAsync(int productId);
        Task<IEnumerable<AddStoneInfo>> AddStoneInfosAsync(IEnumerable<AddStoneInfo> stoneInfos);
        Task<AddStoneInfo> UpdateStoneInfoAsync(AddStoneInfo stoneInfo);
        Task<AddStoneInfo> RemoveStoneInfoAsync(AddStoneInfo stoneInfoId);

        //Lock Type

        Task<IEnumerable<LockType>> GetLockTypesAsync();
        Task<LockType> AddLockTypeAsync(string name);   
        Task<LockType> UpdateLockTypeAsync(LockType lockType);
        Task<int> RemoveLockTypeAsync(int lockTypeId);


        //Shape Type
        
        Task<IEnumerable<ShapeType>> GetShapeTypesAsync();
        Task<ShapeType> AddShapeTypeAsync(string name);
        Task<ShapeType> UpdateShapeTypeAsync(ShapeType shapeType);
        Task<int> RemoveShapeTypeAsync(int shapeTypeId);


        //Specific Product Info

        Task<SpecifictProductInfo?> GetSpecificProductInfoAsync(int productId);
        Task<AddSpecificProductInfo> AddSpecificProductInfoAsync(AddSpecificProductInfo addSpecificProductInfo);
        Task<AddSpecificProductInfo> UpdateSpecificProductInfoAsync(AddSpecificProductInfo addSpecificProductInfo);
        Task<int> RemoveSpecificProductInfoAsync(int specificProductInfoId);
        Task<IEnumerable<SpecifictProductInfo>> GetSpecificProductInfosAsync();

        //Discount 

        Task<IEnumerable<Discount>> GetDiscountsAsync();
        Task<Discount> AddDiscountAsync(AddDiscount discount);
        Task<Discount> UpdateDiscountAsync(Discount discount);
        Task<int> RemoveDiscountAsync(int discountId);
    }
}
