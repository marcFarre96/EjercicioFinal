namespace Domain.Models
{
    public class Buy
    {
        public int BuyId { get; set; }

        public int CustomerId { get; set; }

        public int FruitId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
