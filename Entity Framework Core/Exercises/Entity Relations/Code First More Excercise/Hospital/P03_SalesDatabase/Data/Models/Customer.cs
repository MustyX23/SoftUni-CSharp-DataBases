using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03_SalesDatabase.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }
        public int CustomerId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Unicode(false)]
        public string Email { get; set; }

        public string CreditCardNumber {  get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

    }
}
