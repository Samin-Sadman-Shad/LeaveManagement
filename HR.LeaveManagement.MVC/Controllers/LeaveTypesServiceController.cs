using HR.LeaveManagement.MVC.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers
{
    public class LeaveTypesServiceController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesServiceController(ILeaveTypeService service) 
        {
            _leaveTypeService = service;
        }
        // GET: LeaveTypesServiceController
        public async Task<ActionResult> Index()
        {
            var leaveTypeVM = await _leaveTypeService.GetAll();
            return View(leaveTypeVM);
        }

        // GET: LeaveTypesServiceController/Details/5
        public async Task< ActionResult> Details(int id)
        {
            return View();
        }

        // GET: LeaveTypesServiceController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypesServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypesServiceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: LeaveTypesServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypesServiceController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: LeaveTypesServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
