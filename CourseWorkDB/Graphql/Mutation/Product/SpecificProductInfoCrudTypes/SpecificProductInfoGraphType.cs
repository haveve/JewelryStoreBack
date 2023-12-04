using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class SpecificProductInfoGraphType:ObjectGraphType<SpecifictProductInfo>
    {
        public SpecificProductInfoGraphType()
        {
            Field(el => el.Shape, type: typeof(ShapeTypeGraphType));
            Field(el => el.Lock, type: typeof(LockTypeGraphType));
        }
    }
}
