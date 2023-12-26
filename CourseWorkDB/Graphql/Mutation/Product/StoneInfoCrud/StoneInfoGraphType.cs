using CourseWorkDB.Model.DetailsInfo.Stone;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class StoneInfoGraphType : ObjectGraphType<StoneInfo>
    {
        public StoneInfoGraphType()
        {
            Field(el => el.Count);
            Field(el => el.WeightCarat);
            Field(el => el.Color, type:typeof(StoneColorGraphType));
            Field(el => el.Type, type: typeof(StoneTypeGraphType));
            Field(el => el.Shape, type: typeof(StoneShapeGraphType));
        }
    }
}
