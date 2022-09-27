﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Customer : BaseEntity
    {
        public int PersonId { get; set; }
        public int DiscountValue { get; set; }

        public Person Person { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
    }
}
