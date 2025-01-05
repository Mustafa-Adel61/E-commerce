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
    public class MerchantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MerchantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork._Merchant.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            Merchant merchant = _unitOfWork._Merchant.GetById(c => c.Id == id);
            if(merchant==null)
                return NotFound("This Merchant is NotFounded");
            return Ok(merchant);
        }
        [HttpGet("GetAllIncludeAllRelatedData")]
        public IActionResult GetByIdIncludeCountrie(int id) 
        {
            Merchant merchant = _unitOfWork._Merchant.GetById(c => c.Id == id, new[] { "countrie" });
            if(merchant==null)
                return NotFound("This Merchant is NotFounded");
            return Ok(merchant);
        }
        [HttpGet("MerchantsInCountrie")]
        public IActionResult MerchantsInCountrie(string value)
        {
            var sasa = _unitOfWork._User.GetByExepressionQuery(c => c.countrie.Name.Contains(value));
            return Ok(sasa);
            // return Ok(_unitOfWork._User.GetByExepressionQuery(c => c.CountrieCode == 1));
        }
        [HttpPost("Register")]
        public IActionResult post([FromForm]MerchantDto dto)
        {
            // Merchant merchant = _unitOfWork._Merchant.MerchantDtoToMerchant(dto);
            Merchant merchant = _mapper.Map<Merchant>(dto);
            merchant.CreatedAt = DateTime.Parse(dto.CreatedAt);
            _unitOfWork._Merchant.Post(merchant);
            _unitOfWork.complete();
            return Ok("The Merchant Added Successfully");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id ,[FromForm]MerchantDto dto)
        {
          Merchant merchant=  _mapper.Map<Merchant>(dto);
            merchant.CreatedAt = DateTime.Parse(dto.CreatedAt);
            merchant.Id = id;
            _unitOfWork._Merchant.Update(merchant);
            _unitOfWork.complete();
            return Ok("This Merchant Updated Successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        { 
            Merchant merchant = _unitOfWork._Merchant.GetById(c => c.Id == id);
            if (merchant == null)
                return NotFound("This Merchant is NotFound");
            _unitOfWork._Merchant.Delete(merchant);
            _unitOfWork.complete();
            return Ok("This Merchant is Deleted Successfully");
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBatch(int id,JsonPatchDocument<Merchant>patch)
        {
            Merchant merchant = _unitOfWork._Merchant.GetById(c => c.Id == id);
            if (merchant == null)
                return NotFound("This Merchant Not Founded");
            patch.ApplyTo(merchant,ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            _unitOfWork.complete();
            return Ok("This Merchant Updated Successfully");
        }
    }
}
