using CourseWorkDB.Model.ProductInfo;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class CreatorInputGraphType:InputObjectGraphType<Creator>
    {
        public CreatorInputGraphType() 
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
