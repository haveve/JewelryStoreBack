using CourseWorkDB.Graphql.Mutation;
using CourseWorkDB.Graphql.Query;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Schemas
{
    public class ShopSchema:Schema
    {
        public ShopSchema(IServiceProvider service) : base(service)
        {
            Query = service.GetRequiredService<ShopQuery>();
            Mutation = service.GetRequiredService<ShopMutation>();
        }
    }
}
