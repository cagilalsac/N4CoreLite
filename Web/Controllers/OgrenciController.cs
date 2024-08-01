#nullable disable

using Business.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N4CoreLite.Controllers.Bases;
using N4CoreLite.Models;
using N4CoreLite.Services.Bases;

//Generated from Custom Template.
namespace Web.Controllers
{
    public class OgrenciController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly ServiceBase<Ogrenci, OgrenciQueryModel, OgrenciCommandModel> _ogrenciService;
        private readonly ServiceBase<Sinif, SinifQueryModel, SinifCommandModel> _sinifService;

        /* Replace many to many with the service base name in the current project for using many to many relational services. */
        private readonly ServiceBase<Ders, DersQueryModel, DersCommandModel> _dersService;

        public OgrenciController(
            ServiceBase<Ogrenci, OgrenciQueryModel, OgrenciCommandModel> ogrenciService
            , ServiceBase<Sinif, SinifQueryModel, SinifCommandModel> sinifService
            , ServiceBase<Ders, DersQueryModel, DersCommandModel> dersService
        )
        {
            _ogrenciService = ogrenciService;

            _sinifService = sinifService;
            _dersService = dersService;
        }

        protected override async Task SetViewData(string message = null, PageOrderModel pageOrder = null)
        {
            await base.SetViewData(message, pageOrder); // End message in service with '.' for success, '!' for danger Bootstrap CSS classes to be used in the View
            ViewData["SinifId"] = new SelectList((await _sinifService.GetList()).Data, "Id", "Adi");
            ViewData["DersIdleri"] = new MultiSelectList((await _dersService.GetList()).Data, "Id", "Adi");
        }

        protected override void SetTempData(string message)
        {
            base.SetTempData(message); // End message in service with '.' for success, '!' for danger Bootstrap CSS classes to be used in the View
        }

        // GET: Ogrenci
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var response = await _ogrenciService.GetList();
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // GET: Ogrenci/IndexPageOrder
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexPageOrder(PageOrderModel pageOrder)
        {
            //pageOrder.OrderExpressionsForEntityProperties = new List<string>()
            //{
            //    "Adı",
            //    "Soyadı",
            //    "Doğum Tarihi",
            //    "MezunMu",
            //    "Uyruk"
            //};
            var response = await _ogrenciService.GetList(pageOrder);
            await SetViewData(response.Message, pageOrder);
            return View(nameof(Index), response.Data);
        }

        // GET: Ogrenci/Details/5
        [Authorize(Roles = "admin,kullanici")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _ogrenciService.GetItem(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // GET: Ogrenci/Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            await SetViewData();
            return View();
        }

        // POST: Ogrenci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(OgrenciCommandModel ogrenciCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _ogrenciService.Create(ogrenciCommand);
                SetTempData(response.Message);
                if (response.IsSuccessful)                 
                    return RedirectToAction(nameof(Details), new { response.Data.Id });
            }
            await SetViewData();
            return View(ogrenciCommand);
        }

        // GET: Ogrenci/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _ogrenciService.GetItemCommand(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // POST: Ogrenci/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(OgrenciCommandModel ogrenciCommand)
        {
            if (ModelState.IsValid)
            {
                var response = await _ogrenciService.Update(ogrenciCommand);
                SetTempData(response.Message);
                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { response.Data.Id });
            }
            await SetViewData();
            return View(ogrenciCommand);
        }

        // GET: Ogrenci/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _ogrenciService.GetItem(id);
            await SetViewData(response.Message);
            return View(response.Data);
        }

        // POST: Ogrenci/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _ogrenciService.Delete(id);
            SetTempData(response.Message);
            return RedirectToAction(nameof(Index));
        }
	}
}
