using CourseWorkDB.Model.DetailsInfo.Stone;
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
