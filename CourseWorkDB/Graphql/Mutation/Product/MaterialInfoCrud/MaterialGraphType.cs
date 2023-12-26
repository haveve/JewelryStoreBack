using CourseWorkDB.Model.DetailsInfo.Material;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class MaterialGraphType:ObjectGraphType<Material>
    {
        public MaterialGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
