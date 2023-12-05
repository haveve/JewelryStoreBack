using CourseWork_DB.Helpers;
using CourseWorkDB.Graphql.Mutation.User;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.User;
using GraphQL;
using GraphQL.Types;
using TimeTracker.Services;

namespace CourseWorkDB.Graphql.Query
{
    public class IdentityMutation : ObjectGraphType
    {
        public IdentityMutation(IUserRepository userRepository,IConfiguration configuration)
        {
            Field<NonNullGraphType<IntGraphType>>("register")
                .Argument<NonNullGraphType<UserRegisterGraphType>>("data")
                .ResolveAsync(async context =>
                {
                    var user = context.GetArgument<RegisterUser>("data");
                    var salt = PasswordHasher.GenerateSalt();
                    return (await userRepository.AddUser(new()
                    {
                        FullName = user.FullName,
                        TelephoneNumber = user.TelephoneNumber,
                        Password = user.Password.ComputeHash(salt, configuration.GetIteration()),
                        Salt = salt,
                        Permissions = new()
                        {
                            ProductManage = false,
                            UserManage = false,
                        }
                    })).Id;
                });
        }
    }
}
