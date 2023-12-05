using System.ComponentModel;

namespace CourseWorkDB.ViewModel.Discount
{
    public class AddDiscount
    {
        [Description("start")]
        public DateOnly Start { get; set; }
        [Description("end")]
        public DateOnly End { get; set; }
        [Description("product_id")]
        public int ProductId { get; set; }
        [Description("percent")]
        public int Percent { get; set; }
    }
}
