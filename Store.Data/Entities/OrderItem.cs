namespace Store.Data.Entities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}