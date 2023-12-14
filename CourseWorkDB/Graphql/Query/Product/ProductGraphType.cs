using GraphQL.Types;
using ProductModel = CourseWorkDB.Model.Product;


namespace CourseWorkDB.Graphql.Query.Product
{
    public class ProductGraphType:ObjectGraphType<ProductModel>
    {
        public ProductGraphType()
        {
            Field(el => el.Id);
            Field(el => el.MinCost);
            Field(el => el.Name);
            Field(el => el.Image);
            Field(el => el.CategoryId);
        }
    }
}
