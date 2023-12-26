using CourseWorkDB.Model.UserProduct;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class SelectedProductStatusInputGraphType : InputObjectGraphType<SelectedProductsStatus>
    {
        public SelectedProductStatusInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
