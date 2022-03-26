namespace Domain.Models
{
    public class Fruit
    {
        public int FruitId { get; set; }

        public int FruitTypeId { get; set; }

        public decimal Price { get; set; }

        public virtual FruitType FruitType { get; set; }
    }
}
