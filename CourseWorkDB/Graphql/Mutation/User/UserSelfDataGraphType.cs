using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.User;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class UserSelfDataGraphType : ObjectGraphType<UserSelfData>
    {
        public UserSelfDataGraphType()
        {
            Field(el => el.Id);
            Field(el => el.TelephoneNumber);
            Field(el => el.FullName);
        }
    }
}
