using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class ProductSearchInputGraphType:InputObjectGraphType<ProductSort>
    {
        public ProductSearchInputGraphType()
        {
            Field(el => el.StoneTypes, nullable:true);
            Field(el => el.StoneShapes, nullable: true);
            Field(el => el.StoneColors, nullable: true);
            Field(el => el.Material, nullable: true);
            Field(el => el.MaterialColors, nullable: true);
            Field(el => el.Sizes, nullable: true);
            Field(el => el.Search , nullable: true);
            Field(el => el.CategoryId , nullable: true);
            Field(el => el.Creators, nullable: true);
            Field(el => el.LockTypes, nullable: true);
            Field(el => el.ShapeTypes, nullable: true);
            Field(el => el.OnlyDiscount, nullable: true);
            Field(el => el.Pagination, type:typeof(PaginationSortGraphType));
            Field(el => el.SpecificData, nullable: true, type:typeof(SpecificDataInputGraphType));
            Field(el => el.IsCheaper, nullable: true);
        }
    }
}
