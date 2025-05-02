//using Aryap.Data.Models;
using Shared.DTos;
using Aryap.Shared.Models;

namespace Aryap.Core.Services.Interfaces
{
    public interface IClothesService
    {
        Task<IEnumerable<ClothDto>> GetAllClothesAsync();
        Task<ClothDto> GetClothByIdAsync(int id);
        Task AddClothAsync(ClothDto clothDto);
        Task UpdateClothAsync(int id, ClothDto clothDto);
        Task DeleteClothAsync(int id);
        Task AddToCartAsync(CartItem request);
        Task CheckoutAsync(CheckoutRequest request);
    }
}