using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class SpecificDataGraphType : ObjectGraphType<SpecificData>
    {
        public SpecificDataGraphType()
        {
            Field(el => el.Minimum);
            Field(el => el.Maximum);
            Field(el => el.Count);
        }
    }
}
