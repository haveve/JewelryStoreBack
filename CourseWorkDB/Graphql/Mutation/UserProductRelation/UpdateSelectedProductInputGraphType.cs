using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class UpdateSelectedProductInputGraphType : InputObjectGraphType<SelectedProduct>
    {

        public UpdateSelectedProductInputGraphType()
        {
            Field(el => el.ProductId);
            Field(el => el.SizeId);
            Field(el => el.Count);
            Field(el => el.Id);
        }
    }
}
