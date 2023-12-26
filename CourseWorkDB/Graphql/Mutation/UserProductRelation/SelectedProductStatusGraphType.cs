using CourseWorkDB.Model.UserProduct;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class SelectedProductStatusGraphType : ObjectGraphType<SelectedProductsStatus>
    {
        public SelectedProductStatusGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
