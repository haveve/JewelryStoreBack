﻿using System.ComponentModel;

namespace CourseWorkDB.Model.UserProduct
{
    public class History
    {
        [Description("id")]
        public Guid Id { get; set; }
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
        public int CategoryId { get; set; }
        [Description("address")]
        public string Address { get; set; }
        [Description("date")]
        public DateTime Date { get; set; }
        [Description("size_name")]
        public string SizeName { get; set; }
        [Description("size_id")]
        public int SizeId { get; set; }
    }
}