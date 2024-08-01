#nullable disable
using Microsoft.AspNetCore.Mvc;
using Business.Models;
using DataAccess.Entities;
using N4CoreLite.Services.Bases;

//Generated from Custom Template.
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDersController : ControllerBase
    {
        // TODO: Add service injections here
        private readonly ServiceBase<Ders, DersQueryModel, DersCommandModel> _dersService;

        public ApiDersController(ServiceBase<Ders, DersQueryModel, DersCommandModel> dersService)
        {
            _dersService = dersService;
        }

        // GET: api/ApiDers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _dersService.GetList();
            return Ok(response);
        }

        // GET: api/ApiDers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _dersService.GetItem(id);
            return Ok(response);
        }

		// POST: api/ApiDers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(DersCommandModel dersCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _dersService.Create(dersCommand);

			    //return CreatedAtAction("Get", new { id = ders.Id }, dersCommand);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/ApiDers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(DersCommandModel dersCommand)
        {
            if (ModelState.IsValid)
            {
                 var response = await _dersService.Update(dersCommand);

                //return NoContent();
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/ApiDers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _dersService.Delete(id);

            //return NoContent();
            return Ok(response);
        }
	}
}
