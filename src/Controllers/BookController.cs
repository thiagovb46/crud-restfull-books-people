using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWith.NET.Business;
using RestWith.NET.Data.VO;
using RestWith.NET.Hypermedia.Filters;

namespace RestWith.NET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
            _logger = logger;
        }
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVo book)
        {
            if (book == null)
                return BadRequest();
            return StatusCode(201, _bookBusiness.Create(book));
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get ()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetById(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null)
                return NotFound();
           return  Ok(book);
        }
         
        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVo book, int id) 
        {
            if (book == null)
                return BadRequest();
            return Ok(_bookBusiness.Update(book, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null)
                return NotFound("Book not founded");
            _bookBusiness.Delete(id);
            return Ok("Deleted with Sucess");
        }
    }
}