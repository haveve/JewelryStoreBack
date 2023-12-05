using CourseWorkDB.Graphql.Mutation.Product;
using CourseWorkDB.Graphql.Mutation.User;
using FileUploadSample;
using GraphQL;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;

namespace CourseWorkDB.Graphql.Mutation
{
    public class ShopMutation:ObjectGraphType
    {
        public ShopMutation()
        {
            Field<ProductMutation>("product")
                .Resolve(context => context).AuthorizeWithPolicy("ProductManage");

            Field<DetailProductInfoMutation>("detail_product")
                .Resolve(context => context).AuthorizeWithPolicy("ProductManage");

            Field<UserMutation>("user")
                .Resolve(context => context).AuthorizeWithPolicy("UserManage");

        }
    }
}
