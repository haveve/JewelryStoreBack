using CourseWorkDB.Graphql.Mutation;
using CourseWorkDB.Graphql.Query;
using GraphQL.Types;
using WebSocketGraphql.GraphQl.Directives.Validation;

namespace CourseWorkDB.Graphql.Schemas
{
    public class ShopSchema : Schema
    {
        public ShopSchema(IServiceProvider service) : base(service)
        {
            Directives.Register(new LengthDirective(),
                                new EmailDirective(),
                                new NumberRangeDivective());

            Query = service.GetRequiredService<ShopQuery>();
            Mutation = service.GetRequiredService<ShopMutation>();
        }
    }
}
