using CourseWorkDB.ViewModel.Material;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud
{
    public class AddMaterialInfoGraphType : ObjectGraphType<AddMaterialInfo>
    {
        public AddMaterialInfoGraphType() 
        {
            Field(el => el.MaterialId);
            Field(el => el.Percent);
            Field(el => el.MaterialColorId);
            Field(el => el.ProductId);
        }
    }
}
