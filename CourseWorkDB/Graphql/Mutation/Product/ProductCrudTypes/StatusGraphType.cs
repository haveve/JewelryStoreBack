using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class StatusGraphType:ObjectGraphType<ProductState>
    {
        public StatusGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Disabled);
        }
    }
}
