using AutoMapper;
using Ecommerce.core;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("AllOrders")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork._Order.GetAll());
        }
        [HttpGet("OrderById/{id}")]
        public IActionResult GetById(int id)
        {
            Order order = _unitOfWork._Order.GetById(c => c.Id == id);
            if (order == null)
                return NotFound("This Order NotFounded");
            return Ok(order);
        }
        [HttpGet("OrderByIdIncludeAllRelatedData")]
        public IActionResult GetByIdIncludeUser(int id)
        {
            Order order = _unitOfWork._Order.GetById(c => c.Id == id, new[] { "User" });
            if (order == null)
                return NotFound("This Order NotFounded");
            return Ok(order);
        }
        [HttpPost("CreateNewOrder")]
        public IActionResult post([FromForm]OrderDto dto)
        {
            Order order = _mapper.Map<Order>(dto);
            order.CreatedAt = DateTime.Parse(dto.CreatedAt);
            _unitOfWork._Order.Post(order);
            _unitOfWork.complete();
           // var id =(_unitOfWork._Order.GetLastElement(c => c.Id)).Id;
            return Ok("The User Added Successfully");
        }
        [HttpPut("UpdateOrder/{id}")]
        public IActionResult Update(int id, [FromForm]OrderDto dto)
        {
            Order order=_mapper.Map<Order>(dto);
            order.CreatedAt = DateTime.Parse(dto.CreatedAt);
            order.Id = id;
            _unitOfWork._Order.Update(order);
            _unitOfWork.complete();
            return Ok("The Order Updated Successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            Order order = _unitOfWork._Order.GetById(c => c.Id == id, new[] { "User" });
            if (order == null)
                return NotFound("This Order NotFounded");
            _unitOfWork._Order.Delete(order);
            _unitOfWork.complete();
            return Ok("This Order Deleteded Successfully");
        }
        [HttpPatch("UpdateOrder/{id}")]
        public IActionResult UpdatePatch(int id, JsonPatchDocument<Order> patch)
        {
            Order order = _unitOfWork._Order.GetById(c => c.Id == id);
            if (order == null)
                return NotFound("This Oreder Not Founded");
            patch.ApplyTo(order,ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.complete();
            return Ok("This Order Updated Successfully ");
        }
    }
}
