using CourseWorkDB.Model.ProductInfo;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class CategoryGraphType:ObjectGraphType<Category>
    {
        public CategoryGraphType() 
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
