using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneShapeInputGraphType : InputObjectGraphType<StoneShape>
    {
        public StoneShapeInputGraphType()
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
