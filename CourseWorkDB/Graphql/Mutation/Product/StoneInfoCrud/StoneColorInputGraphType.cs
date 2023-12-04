using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneColorInputGraphType : InputObjectGraphType<StoneColor>
    {
        public StoneColorInputGraphType()
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
