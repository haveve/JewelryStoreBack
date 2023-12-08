using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.UserProductRelation
{
    public class SelectedProductGraphType : ObjectGraphType<SelectedProduct>
    {

        public SelectedProductGraphType()
        {
            Field(el => el.ProductId);
            Field(el => el.SizeId);
            Field(el => el.UserId);
            Field(el => el.Count);
            Field(el => el.StatusId);
            Field(el => el.Id);
            Field(el => el.Persent);
        }
    }
}
