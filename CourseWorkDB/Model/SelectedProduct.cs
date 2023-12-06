namespace CourseWorkDB.Model
{
    public class SelectedProduct
    {
        public Guid Id { get; set; } = Guid.Empty;
        public int Count { get; set; }
        public int UserId { get; set; } = 1;
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int StatusId { get; set; } = 1;
    }

    public enum SelectedStatus
    {
        Beloved = 1,
        InBucket,
        Ordered
    }
}
