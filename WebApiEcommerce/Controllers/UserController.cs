using AutoMapper;
using Ecommerce.core;
using Ecommerce.core.Const;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using Ecommerce.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }       
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var query = _unitOfWork._User.GetAll();
            return Ok(query);  
        }
        [HttpGet("GetAllIncludeAllRelatedData")]
        public IActionResult GetAllIncludeCountrie()
        {
            var query = _unitOfWork._User.GetAll(new[] { "countrie","countrie.Users", "countrie.merchants", "cart","cart.product" });
            return Ok(query);
        }
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetById(int id)
        {
            User user = _unitOfWork._User.GetById(c => c.Id == id, new[] { "countrie"});
            if(user==null)
                return NotFound("This User is NotFounded");
            return Ok(user);  
        }
        [HttpGet("Search")]
        public IActionResult GetWhoseCountine(string value)
        {
            IEnumerable<User> user = _unitOfWork._User.GetByExepressionQuery(c => c.Full_Name.Contains(value));
            if (user.Count() == 0)
                return NotFound($"Not Founded Any Users Contains {value}");
            return Ok(user);

        }
        [HttpGet("UsersInCountrie")]
        public IActionResult UsersInCountrie(string value)
        {
            var user = _unitOfWork._User.GetByExepressionQuery(c => c.countrie.Name.Contains(value));
            if (user.Count() == 0)
                return NotFound($"Not Founded Any Users his Countrie is {value}");
            return Ok(user);
            // return Ok(_unitOfWork._User.GetByExepressionQuery(c => c.CountrieCode == 1));
        }
        [HttpGet("GetWithFilter")]
        public IActionResult GetWithFilter()
        {
            return Ok(_unitOfWork._User.GetByExepressionQuery(c => c.CountrieCode == 1));
        
        }
        [HttpGet("GetWithOrderDescendingById")]
        public IActionResult GetWithOrder()
        {
            var sasa = _unitOfWork._User.GetByExepressionQuery(c => c.Id>0, 0, 0, m => m.Id,OrderBy.Descending);
            return Ok(sasa);
           // return Ok(_unitOfWork._User.GetByExepressionQuery(c => c.CountrieCode == 1));
        }
       
        [HttpPost("Register")]
        public IActionResult Post([FromForm]UserDto dto)
        {
           MemoryStream memoryStream=_unitOfWork._User.checkImage(dto.Image, out bool check,out string check2);
            if (!check) {
                return BadRequest(memoryStream);
            }
            User user = _mapper.Map<User>(dto);
            user.Image = memoryStream.ToArray();
            user.CreatedAt = DateTime.Parse(dto.CreatedAt);
            user.Date_of_birth = DateTime.Parse(dto.Date_of_birth);
            if (dto.Gender.ToLower() == "male")
                user.Gender = Gender.male;
            else
               user.Gender=Gender.female;
            _unitOfWork._User.Post(user);
            _unitOfWork.complete();
            return Ok("This User Added Successfully");
        }

        [HttpPut("UpdateProfile/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromForm] UserDto dto)
        {
            //CustomValidation
            //لو في اي مشكله في الحاجات الي جايه زي انه requerd ومش مبعوته من ال fronnt وهكذا
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            MemoryStream memoryStream =_unitOfWork._User.checkImage(dto.Image, out bool check, out string check2);
            if (!check)
                return BadRequest(check2);
             User user = _mapper.Map<User>(dto);
            user.Id = id;
            user.Image = memoryStream.ToArray();
            user.CreatedAt = DateTime.Parse(dto.CreatedAt);
            user.Date_of_birth = DateTime.Parse(dto.Date_of_birth);
            if (dto.Gender.ToLower() == "male")
                user.Gender = Gender.male;
            else
                user.Gender = Gender.female;
            _unitOfWork._User.Update(user);
            _unitOfWork.complete();

            return Ok("This User Updated Successfully");
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateBatch(int id, JsonPatchDocument<User> patch)
        {
            User user = _unitOfWork._User.GetById(c => c.Id == id);
            if (user == null)
                return NotFound("This User Is Not Founded");
            patch.ApplyTo(user,ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            _unitOfWork.complete();
            return Ok("This User Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
          User user=  _unitOfWork._User.GetById(c => c.Id == id);
            if (user == null)
                return NotFound("This User Not Founded");
            _unitOfWork._User.Delete(user);
            _unitOfWork.complete();
            return Ok("This User Deleted Successfully");
        }
    }
}
