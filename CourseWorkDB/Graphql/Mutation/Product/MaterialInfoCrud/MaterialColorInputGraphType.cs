using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class MaterialColorInputGraphType : InputObjectGraphType<MaterialColor>
    {
        public MaterialColorInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
