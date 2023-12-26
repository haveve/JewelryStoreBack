using CourseWorkDB.ViewModel.Product;
using System.ComponentModel;

namespace CourseWorkDB.Model.ProductInfo
{
    public class Product
    {
        [Description("id")]
        public int Id { get; set; }
        [Description("name")]
        public string Name { get; set; }
        [Description("image")]
        public string Image { get; set; }
        [Description("MinCost")]
        public decimal MinCost { get; set; }
        [Description("discount_percent")]
        public int? Discount { get; set; }
        [Description("category_id")]
        public int CategoryId { get; set; }
    }
}
