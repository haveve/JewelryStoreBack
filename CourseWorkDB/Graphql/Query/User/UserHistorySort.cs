using CourseWorkDB.ViewModel.History;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.User
{
    public class UserHistorySortInputGraphType : InputObjectGraphType<UserHistorySort>
    {
        public UserHistorySortInputGraphType()
        {
            Field(el => el.OrderDate, nullable: true);
        }
    }
}
