using Aryap.Shared.Abstractions;

namespace Aryap.Data.Entities
{
    // Represents clothing items in the database
    public class Cloth : BaseEntity
    {   
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}