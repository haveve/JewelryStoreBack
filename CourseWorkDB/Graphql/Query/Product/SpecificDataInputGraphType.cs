using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class SpecificDataInputGraphType : InputObjectGraphType<SpecificData>
    {
        public SpecificDataInputGraphType()
        {
            Field(el => el.Maximum).Directive("constraint_number", "min", 0);
            Field(el => el.Minimum).Directive("constraint_number", "min", 0);
        }
    }
}
