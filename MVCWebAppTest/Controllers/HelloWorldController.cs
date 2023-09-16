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
            //Todo, this looks interesting.  I could probably cause the whole page to be reloaded if I returned a view from a controller and pasased in the page model as a model
            //I think I can just cast the ILogger and pass it around
            //That seems weird though.  So services only seves up an instance to the highest level in the call stack. Wait.  But then how will the client code write to the logger?
            //

            /*
             Need to make a test
            Call controller -> controller instantiates TestPageModel -> controller pases TestPageModel Instance to View() to invoke the same page
            Will asp.net
                Send copy of instance to browser to run alongside html
                set up a server instance of TestPageModel and use that to inform the building of the TestPage view from the layout file
             
             */
            TestPageModel tpm = new(logger as ILogger<TestPageModel>, null);
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
