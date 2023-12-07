using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.History;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class UpdateUserHistoryInputGraphType : InputObjectGraphType<UpdateUserHistory>
    {
        public UpdateUserHistoryInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Address);
        }
    }
}
