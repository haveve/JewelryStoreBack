using CourseWorkDB.Graphql.Query.Product;
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
        }
    }
}
