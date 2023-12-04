using GraphQL.Types;
using ProductModel = CourseWorkDB.Model.Product;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class ProductGraphType:ObjectGraphType<ProductModel>
    {
        public ProductGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Image);
            Field(el => el.CreatorId);
            Field(el => el.Name);
            Field(el => el.CategoryId);
            Field(el => el.Description);
            Field(el => el.Discount, nullable: true);
            Field(el => el.SpecificProductInfoId, nullable: true);

        }
    }

}