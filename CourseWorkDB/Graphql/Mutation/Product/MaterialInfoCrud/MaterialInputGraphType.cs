using CourseWorkDB.Model.DetailsInfo.Material;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class MaterialInputGraphType : InputObjectGraphType<Material>
    {
        public MaterialInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
