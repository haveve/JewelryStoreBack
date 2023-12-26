using CourseWorkDB.Model.DetailsInfo.Size;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SizeInfoCrudTypes
{
    public class SizeInfoGraphType:ObjectGraphType<SizeInfo>
    {
        public SizeInfoGraphType()
        {
            Field(el => el.Cost);
            Field(el => el.Count);
            Field(el => el.WeightGram);
            Field(el => el.Size, type:typeof(SizeGraphType));
        }
    }
}
