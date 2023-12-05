using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.Discount;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.DicountGrud
{
    public class DiscountGraphType : ObjectGraphType<Discount>
    {
        public DiscountGraphType()
        {
            Field(el => el.Id);
            Field(el => el.End);
            Field(el => el.Start);
            Field(el => el.Percent);
            Field(el => el.ProductId);
        }
    }
}
