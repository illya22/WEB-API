﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product : BaseEntity
    {
        public int ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
