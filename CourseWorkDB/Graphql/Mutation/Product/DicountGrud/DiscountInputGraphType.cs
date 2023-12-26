using CourseWorkDB.Model.ProductInfo;
using CourseWorkDB.ViewModel.Discount;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product.DicountGrud
{
    public class DiscountInputGraphType : InputObjectGraphType<Discount>
    {
        public DiscountInputGraphType()
        {
            Field(el => el.Id);
            Field(el => el.End);
            Field(el => el.Start);
            Field(el => el.Percent);
            Field(el => el.ProductId);
        }
    }
}
