using CourseWorkDB.ViewModel.Discount;
using System.ComponentModel;

namespace CourseWorkDB.Model.ProductInfo
{
    public class Discount : AddDiscount
    {
        [Description("id")]
        public int Id { get; set; }

        public Discount()
        {

        }

        public Discount(DateOnly start, DateOnly end, int productId, int percent, int id)
        {
            Start = start;
            End = end;
            ProductId = productId;
            Percent = percent;
            Id = id;
        }
    }
}
