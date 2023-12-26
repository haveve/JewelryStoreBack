using CourseWorkDB.Model.DetailsInfo.Stone;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneTypeGraphType : ObjectGraphType<StoneType>
    {
        public StoneTypeGraphType()
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
