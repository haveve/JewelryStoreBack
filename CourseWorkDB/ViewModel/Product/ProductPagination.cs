namespace CourseWorkDB.ViewModel.Product
{
    public class ProductPagination
    {
        public int Count {  get; set; }
        public IEnumerable<CourseWorkDB.Model.Product> Products { get; set; }
    }
}
