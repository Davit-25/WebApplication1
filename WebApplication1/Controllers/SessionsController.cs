using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Entities;
using WebApplication1.Iterfaces;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SessionsController : Controller
    {
        protected CinemaTableContext _context;
        private readonly ISessionsService _sessionService;

        public SessionsController(CinemaTableContext context, ISessionsService sessionsService)
        {
            _context = context;
            _sessionService = sessionsService;
        }

        [Route("Main page")]
        [Route("Main page/Sessions")]
        [Route("Sessions/Session")]
        [AllowAnonymous]
        public IActionResult AllCategories(int pg = 1)
        {
            var objSessions = _sessionService.GetAllSessions();
            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = objSessions.Count();
            var pager = new Paging(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = objSessions.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Paging = pager;
            return View(data);
        }

        [Route("Sessions/Create")]
        [Route("Sessions/Create/{id?}")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Sessions/Created")]
        [Route("Sessions/Created/{id?}")]
        public IActionResult Created([FromHeader] ModelSessions modelSessions)
        {
            CreateSessionsResponce createSessionsResponce = new CreateSessionsResponce();
            if (ModelState.IsValid)
            {
                createSessionsResponce = _sessionService.CreateSessions(modelSessions);
                return RedirectToAction("AllCategories");
            }
            return View(createSessionsResponce.modelSessions);
        }

        [Route("Sessions/Delete")]
        [Route("Sessions/Delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var deleteSessResponce = _sessionService.GetSessions(new GetSessionsRequest() { GetSesRequestID = (int)id });
            if (deleteSessResponce == null)
            {
                return NotFound();
            }

            return View(deleteSessResponce.GetModelSessions);
        }
       
        [Route("Sessions/Delete")]
        [Route("Sessions/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Deleted(int? id)
        {

            var sessResponce = _sessionService.GetSessions(new GetSessionsRequest() { GetSesRequestID = (int)id });
            if (sessResponce == null)
            {
                return NotFound();
            }
            _sessionService.DeleteSessions(new DeleteSessionsRequest() { DeleteSessRequestID = (int)id });
            return RedirectToAction("AllCategories");
        }

        [Route("Sessions/Edit")]
        [Route("Sessions/Edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var getSessResponce = _sessionService.GetSessions(new GetSessionsRequest() { GetSesRequestID = (int)id });
            if (getSessResponce == null)
            {
                return NotFound();
            }

            return View(getSessResponce.GetModelSessions);
        }

        [HttpPost]
        [Route("Sessions/Edit")]
        [Route("Sessions/Edit/{id?}")]
        public IActionResult Edited([FromHeader] ModelSessions modelSessions)
        {

            if (ModelState.IsValid)
            {
                _sessionService.UpdateSessions(new UpdateSessionsRequest() { UpdateToSessions = modelSessions });
                return RedirectToAction("AllCategories");
            }
            return View(modelSessions);
        }
    }
}
