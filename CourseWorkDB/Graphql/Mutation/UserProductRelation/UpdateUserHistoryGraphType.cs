using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.History;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class UpdateUserHistoryGraphType:ObjectGraphType<UpdateUserHistory>
    {
        public UpdateUserHistoryGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Address);
        }
    }
}
