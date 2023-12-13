namespace CourseWorkDB.ViewModel.Product
{
    public class ProductSort
    {
        public IEnumerable<int>? Material { get; set; }
        public IEnumerable<int>? MaterialColors { get; set; }
        public IEnumerable<int>? Sizes { get; set; }
        public IEnumerable<int>? StoneTypes { get; set; }
        public IEnumerable<int>? StoneColors { get; set; }
        public IEnumerable<int>? StoneShapes { get; set; }
        public IEnumerable<int>? Creators { get; set; }
        public IEnumerable<int>? LockTypes { get; set; }
        public IEnumerable<int>? ShapeTypes { get; set; }
        public bool OnlyDiscount { get; set; } = false;
        public bool? IsCheaper {  get; set; }
        public SpecificData? SpecificData { get; set; }
        public int? CategoryId { get; set; }
        public string? Search {  get; set; }
        public PaginationSort Pagination { get; set; }
    }
}
