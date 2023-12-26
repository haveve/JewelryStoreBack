using CourseWorkDB.Model.UserProduct;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class AddSelectedProductInputGraphType:InputObjectGraphType<SelectedProduct>
    {

        public AddSelectedProductInputGraphType()
        {
            Field(el => el.ProductId);
            Field(el => el.SizeId);
            Field(el => el.Count);
        }
    }
}
