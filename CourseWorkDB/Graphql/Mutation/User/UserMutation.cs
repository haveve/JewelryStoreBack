using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.User;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class UserMutation : ObjectGraphType
    {
        public UserMutation(IUserRepository userRepository)
        {
            Field<IntGraphType>("disable_user")
                .Argument<IntGraphType>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await userRepository.DisableUser(id);
                });
            Field<ChangePermissionGraphType>("change_user_permission")
                .Argument<ChangePermissionInputGraphType>("userPermission")
                .ResolveAsync(async context =>
                {
                    var data = context.GetArgument<ChangePermission>("userPermission");
                    return await userRepository.ChangePermission(data);
                });
        }
    }
}
