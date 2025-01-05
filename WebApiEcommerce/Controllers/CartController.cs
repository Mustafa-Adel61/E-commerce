using AutoMapper;
using Ecommerce.core;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Cart> carts = _unitOfWork._Cart.GetAll();
            return Ok(carts); 
        }
        [HttpPost("AddProductToCart")]
        public IActionResult Post([FromForm]CartDto dto)
        {
            var cart = _mapper.Map<Cart>(dto);
            _unitOfWork._Cart.Post(cart);
            _unitOfWork.complete();
            return Ok("The Product Added To Cart Successfully ");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm]CartDto dto)
        {
            Cart cart = _mapper.Map<Cart>(dto);
            cart.Id = id;
            _unitOfWork._Cart.Update(cart);
            _unitOfWork.complete();
            return Ok("This Cart Update Successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Cart cart = _unitOfWork._Cart.GetById(c => c.Id == id);
            _unitOfWork._Cart.Delete(cart);
            _unitOfWork.complete();
            return Ok("This Cart Deleted Successfully");
        }
    }
}
