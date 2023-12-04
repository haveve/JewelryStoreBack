using CourseWorkDB.ViewModel.Size;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud
{
    public class AddStoneInfoGraphType:ObjectGraphType<AddStoneInfo>
    {
        public AddStoneInfoGraphType()
        {
            Field(el => el.StoneTypeId);
            Field(el => el.ProductId);
            Field(el => el.StoneShapeId);
            Field(el => el.StoneColorId);
            Field(el => el.WeightCarat);
            Field(el => el.Count);

        }
    }
}
