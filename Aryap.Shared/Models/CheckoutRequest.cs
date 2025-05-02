// Represents the checkout request payload

namespace Aryap.Shared.Models
{
    public class CheckoutRequest
    {
        //public int UserId { get; set; } // User ID
        public List<CartItem> Items { get; set; } = new List<CartItem>(); // List of cart items
    }
}
