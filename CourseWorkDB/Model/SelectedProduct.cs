using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class SelectedProduct
    {
        [Description("id")]
        public Guid Id { get; set; } = Guid.Empty;
        [Description("count")]
        public int Count { get; set; }
        [Description("user_id")]
        public int UserId { get; set; } = 1;
        [Description("product_id")]
        public int ProductId { get; set; }
        [Description("size_id")]
        public int SizeId { get; set; }
        [Description("status_id")]
        public int StatusId { get; set; } = 1;
        [Description("present")]
        public bool Persent { get; set; } = false; 
    }

    public enum SelectedStatus
    {
        Beloved = 1,
        InBucket
    }
}
