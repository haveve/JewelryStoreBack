using CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes;
using CourseWorkDB.Graphql.Mutation.User;
using CourseWorkDB.Graphql.Mutation.UserProductRelation;
using CourseWorkDB.Graphql.Query.UserProductRelation;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.History;
using CourseWorkDB.ViewModel.User;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.User
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserRepository userRepository, IUserProductRelation userProductRelation)
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<UserSelfDataGraphType>>>>("users")
                .Argument<NonNullGraphType<UserSortInputGraphType>>("sort")
                .ResolveAsync(async context =>
                {
                    var sortData = context.GetArgument<UserSort>("sort");
                    return await userRepository.GetUsers(sortData);
                });
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<HistoryGraphType>>>>("get_history")
                .Argument<NonNullGraphType<IntGraphType>>("userId")
                .Argument<NonNullGraphType<UserHistorySortInputGraphType>>("sort")
                .ResolveAsync(async context =>
                {
                    var sortData = context.GetArgument<UserHistorySort>("sort");
                    var userId = context.GetArgument<int>("userId");
                    return await userProductRelation.GetUserHistoryAsync(userId,sortData);
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
