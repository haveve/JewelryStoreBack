using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class History
    {
        [Description("total_cost")]
        public decimal TotalCost { get; set; }
        [Description("count")]
        public int Count { get; set; }
        [Description("name")]
        public string Name { get; set; }
        [Description("image")]
        public string Image { get; set; }
        [Description("disabled")]
        public bool Disabled { get; set; }
        [Description("category_id")]
        public int CategoryId {  get; set; }
    }
}
