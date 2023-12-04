using CourseWorkDB.ViewModel.Product;
using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class Product: AddProduct
    {
        [Description("id")]
        public int Id { get; set; }
        [Description("discount_percent")]
        public int? Discount { get; set; }

        public Product()
        {

        }

        public Product(AddProduct product,int id = 0)
        {
            Image = product.Image;
            Name = product.Name;
            CategoryId = product.CategoryId;
            CreatorId = product.CreatorId;
            Description = product.Description;
            Id = id;
        }
    }
}
