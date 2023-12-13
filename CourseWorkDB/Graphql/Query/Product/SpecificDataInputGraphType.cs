using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class SpecificDataInputGraphType : InputObjectGraphType<SpecificData>
    {
        public SpecificDataInputGraphType()
        {
            Field(el => el.Maximum);
            Field(el => el.Minimum);
        }
    }
}
