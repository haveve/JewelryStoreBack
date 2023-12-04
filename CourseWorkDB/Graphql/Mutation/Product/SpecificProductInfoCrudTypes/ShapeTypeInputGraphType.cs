using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class ShapeTypeInputGraphType : InputObjectGraphType<ShapeType>
    {
        public ShapeTypeInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
