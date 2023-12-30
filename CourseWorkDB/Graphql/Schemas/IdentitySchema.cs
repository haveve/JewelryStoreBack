using CourseWorkDB.Graphql.Query;
using GraphQL.Types;
using WebSocketGraphql.GraphQl.Directives.Validation;

namespace CourseWorkDB.Graphql.Schemas
{
    public class IdentitySchema:Schema
    {
        public IdentitySchema(IServiceProvider service) : base(service)
        {
            Directives.Register(new LengthDirective(),
                                new EmailDirective(),
                                new NumberRangeDivective());

            Query = service.GetRequiredService<IdentityQuery>();
            Mutation = service.GetRequiredService<IdentityMutation>();
        }
    }
}
