using ImageCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace ImageCRUD.Controllers
{
    public class ItemController : Controller
    {


        private readonly App2025Context context;
        private readonly IWebHostEnvironment enviroment;

        public ItemController(App2025Context context, IWebHostEnvironment enviroment)
        {

            this.context = context;
            this.enviroment = enviroment;
        }
        
        public IActionResult Index()
        {
            var data = context.Items.ToList();
            return View(data);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Item ch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniquefilename = UploadImage(ch);
                    var data = new Item()
                    {
                        Name = ch.Name,
                        Age = ch.Age,
                        Education = ch.Education,
                        Image = uniquefilename
                    };
                    context.Items.Add(data);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Model property is not valid please check");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(ch);
        }

        private string UploadImage(Item chr)
        {
            string uniquefilename = "";
            if (chr.Imagepath != null)
            {
                string UploadFolder = Path.Combine(enviroment.WebRootPath, "images");
                uniquefilename = Guid.NewGuid().ToString() + "_" + chr.Imagepath.FileName;
                string FilePath = Path.Combine(UploadFolder, uniquefilename);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    chr.Imagepath.CopyTo(fileStream);
                }
            }
            return uniquefilename;
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = context.Items.Where(e => e.Id == id).SingleOrDefault();
                if (data != null)
                {
                    string deleteFromFolder = Path.Combine(enviroment.WebRootPath, "images");
                    string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, data.Image);
                    if (currentImage != null)
                    {
                        if (System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);
                        }
                    }
                    context.Items.Remove(data);
                    context.SaveChanges();
                    //TempData["Success"] = "Record Deleted!";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = context.Items.Where(e => e.Id == id).SingleOrDefault();
            return View(data);
        }
        public IActionResult Edit(int id)
        {
            var data = context.Items.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Edit(Item chr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = context.Items.Where(e => e.Id == chr.Id).SingleOrDefault();
                    string uniqueFileName = string.Empty;
                    if (chr.Imagepath != null)
                    {
                        if (data.Image != null)
                        {
                            string filepath = Path.Combine(enviroment.WebRootPath, "images", data.Image);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                        }
                        uniqueFileName = UploadImage(chr);
                    }
                    data.Name = chr.Name;
                    data.Age = chr.Age;
                    data.Education = chr.Education;

                    if (chr.Imagepath != null)
                    {
                        data.Image = uniqueFileName;
                    }
                    context.Items.Update(data);
                    context.SaveChanges();
                    //TempData["Success"] = "Record Updated Successfully!";
                }
                else
                {
                    return View(chr);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("index");
        }


    }
}
