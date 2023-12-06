using CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo;
using CourseWorkDB.Helpers;
using CourseWorkDB.Model;
using CourseWorkDB.Repositories;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class UserProductRelationMutation:ObjectGraphType
    {
        public UserProductRelationMutation(IUserProductRelation userProductRelation)
        {
            Field<NonNullGraphType<SelectedProductGraphType>>("add_beloved")
                .Argument<NonNullGraphType<IntGraphType>>("productId")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("productId");

                    return await userProductRelation.AddSelectedProductAsync(new()
                    {
                        Count = 0,
                        StatusId = (int)SelectedStatus.Beloved,
                        Id = Guid.NewGuid(),
                        ProductId = id,
                        UserId = context.User!.GetUserId()
                    },true); 
                });

             Field<NonNullGraphType<SelectedProductGraphType>>("add_in_bucket")
                .Argument< NonNullGraphType<AddSelectedProductInputGraphType>> ("product")
                .ResolveAsync(async context =>
                {
                    var product = context.GetArgument<SelectedProduct>("product");
                    product.Id = Guid.NewGuid();
                    product.UserId = context.User!.GetUserId();
                    product.StatusId = (int)SelectedStatus.InBucket;

                    return await userProductRelation.AddSelectedProductAsync(product); 
                });

             Field<NonNullGraphType<SelectedProductGraphType>>("update_selected_product")
                .Argument<NonNullGraphType<UpdateSelectedProductInputGraphType>>("product")
                .ResolveAsync(async context =>
                {
                    var product = context.GetArgument<SelectedProduct>("product");
                    product.UserId = context.User!.GetUserId();
                    product.StatusId = (int)SelectedStatus.InBucket;

                    return await userProductRelation.UpdateProductAsync(product); 
                });

            Field<NonNullGraphType<StringGraphType>>("create_order")
               .Argument<NonNullGraphType<ListGraphType<NonNullGraphType<IntGraphType>>>>("Ids")
               .ResolveAsync(async context =>
               {
                   var Ids = context.GetArgument<IEnumerable<int>>("Ids");
                   return await userProductRelation.CreateOrderAsync(Ids);
               });

            Field<NonNullGraphType<StringGraphType>>("decline_order")
               .Argument<NonNullGraphType<GuidGraphType>>("id")
               .ResolveAsync(async context =>
               {
                   var id = context.GetArgument<Guid>("id");
                   return await userProductRelation.DeclineOrderAsync(id,context.User!.GetUserId(),true);
               });
           






            Field<NonNullGraphType<StringGraphType>>("decline_user_order")
               .Argument<NonNullGraphType<GuidGraphType>>("id")
               .ResolveAsync(async context =>
               {
                   var id = context.GetArgument<Guid>("id");
                   return await userProductRelation.DeclineOrderAsync(id,0,false);
               }).AuthorizeWithPolicy("UserManage");

            Field<NonNullGraphType<IntGraphType>>("delete_product_status")
               .Argument<NonNullGraphType<IntGraphType>>("id")
               .ResolveAsync(async context =>
               {
                   var id = context.GetArgument<int>("id");
                   return await userProductRelation.RemoveSelectedProductsStatusAsync(id);
               }).AuthorizeWithPolicy("UserManage");


            Field<NonNullGraphType<SelectedProductStatusGraphType>>("add_product_status")
              .Argument<NonNullGraphType<StringGraphType>>("name")
              .ResolveAsync(async context =>
              {
                  var name = context.GetArgument<string>("name");
                  return await userProductRelation.AddSelectedProductsStatusAsync(name);

              }).AuthorizeWithPolicy("UserManage");

            Field<NonNullGraphType<SelectedProductStatusGraphType>>("update_product_status")
                .Argument<NonNullGraphType<SelectedProductStatusInputGraphType>>("productStatus")
                .ResolveAsync(async context =>
                {
                    var data = context.GetArgument<SelectedProductsStatus>("productStatus");
                    return await userProductRelation.UpdateSelectedProductsStatusAsync(data);

                }).AuthorizeWithPolicy("UserManage");

        }
    }
}
