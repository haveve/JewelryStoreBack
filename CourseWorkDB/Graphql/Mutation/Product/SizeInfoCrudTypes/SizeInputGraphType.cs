using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.SizeInfoCrudTypes
{
    public class SizeInputGraphType:InputObjectGraphType<Size>
    {
        public SizeInputGraphType() 
        {
            Field(el => el.Id);
            Field(el => el.Name);
        }
    }
}
