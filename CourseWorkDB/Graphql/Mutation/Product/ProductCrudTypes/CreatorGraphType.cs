using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class CreatorGraphType : ObjectGraphType<Creator>
    {
        public CreatorGraphType() 
        {
            Field(el => el.Name);
            Field(el => el.Id);
        }
    }
}
