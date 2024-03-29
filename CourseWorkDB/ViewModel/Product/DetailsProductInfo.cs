﻿using System.ComponentModel;

namespace CourseWorkDB.ViewModel.Product
{
    public class DetailsProductInfo : AddProduct
    {
        [Description("id")]
        public int Id { get; set; }
        [Description("discount_percent")]
        public int? Discount { get; set; }

        public DetailsProductInfo()
        {

        }

        public DetailsProductInfo(AddProduct product, int id = 0)
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
