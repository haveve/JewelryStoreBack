using CourseWorkDB.Model.DetailsInfo.Material;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class MaterialColorGraphType:ObjectGraphType<MaterialColor>
    {
        public MaterialColorGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
