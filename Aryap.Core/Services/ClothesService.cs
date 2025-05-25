using Aryap.Core.Services.Interfaces;
using Aryap.Data.Entities;
using Aryap.Shared.Models;
using Microsoft.EntityFrameworkCore;
using org.omg.CORBA;
using Shared.DTos;
//using Aryap.Shared.Models;
using Aryap.Shared.Repositories.Interface;

namespace Aryap.Core.Services
{
    public class ClothesService : IClothesService
    {
        private readonly IRepository<Cloth> _clothRepository;
        private readonly IRepository<Cart> _cartRepository;
        

        public ClothesService(IRepository<Cloth> clothRepository, IRepository<Cart> cartRepository)
        {
            _clothRepository = clothRepository;
            _cartRepository = cartRepository;
           
        }

        public async Task<IEnumerable<ClothDto>> GetAllClothesAsync()
        {
            var clothes = await _clothRepository.GetAllAsync();
            return clothes.Select(c => new ClothDto
            {
                ClothId = c.ClothId,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                Stock = c.Stock
            });
        }

        public async Task<ClothDto> GetClothByIdAsync(int id)
        {
            var cloth = await _clothRepository.GetByIdAsync(id);
            return new ClothDto
            {
                ClothId = cloth.ClothId,
                Name = cloth.Name,
                Description = cloth.Description,
                Price = cloth.Price,
                ImageUrl = cloth.ImageUrl,
                Stock = cloth.Stock
            };
        }

        public async Task AddClothAsync(ClothDto clothDto)
        {
            var cloth = new Cloth
            {
                Name = clothDto.Name,
                Description = clothDto.Description,
                Price = clothDto.Price,
                ImageUrl = clothDto.ImageUrl,
                Stock = clothDto.Stock
            };

            await _clothRepository.AddAsync(cloth);
            await _clothRepository.SaveChangesAsync();
        }

        public async Task UpdateClothAsync(int id, ClothDto clothDto)
        {
            var cloth = await _clothRepository.GetByIdAsync(id);
            if (cloth != null)
            {
                cloth.Name = clothDto.Name;
                cloth.Description = clothDto.Description;
                cloth.Price = clothDto.Price;
                cloth.ImageUrl = clothDto.ImageUrl;
                cloth.Stock = clothDto.Stock;

                await _clothRepository.UpdateAsync(cloth);
                await _clothRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteClothAsync(int id)
        {
            await _clothRepository.DeleteAsync(id);
            await _clothRepository.SaveChangesAsync();
        }

        public async Task AddToCartAsync(CartItem request)
        {
            var cartItem = await _cartRepository.FindAsync(c => c.ClothId == request.ClothId);
            if (cartItem.Any())
            {
                var existingCartItem = cartItem.First();
                existingCartItem.Quantity += request.Quantity;
                await _cartRepository.UpdateAsync(existingCartItem);
            }
            else
            {
                var newCartItem = new Cart
                {
                    ClothId = request.ClothId,
                    Quantity = request.Quantity
                };
                await _cartRepository.AddAsync(newCartItem);
            }

            await _cartRepository.SaveChangesAsync();
        }

        public async Task CheckoutAsync(CheckoutRequest request)
        {
            foreach (var item in request.Items)
            {
                var cloth = await _clothRepository.GetByIdAsync(item.ClothId);
                if (cloth == null || cloth.Stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for Cloth ID {item.ClothId}");
                }

                cloth.Stock -= item.Quantity;
                await _clothRepository.UpdateAsync(cloth);

                var cartItem = await _cartRepository.FindAsync(c => c.ClothId == item.ClothId);
                if (cartItem.Any())
                {
                    await _cartRepository.DeleteAsync(cartItem.First().ClothId);
                }
            }

            await _clothRepository.SaveChangesAsync();
        }
    }
}