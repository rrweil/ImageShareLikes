using HW4._5._21.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using HW4._5._21.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HW4._5._21.Web.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _environment;

        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            var vm = new IndexViewModel
            {
                Images = repo.GetAllImages()
            };
            return View(vm);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(Image image, IFormFile imageFile)
        {
            var filePath = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", filePath);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                imageFile.CopyTo(stream);
            }
            image.FilePath = filePath;
            image.DateUploaded = DateTime.Now;

            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            repo.AddImage(image);
            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            var vm = new ViewImageViewModel
            {
                Image = repo.GetImage(id)
        };
            return View(vm);
        }

        public IActionResult AddLike(int id)
        {

        }

        public IActionResult GetLikes (int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            return Json(repo.GetLikes(id));
        }
    }

}

