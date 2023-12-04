using System.ComponentModel;

namespace CourseWorkDB.ViewModel.Product
{
    public class AddProduct
    {
        [Description("name")]
        public string Name { get; set; }
        [Description("description")]
        public string Description { get; set; }
        [Description("category_id")]
        public int CategoryId { get; set; }
        [Description("creator_id")]
        public int CreatorId { get; set; }
        [Description("specific_product_info_id")]
        public int? SpecificProductInfoId { get; set; } = null;
        [Description("image")]
        public string Image { get; set; } = string.Empty;
    }
}
