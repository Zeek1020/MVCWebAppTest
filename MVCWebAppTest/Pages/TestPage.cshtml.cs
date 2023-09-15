using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MVCWebAppTest.Models;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text.Json;

namespace MVCWebAppTest.Pages
{
    [BindProperties]
    public class TestPageModel : PageModel
    {

        protected int _StudentID = 0;
        protected string _Name = string.Empty;
        protected bool _Active = false;

        public int StudentID { get => _StudentID; set => _StudentID = value; }
        
        public string Name { get => _Name; set => _Name = value; }
        
        public bool Active { get => _Active; set => _Active = value; }

        protected ILogger<TestPageModel> logger;
        protected HttpClient _client { get; set; }

        public void Log( string s) {
            this.logger.LogInformation(s);
        }

        public TestPageModel( ILogger<TestPageModel> logger, IHttpClientFactory clientFactory ) {
            this._client = clientFactory.CreateClient( "default" );
            this.logger = logger;
        }

        public void OnGet() {
            this.logger.LogInformation($"Get: {StudentID}:{Name}:{Active}");
        }

        public async void OnPostAsync() {
                        
            ActionResult<Student> result = await this._client.GetFromJsonAsync<Student>($"/HelloWorld/GetStudentID/{this.StudentID}");

            Student student = result.Value ?? new Student();

            this.StudentID = student.Id;
            this.Name = student.Name;
            this.Active = student.active;
            
            this.logger.LogInformation($"Post: {StudentID}:{Name}:{Active}");
        }

        public override string ToString() {
            return $"{this.StudentID},{this.Name},{this.Active}";
        }

    }

}
