using CourseWorkDB.ViewModel.Material;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class AddMaterialInfoInputGraphType:InputObjectGraphType<AddMaterialInfo>
    {
        public AddMaterialInfoInputGraphType() 
        {
            Field(el => el.MaterialId);
            Field(el => el.Percent);
            Field(el => el.MaterialColorId);
            Field(el => el.ProductId);
        }
    }
}
