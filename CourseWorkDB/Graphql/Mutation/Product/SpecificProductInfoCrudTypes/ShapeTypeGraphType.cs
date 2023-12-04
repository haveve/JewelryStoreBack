using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo
{
    public class ShapeTypeGraphType : ObjectGraphType<ShapeType>
    {
        public ShapeTypeGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
