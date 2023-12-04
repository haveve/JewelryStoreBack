using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class AddSpecificProductInfoGraphType:ObjectGraphType<AddSpecificProductInfo>
    {
        public AddSpecificProductInfoGraphType()
        {
            Field(el => el.Id);
            Field(el => el.LockTypeId);
            Field(el => el.ShapeTypeId);
        }
    }
}
