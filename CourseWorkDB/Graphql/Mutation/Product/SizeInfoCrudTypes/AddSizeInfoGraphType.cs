using CourseWorkDB.ViewModel.Size;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SizeInfoCrudTypes
{
    public class AddSizeInfoGraphType:ObjectGraphType<AddSizeInfo>
    {
        public AddSizeInfoGraphType()
        {
            Field(el => el.SizeId);
            Field(el => el.ProductId);
            Field(el => el.WeightGram);
            Field(el => el.Cost);
            Field(el => el.Count);
        }
    }
}
