using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class LockTypeInputGraphType : InputObjectGraphType<LockType>
    {
        public LockTypeInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
