using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class MaterialInfoGraphType:ObjectGraphType<MaterialInfo>
    {
        public MaterialInfoGraphType()
        {
            Field(el => el.Percent);
            Field(el => el.Material,type: typeof(MaterialGraphType));
            Field(el => el.MaterialColor, type: typeof(MaterialColorGraphType));
        }
    }
}
