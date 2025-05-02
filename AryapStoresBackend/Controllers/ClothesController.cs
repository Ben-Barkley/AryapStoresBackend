using Aryap.Core.Services.Interfaces;
using Aryap.Shared.Models;
//using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTos;
//using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothesController : ControllerBase
    {
        private readonly IClothesService _clothesService;

        public ClothesController(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClothes()
        {
            var clothes = await _clothesService.GetAllClothesAsync();
            return Ok(clothes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClothById(int id)
        {
            var cloth = await _clothesService.GetClothByIdAsync(id);
            return cloth != null ? Ok(cloth) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCloth([FromBody] ClothDto clothDto)
        {
            await _clothesService.AddClothAsync(clothDto);
            return Ok(new { Message = "Cloth added successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCloth(int id, [FromBody] ClothDto clothDto)
        {
            await _clothesService.UpdateClothAsync(id, clothDto);
            return Ok(new { Message = "Cloth updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCloth(int id)
        {
            await _clothesService.DeleteClothAsync(id);
            return Ok(new { Message = "Cloth deleted successfully." });
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem request)
        {
            await _clothesService.AddToCartAsync(request);
            return Ok(new { Message = "Item added to cart successfully." });
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            await _clothesService.CheckoutAsync(request);
            return Ok(new { Message = "Checkout completed successfully." });
        }
    }
}