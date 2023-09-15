using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MVCWebAppTest.Models;
using MVCWebAppTest.Pages;
using MVCWebAppTest.Pages.Shared;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MVCWebAppTest.Controllers
{
    [ApiController]
    public class HelloWorldController : ControllerBase
    {

        ILogger<HelloWorldController> logger;

        public HelloWorldController( ILogger<HelloWorldController> logger ) {
            this.logger = logger;
        }

        [HttpGet("{controller}/GetStudentId/{id}")]
        public ActionResult<Student> GetStudentId( int id ) {
            Student student = new Student();
            student.Id = id;
            student.Name = "Get: Test";
            student.active = true;
            return student;
        }

        [HttpPost("{Controller}/SubmitUser")]
        public ActionResult<Student> SubmitUser( Student student ) {
            student.Id += 1;
            student.active = !student.active;
            student.Name = "Tag: " + student.Name;

            return CreatedAtAction( nameof(SubmitUser), student );
        }

    }
}
