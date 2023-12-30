using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class PaginationSortGraphType : InputObjectGraphType<PaginationSort>
    {
        public PaginationSortGraphType()
        {
            Field(el => el.Skip).Directive("constraint_number","min",0);
            Field(el => el.Take).Directive("constraint_number","min",1);
        }
    }
}
