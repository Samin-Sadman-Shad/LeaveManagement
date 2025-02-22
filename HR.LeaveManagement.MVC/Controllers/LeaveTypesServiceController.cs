using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            var leaveTypeVM = await _leaveTypeService.GetById(id);
            return View(leaveTypeVM);
        }

        // GET: LeaveTypesServiceController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypesServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeViewModel viewModel)
        {
            try
            {
                var response = await _leaveTypeService.Create(viewModel);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if(response.StatusCode == Services.Base.HttpStatusCode._400)
                {
                    ModelState.AddModelError("Errors", response.ValidationErrors.SelectMany(i => i).ToString());
                }
                
            }
            catch
            {
                ModelState.AddModelError("Error", "Unexpected error occurred. Please contact to service Administrater");
            }
            return View(viewModel);

        }

        // GET: LeaveTypesServiceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveTypeVM = await _leaveTypeService.GetById(id);
            return View(leaveTypeVM);
        }

        // POST: LeaveTypesServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeViewModel  viewModel)
        {
            try
            {
                var response = await _leaveTypeService.Update(id, viewModel);
                if(response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == Services.Base.HttpStatusCode._400)
                {
                    ModelState.AddModelError("Errors", response.ValidationErrors.SelectMany(i => i).ToString());
                }

            }
            catch
            {
                ModelState.AddModelError("Error", "Unexpected error occurred. Please contact to service Administrater");
            }
            return View(viewModel);
        }

        // GET: LeaveTypesServiceController/Delete/5
/*        public async Task<ActionResult> Delete(int id)
        {
            var leaveTypeVM = await _leaveTypeService.GetById(id);
            return View(leaveTypeVM);
        }*/

        // POST: LeaveTypesServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _leaveTypeService.Delete(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == Services.Base.HttpStatusCode._400)
                {
                    ModelState.AddModelError("Errors", response.ValidationErrors.SelectMany(i => i).ToString());
                }

            }
            catch
            {
                ModelState.AddModelError("Error", "Unexpected error occurred. Please contact to service Administrater");
            }
            return BadRequest();
        }
    }
}
