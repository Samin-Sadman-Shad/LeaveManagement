using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.LeaveManagement.MVC.Controllers
{
    [Authorize(Roles = "Employee")]
    public class LeaveRequestsServiceController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsServiceController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
            _leaveTypeService = leaveTypeService;
        }

        // GET: LeaveRequestsServiceController
        [Authorize(Roles ="Administrator")]
        public ActionResult Index()
        {
            var model = _leaveRequestService.GetAdminLeaveRequestList();
            return View(model);
        }

        // GET: LeaveRequestsServiceController/Details/5
        public ActionResult Details(int id)
        {
            var model = _leaveRequestService.GetLeaveRequestById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveRequest(int id, bool approval)
        {
            try
            {
                await _leaveRequestService.ApproveLeaveRequest(id, approval);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LeaveRequestsServiceController/Create
        /// <summary>
        /// Load the create page first time
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _leaveTypeService.GetAll();

            //Note: DataValueField and DataTextValue must be same as the propertyname of the Type
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "LeaveTypeName");

            var viewModel = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTypeItems
            };
            return View(viewModel);
        }

        // POST: LeaveRequestsServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create( CreateLeaveRequestViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestService.CreateLeaveRequest(viewModel);
                if (response.Success)
                {
                   return RedirectToAction(nameof(Index));
                }
            }
            //load the create page again
            var leaveTypes = await _leaveTypeService.GetAll();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "LeaveTypeName");
            //use the data coming to use as the model, display the incoming data from submission form again
/*             viewModel = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTypeItems
            };*/
            viewModel.LeaveTypes = leaveTypeItems;
            return View(viewModel);

        }

        // GET: LeaveRequestsServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestsServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LeaveRequestsServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestsServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
