using System.Collections.Generic;

namespace Domain.Models
{
    public class Customer
    {
        public Customer()
        {
            Buy = new HashSet<Buy>();
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Buy> Buy { get; set; }
    }
}
