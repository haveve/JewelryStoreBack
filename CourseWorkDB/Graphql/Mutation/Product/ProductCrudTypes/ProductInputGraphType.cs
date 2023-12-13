using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;
using ProductModel = CourseWorkDB.Model.Product;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class DetailsProductInfoInputGraphType : InputObjectGraphType<DetailsProductInfo>
    {
        public DetailsProductInfoInputGraphType() 
        {
            Field(el => el.Id);
            Field(el => el.Image);
            Field(el => el.CreatorId);
            Field(el => el.Name);
            Field(el => el.CategoryId);
            Field(el => el.Description);
            Field(el => el.SpecificProductInfoId,nullable:true);
        }
    }
}
