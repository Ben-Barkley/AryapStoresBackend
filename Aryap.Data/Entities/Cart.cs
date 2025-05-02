using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aryap.Data.Entities
{
    // Represents cart items in the database
    public class Cart
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }     
        public int UserId { get; set; }
        public int ClothId { get; set; }
        public int Quantity { get; set; }

        //Navigation Properties
        public User User { get; set; }
        public Cloth Cloth { get; set; }
    }
}