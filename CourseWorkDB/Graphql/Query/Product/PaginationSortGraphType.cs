using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class PaginationSortGraphType : InputObjectGraphType<PaginationSort>
    {
        public PaginationSortGraphType()
        {
            Field(el => el.Skip);
            Field(el => el.Take);
        }
    }
}
