#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using N4CoreLite.Services.Bases;
using N4CoreLite.Controllers.Bases;
using DataAccess.Entities;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using N4CoreLite.Models;

//Generated from Custom Template.
namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class DersController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly ServiceBase<Ders, DersQueryModel, DersCommandModel> _dersService;

        /* Replace many to many with the service base name in the current project for using many to many relational services. */
        //private readonly ServiceBase<Entity, QueryModel, CommandModel> _manyToManyService;

        public DersController(
			ServiceBase<Ders, DersQueryModel, DersCommandModel> dersService
            //, ServiceBase<Entity, QueryModel, CommandModel> manyToManyService
        )
        {
            _dersService = dersService;

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

        // GET: Ders
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var response = await _dersService.GetList();
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // GET: Ders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _dersService.GetItem(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // GET: Ders/Create
        public async Task<IActionResult> Create()
        {
            await SetViewData();
            return View();
        }

        // POST: Ders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DersCommandModel dersCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _dersService.Create(dersCommand);
                SetTempData(response.Message);
                if (response.IsSuccessful)                 
                    return RedirectToAction(nameof(Details), new { response.Data.Id });
            }
            await SetViewData();
            return View(dersCommand);
        }

        // GET: Ders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _dersService.GetItemCommand(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // POST: Ders/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DersCommandModel dersCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _dersService.Update(dersCommand);
                SetTempData(response.Message);
                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { response.Data.Id });
            }
            await SetViewData();
            return View(dersCommand);
        }

        // GET: Ders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _dersService.GetItem(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // POST: Ders/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _dersService.Delete(id);
            SetTempData(response.Message);
            return RedirectToAction(nameof(Index));
        }
	}
}
