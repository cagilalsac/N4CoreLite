#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using N4CoreLite.Services.Bases;
using N4CoreLite.Controllers.Bases;
using N4CoreLite.Models;
using DataAccess.Entities;
using Business.Models;
using Business.Services.Reports.Bases;
using Business.Models.Reports;

//Generated from Custom Template.
namespace Web.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class OgrenciRaporuController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IOgrenciRaporuService _ogrenciRaporuService;

        /* Replace many to many with the service base name in the current project for using many to many relational services. */
        //private readonly ServiceBase<Entity, QueryModel, CommandModel> _manyToManyService;

        public OgrenciRaporuController(
			IOgrenciRaporuService ogrenciRaporuService
            //, ServiceBase<Entity, QueryModel, CommandModel> manyToManyService
        )
        {
            _ogrenciRaporuService = ogrenciRaporuService;

            //_manyToManyService = manyToManyService;
        }

        // GET: Reports/OgrenciRaporu
        public async Task<IActionResult> Index()
        {
            var response = await _ogrenciRaporuService.Getir();
            return View(response.Data);
        }

        // POST: Reports/OgrenciRaporu/Index
        [HttpPost]
        public async Task<IActionResult> Index(OgrenciRaporuViewModel viewModel)
        {
            var response = await _ogrenciRaporuService.Getir(viewModel);
            return PartialView("_List", response.Data);
        }
	}
}
