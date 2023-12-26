using CourseWorkDB.Model.ProductInfo;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class CategoryInputGraphType:InputObjectGraphType<Category>
    {
        public CategoryInputGraphType() 
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
