using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Models
{
    public class Products
    {
        [Key]
        public Guid ProdId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Name can only be 50 characters")]
        public string ProdName { get; set; }

        public string Category { get; set; }
        public string  Discription { get; set; }
        public bool isAvailable { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQty { get; set; }
    }
}