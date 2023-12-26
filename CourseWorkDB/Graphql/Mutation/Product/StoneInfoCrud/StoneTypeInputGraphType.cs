using CourseWorkDB.Model.DetailsInfo.Stone;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneTypeInputGraphType : InputObjectGraphType<StoneShape>
    {
        public StoneTypeInputGraphType()
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
