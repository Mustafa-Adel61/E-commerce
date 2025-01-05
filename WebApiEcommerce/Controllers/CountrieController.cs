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
    public class CountrieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountrieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork._Countrie.GetAll());
        } 
        [HttpGet("GetCountrieById/{id}")]
        public IActionResult GetById(int id)
        {
            var query = _unitOfWork._Countrie.GetById(c => c.Id == id);
            if(query == null)
                return NotFound("This Countrie NotFound");
            return Ok(query);
        }
        [HttpGet("Users&MerchantInCountrie/{id}")]
        public IActionResult GetByIdIncludeUsersAndMerchants(int id)
        {
            var query = _unitOfWork._Countrie.GetById(c => c.Id == id, new[] { "Users", "merchants" });
            if(query == null)
                return NotFound("This Countrie NotFound");
            return Ok(query);
        }

        [HttpPost]
        public IActionResult post([FromForm]CountrieDto dto)
        {
            var countrie = _mapper.Map<Countrie>(dto);
            _unitOfWork._Countrie.Post(countrie);
            _unitOfWork.complete();
            return Ok("This Countrie is Added Successfully");     
        }
        [HttpDelete("{id}")]
        public IActionResult Delete_Countrie(int id)
        {
            _unitOfWork._Countrie.Delete(_unitOfWork._Countrie.GetById(c => c.Id == id));
            _unitOfWork.complete();
            return Ok("This Countrie is Deleted Successfully");
        }
        [HttpPut("{id}")]
        public IActionResult Update_Countrie(int id,[FromForm]CountrieDto dto)
        {

           Countrie countrie = _mapper.Map<Countrie>(dto);
            countrie.Id = id;
            _unitOfWork._Countrie.Update(countrie);
            _unitOfWork.complete();
            return Ok("This Countrie Updated Successfully");
            
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateBatch(int id,JsonPatchDocument<Countrie> patch)
        {
            Countrie countrie = _unitOfWork._Countrie.GetById(c => c.Id == id);
            if (countrie == null)
                return NotFound("This User Is Not Founded");
            patch.ApplyTo(countrie, ModelState);
            _unitOfWork.complete();
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(countrie);
        }
    }
}
