using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class LockTypeGraphType : ObjectGraphType<LockType>
    {
        public LockTypeGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
