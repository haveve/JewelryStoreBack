using CourseWorkDB.Graphql.Mutation.User;
using CourseWorkDB.Repositories;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.User
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserRepository userRepository)
        {
            Field<UserSelfDataGraphType>("users")
                .ResolveAsync(async context =>
                {
                    return await userRepository.GetUsers();
                });
        }
    }
}
