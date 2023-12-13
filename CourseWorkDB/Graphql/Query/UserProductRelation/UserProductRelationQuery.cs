using CourseWorkDB.Graphql.Query.User;
using CourseWorkDB.Graphql.Query.UserProductRelation;
using CourseWorkDB.Helpers;
using CourseWorkDB.Model;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.History;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class UserProductRelationQuery : ObjectGraphType
    {
        public UserProductRelationQuery(IUserProductRelation userProductRelation)
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<HistoryGraphType>>>>("get_history")
               .Argument<NonNullGraphType<UserHistorySortInputGraphType>>("sort")
                .ResolveAsync(async context =>
                {
                    var sortData = context.GetArgument<UserHistorySort>("sort");
                    return await userProductRelation.GetUserHistoryAsync(context.User!.GetUserId(), sortData);
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<SelectedProductGraphType>>>>("get_selected_products")
               .Argument<NonNullGraphType<IntGraphType>>("statusId")
               .ResolveAsync(async context =>
               {
                   return await userProductRelation.GetSelectedProductsAsync(context.User!.GetUserId(),
                                                                       context.GetArgument<int>("statusId"));
               });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<SelectedProductStatusGraphType>>>>("get_product_statuses")
               .ResolveAsync(async context =>
               {
                   return await userProductRelation.GetSelectedProductsStatusAsync();
               });
        }
    }
}
