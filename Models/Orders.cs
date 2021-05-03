using System;

namespace Models
{
    public class Orders
    {
        public Guid OrderId { get; set; }
        public string OrderItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderQty { get; set; }
        public decimal TotalPrice { get; set; }

    }
}