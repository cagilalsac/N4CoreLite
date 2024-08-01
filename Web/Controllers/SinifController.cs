#nullable disable

using Business.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N4CoreLite.Controllers.Bases;
using N4CoreLite.Models;
using N4CoreLite.Services.Bases;

//Generated from Custom Template.
namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class SinifController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly ServiceBase<Sinif, SinifQueryModel, SinifCommandModel> _sinifService;

        /* Replace many to many with the service base name in the current project for using many to many relational services. */
        //private readonly ServiceBase<Entity, QueryModel, CommandModel> _manyToManyService;

        public SinifController(
			ServiceBase<Sinif, SinifQueryModel, SinifCommandModel> sinifService
            //, ServiceBase<Entity, QueryModel, CommandModel> manyToManyService
        )
        {
            _sinifService = sinifService;

            //_manyToManyService = manyToManyService;
        }

        protected override async Task SetViewData(string message = null, PageOrderModel pageOrder = null)
        {
            await base.SetViewData(message, pageOrder); // End message in service with '.' for success, '!' for danger Bootstrap CSS classes to be used in the View
            //ViewData["ManyToManyIds"] = new MultiSelectList(await _manyToManyService.GetList(), "Id", "Adi");
        }

        protected override void SetTempData(string message)
        {
            base.SetTempData(message); // End message in service with '.' for success, '!' for danger Bootstrap CSS classes to be used in the View
        }

        // GET: Sinif
        public async Task<IActionResult> Index()
        {
            var response = await _sinifService.GetList();
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // GET: Sinif/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _sinifService.GetItem(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // GET: Sinif/Create
        public async Task<IActionResult> Create()
        {
            await SetViewData();
            return View();
        }

        // POST: Sinif/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinifCommandModel sinifCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _sinifService.Create(sinifCommand);
                SetTempData(response.Message);
                if (response.IsSuccessful)                 
                    return RedirectToAction(nameof(Details), new { response.Data.Id });
            }
            await SetViewData();
            return View(sinifCommand);
        }

        // GET: Sinif/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _sinifService.GetItemCommand(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // POST: Sinif/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SinifCommandModel sinifCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _sinifService.Update(sinifCommand);
                SetTempData(response.Message);
                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { response.Data.Id });
            }
            await SetViewData();
            return View(sinifCommand);
        }

        // GET: Sinif/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _sinifService.GetItem(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // POST: Sinif/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _sinifService.Delete(id);
            SetTempData(response.Message);
            return RedirectToAction(nameof(Index));
        }
	}
}
