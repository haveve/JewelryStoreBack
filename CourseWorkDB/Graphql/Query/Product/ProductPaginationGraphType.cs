using CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes;
using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class ProductPaginationGraphType:ObjectGraphType<ProductPagination>
    {
        public ProductPaginationGraphType()
        {
            Field(el => el.Count);
            Field(el => el.Products,type: typeof(ListGraphType<ProductGraphType>));
        }
    }
}
