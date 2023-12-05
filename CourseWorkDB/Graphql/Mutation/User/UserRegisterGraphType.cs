using CourseWorkDB.ViewModel.User;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class UserRegisterGraphType:InputObjectGraphType<RegisterUser>
    {
        public UserRegisterGraphType()
        {
            Field(el => el.TelephoneNumber);
            Field(el => el.FullName);
            Field(el => el.Password);
        }
    }
}
