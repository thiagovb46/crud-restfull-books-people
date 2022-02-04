using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWith.NET.Business;
using RestWith.NET.Data.VO;
using RestWith.NET.Hypermedia.Filters;

namespace RestWith.NET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;
        private readonly ILogger<PersonController> _logger;


        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
            _logger = logger;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HyperMediaFilter))]

        public IActionResult Get ([FromQuery] string name, string sortDirection,int pageSize, int page)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name,sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetById(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
                return NotFound();
           return  Ok(person);
        }
        [HttpGet("findPersonByName")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var person = _personBusiness.FindByName(firstName,lastName);
            if (person == null)
                return NotFound();
           return  Ok(person);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVo person)
        {
            if (person == null)
                return BadRequest();
            return StatusCode(201,_personBusiness.Create(person));
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVo person, int id) 
        {
            if (person == null)
                return BadRequest();
            return Ok(_personBusiness.Update(person, id));
        }

        [HttpPatch("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Disable(long id)
        {
            var person = _personBusiness.Disable(id);
            if (person == null)
                return NotFound();
           return  Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
                return NotFound("Person not founded");
            _personBusiness.Delete(id);
            return Ok("Deleted with Sucess");
        }
    }
}