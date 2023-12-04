using GraphQL.Types;
using ProductModel = CourseWorkDB.Model.Product;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class ProductInputGraphType:InputObjectGraphType<ProductModel>
    {
        public ProductInputGraphType() 
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
