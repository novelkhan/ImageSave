using ImageSave.IService;
using ImageSave.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ImageSave.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;

        public HomeController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public string SaveFile(FileUpload fileObj)
        {
            Student oStudent = JsonConvert.DeserializeObject<Student>(fileObj.Student);

            if (fileObj.file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileObj.file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    oStudent.Photo = fileBytes;

                    oStudent = _studentService.Save(oStudent);
                    if (oStudent.StudentId > 0)
                    {
                        return "Saved";
                    }
                }
            }

            return "Failed";
        }



        [HttpGet]
        public JsonResult GetSavedStudent()
        {
            var student = _studentService.GetSavedStudent();
            student.Photo = this.GetImage(Convert.ToBase64String(student.Photo));
            return Json(student);
        }



        public byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }

            return bytes;
        }






















        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}