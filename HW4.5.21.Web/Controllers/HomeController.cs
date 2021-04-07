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
using Newtonsoft.Json;

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

            var likedImages = HttpContext.Session.Get<List<int>>("likedImages");

            var vm = new ViewImageViewModel
            {
                Image = repo.GetImage(id)
            };
            
            if (likedImages != null)
            {
                vm.likedImages = likedImages;
            }

            if (likedImages != null && likedImages.Contains(id))
            {
                vm.alreadyLiked = true;
            } else
            {
                vm.alreadyLiked = false;
            }

            return View(vm);
        }

        public IActionResult AddLike(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            repo.AddLike(id);

            List<int> likedImages = HttpContext.Session.Get<List<int>>("likedImages");
            if(likedImages == null)
            {
                likedImages = new List<int>();
            }

            likedImages.Add(id);
            HttpContext.Session.Set("likedImages", likedImages);

            return Json(repo.GetLikes(id));
        }

        public IActionResult GetLikes(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            return Json(repo.GetLikes(id));
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}

