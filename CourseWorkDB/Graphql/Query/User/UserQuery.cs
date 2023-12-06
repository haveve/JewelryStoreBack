using CourseWorkDB.Graphql.Mutation.User;
using CourseWorkDB.Graphql.Mutation.UserProductRelation;
using CourseWorkDB.Graphql.Query.UserProductRelation;
using CourseWorkDB.Repositories;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.User
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserRepository userRepository, IUserProductRelation userProductRelation)
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<UserSelfDataGraphType>>>>("users")
                .ResolveAsync(async context =>
                {
                    return await userRepository.GetUsers();
                });
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<HistoryGraphType>>>>("get_history")
                .Argument<NonNullGraphType<IntGraphType>>("userId")
                .ResolveAsync(async context =>
                {
                    return await userProductRelation.GetUserHistoryAsync(context.GetArgument<int>("userId"));
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<SelectedProductGraphType>>>>("get_selected_products")
                .Argument<NonNullGraphType<IntGraphType>>("statusId")
                .Argument<NonNullGraphType<IntGraphType>>("userId")
                .ResolveAsync(async context =>
                {
                    return await userProductRelation.GetSelectedProductsAsync(context.GetArgument<int>("userId"),
                                                                        context.GetArgument<int>("statusId"));
                });
        }
    }
}
