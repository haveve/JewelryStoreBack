using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class AddSpecificProductInfoInputGraphType : InputObjectGraphType<AddSpecificProductInfo>
    {
        public AddSpecificProductInfoInputGraphType()
        {
            Field(el => el.LockTypeId);
            Field(el => el.ShapeTypeId);
        }
    }
}
