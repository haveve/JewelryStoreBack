using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneColorGraphType:ObjectGraphType<StoneColor>
    {
        public StoneColorGraphType()
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
