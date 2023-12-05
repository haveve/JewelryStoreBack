using CourseWorkDB.ViewModel.Discount;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.DicountGrud
{
    public class AddDiscountInputGraphType : InputObjectGraphType<AddDiscount>
    {
        public AddDiscountInputGraphType()
        {
            Field(el => el.End);
            Field(el => el.Start);
            Field(el => el.Percent);
            Field(el => el.ProductId);
        }
    }
}
