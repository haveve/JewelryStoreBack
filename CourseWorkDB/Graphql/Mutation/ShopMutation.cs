using CourseWorkDB.Graphql.Mutation.Product;
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
                .Resolve(context => context);

            Field<DetailProductInfoMutation>("detail_product")
                .Resolve(context => context);
        }
    }
}
