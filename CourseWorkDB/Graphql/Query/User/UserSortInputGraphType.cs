using CourseWorkDB.ViewModel.User;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.User
{
    public class UserSortInputGraphType : InputObjectGraphType<UserSort>
    {
        public UserSortInputGraphType()
        {
            Field(el => el.TelephoneNumber,nullable:true);
            Field(el => el.FullName, nullable: true);
        }
    }
}
