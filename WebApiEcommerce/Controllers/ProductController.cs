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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = _unitOfWork._Product.GetAll();
            if(products==null)
                return NotFound("There is No Product Available");
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product=_unitOfWork._Product.GetById(c=>c.Id==id);
            if (product == null)
                return NotFound("This Product Not Founded");
            return Ok(product);
        }
        [HttpGet("GetAllIncludeAllRelatedData")]
        public IActionResult GetByIdIncludeMerchant(int id)
        {
            Product product=_unitOfWork._Product.GetById(c=>c.Id==id, new[] { "merchant","merchant.countrie" });
            if (product == null)
                return NotFound("This Product Not Founded");
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Post([FromForm]ProductDto dto)
        {
            MemoryStream memoryStream = _unitOfWork._Product.checkImage(dto.Image, out bool check, out string check1);
            if (!check)
                return BadRequest(check1);
            Product product = _mapper.Map<Product>(dto);
            product.Image= memoryStream.ToArray();
            product.CreateAt = DateTime.Parse(dto.CreateAt);
            _unitOfWork._Product.Post(product);
            _unitOfWork.complete();
            return Ok("This Product Added Successfully");
        }
        [HttpPut("{id}")]
        public IActionResult update(int id ,[FromForm]ProductDto dto)
        {
            MemoryStream memoryStream = _unitOfWork._Product.checkImage(dto.Image, out bool check, out string check1);
            if (!check)
                return BadRequest(check1);
            Product product= _mapper.Map<Product>(dto);
            product.Id= id;
            product.Image = memoryStream.ToArray();
            product.CreateAt = DateTime.Parse(dto.CreateAt);
            _unitOfWork._Product.Update(product);
            _unitOfWork.complete();
            return Ok("This Product Updated Successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _unitOfWork._Product.GetById(c=>c.Id==id);
            if (product == null)
                return NotFound("This Product Not Founded");
            _unitOfWork._Product.Delete(product);
            _unitOfWork.complete();
            return Ok("This Product Is Deleted Successfully");

        }
        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, JsonPatchDocument<Product> patch)
        {
            Product product = _unitOfWork._Product.GetById(c => c.Id == id);
            if (product == null)
                return NotFound("This Product Not Founded");
            patch.ApplyTo(product);
            _unitOfWork.complete();
            return Ok("This Product Updated Successfully");
        }
    }
}
