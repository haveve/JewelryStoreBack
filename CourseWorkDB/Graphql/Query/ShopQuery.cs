using CourseWorkDB.Graphql.Mutation.UserProductRelation;
using CourseWorkDB.Graphql.Query.Product;
using CourseWorkDB.Graphql.Query.User;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query
{
    public class ShopQuery:ObjectGraphType
    {
        public ShopQuery() 
        {
            Field<ProductQuery>("product")
                .Resolve(context => context);

            Field<DetailProductInfoQuery>("detail_product")
                .Resolve(context => context);

            Field<UserQuery>("users")
                .Resolve(context => context).AuthorizeWithPolicy("UserManage");

            Field<UserProductRelationQuery>("user_product")
                .Resolve(context => context).AuthorizeWithPolicy("Authorized");
        }
    }
}
