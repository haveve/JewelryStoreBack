using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class UpdateSpecificProductInfoInputGraphType : InputObjectGraphType<AddSpecificProductInfo>
    {
        public UpdateSpecificProductInfoInputGraphType()
        {
            Field(el => el.LockTypeId);
            Field(el => el.ShapeTypeId);
            Field(el => el.Id);
        }
    }
}
