using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class AddProductInputGraphType:InputObjectGraphType<AddProduct>
    {
        public AddProductInputGraphType() 
        {
            Field(el => el.CategoryId);
            Field(el => el.CreatorId);
            Field(el => el.Description);
            Field(el => el.Name);
            Field(el => el.SpecificProductInfoId, nullable: true);
        }
    }
}
