using CourseWorkDB.ViewModel.User;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class UserInputLoginGraphType : InputObjectGraphType<LoginUser>
    {
        public UserInputLoginGraphType()
        {
            Field(el => el.TelephoneNumber);
            Field(el => el.Password);
        }
    }
}
