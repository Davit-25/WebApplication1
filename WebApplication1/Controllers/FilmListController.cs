using AspNetCore;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using WebApplication1.Entities;
using WebApplication1.Iterfaces;
using WebApplication1.Models;
using System.Collections.Generic;
using PagedList;
using Microsoft.CodeAnalysis.Options;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Humanizer;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class FilmListController : Controller
    {

        protected CinemaTableContext _context;
        private readonly IFilmListService _filmListService;


        public FilmListController(CinemaTableContext context, IFilmListService filmListService)
        {

            _context = context;
            _filmListService = filmListService;
        }
        
        [Route("Main page")]
        [Route("Main page/FilmList")]
        [Route("FilmList/Films")]
        [AllowAnonymous]
         public IActionResult FilmList(string currentFilter,string searchString, int pg=1)
         {
            ViewData["currentFilter"] = searchString;
            var objFilms = _filmListService.GetFilmsList(searchString);
            const int pageSize = 5;
            if (pg<1)
            {
                pg = 1;
            }
            int recsCount = objFilms.Count();
            var pager=new Paging(recsCount,pg, pageSize);
            int recSkip=(pg-1) *pageSize;
            var data = objFilms.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Paging= pager;

            return View(data);

        }


        [Route("FilmList/Create")]
        [Route("FilmList/Create/{id?}")]
        public IActionResult Create()
        {
            return View();
        }


        [Route("FilmList/Created")]
        [Route("FilmList/Created/{id?}")]

        public IActionResult Created([FromHeader] ModelFilmList modelFilmList)
        {
            FilmListResponce filmListResponce = new FilmListResponce();
            if (ModelState.IsValid)
            {
                filmListResponce = _filmListService.CreateFilmList(modelFilmList);
                return RedirectToAction("FilmList");
            }
            return View(filmListResponce.modelFilmList);
        }
     
        [Route("FilmList/Delete")]
        [Route("FilmList/Delete/{id?}")]
        
       
        public IActionResult Delete( int? id)
        {
            if (id==null|| id==0)
            {   
               return NotFound();
            }

            var deleteFilmResponce = _filmListService.GetFilmList(new GetFilmListRequest() { GetRequestID = (int)id });
            if (deleteFilmResponce==null)
            {
                return NotFound();
            }
           
            return View(deleteFilmResponce.GetModelFilmList);
        }

        [Route("FilmList/Delete")]
        [Route("FilmList/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Deleted( int? id)
        {

            var filmResponce = _filmListService.GetFilmList(new GetFilmListRequest() { GetRequestID = (int)id });
            if (filmResponce == null)
            {
                return NotFound();
            }
            _filmListService.DeleteFilmList(new DeleteFilmListRequest() { DeleteRequestID = (int)id });
            return RedirectToAction("FilmList");
        }

        [Route("FilmList/Edit")]
        [Route("FilmList/Edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var getFilmResponce = _filmListService.GetFilmList(new GetFilmListRequest() { GetRequestID = (int)id });
            if (getFilmResponce == null)
            {
                return NotFound();
            }

            return View(getFilmResponce.GetModelFilmList);
        }

        [HttpPost]
        [Route("FilmList/Edit")]
        [Route("FilmList/Edit/{id?}")]
        public IActionResult Edited([FromHeader] ModelFilmList UpdatetoFilmList)
        {

            if (ModelState.IsValid)
            {
              _filmListService.UpdateFilmList(new UpdateFilmListRequest() { UpdatetoFilmList = UpdatetoFilmList });
              return RedirectToAction("FilmList");
            }
            return View(UpdatetoFilmList); 
        }

       
    }
}
