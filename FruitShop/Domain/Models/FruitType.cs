using System.Collections.Generic;

namespace Domain.Models
{
    public class FruitType
    {
        public FruitType()
        {
            Fruit = new HashSet<Fruit>();
        }

        public int FruitTypeId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Fruit> Fruit { get; set; }
    }
}
