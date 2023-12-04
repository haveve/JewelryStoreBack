using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneShapeGraphType : ObjectGraphType<StoneShape>
    {
        public StoneShapeGraphType()
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
