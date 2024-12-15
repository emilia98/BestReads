using BestReads.Data.Models;
using BestReads.OutputModels;
using BestReads.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestReads.MVC.Controllers
{
    [Route("genre")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public static string ControllerName => nameof(GenreController).Replace("Controller", "");

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        // GET: GenreController
        [HttpGet("")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var genres = _genreService.GetAllWithBooks<GenreOutputModel>(withDeleted: false, count: null);
			var model = new GenreListOutputModel
            {
                Genres = genres.ToList()
            };

            return View(model);
        }

        [HttpGet("id/{id:int}")]
        public ActionResult Details(int id)
        {
            ViewData["ID"] = id;
            return View();
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: GenreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenreController/Edit/5
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

        // GET: GenreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenreController/Delete/5
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
