using CourseWorkDB.Model.DetailsInfo.Size;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SizeInfoCrudTypes
{
    public class SizeGraphType:ObjectGraphType<Size>
    {
        public SizeGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
