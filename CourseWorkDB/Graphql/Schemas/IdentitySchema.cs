using CourseWorkDB.Graphql.Query;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Schemas
{
    public class IdentitySchema:Schema
    {
        public IdentitySchema(IServiceProvider service) : base(service)
        {
            Query = service.GetRequiredService<IdentityQuery>();
            Mutation = service.GetRequiredService<IdentityMutation>();
        }
    }
}
