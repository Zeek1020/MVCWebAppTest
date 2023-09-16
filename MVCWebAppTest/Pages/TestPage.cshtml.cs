using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MVCWebAppTest.Models;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text.Json;

namespace MVCWebAppTest.Pages
{
    [BindProperties]
    public class TestPageModel : PageModel {

        protected int _StudentID = 0;
        protected string _Name = string.Empty;
        protected bool _Active = false;

        public int StudentID { get => _StudentID; set => _StudentID = value; }

        public string Name { get => _Name; set => _Name = value; }

        public bool Active { get => _Active; set => _Active = value; }

        protected ILogger<TestPageModel> logger;
        protected HttpClient _client { get; set; }

        public void Log(string s) {
            this.logger.LogInformation(s);
        }

        public TestPageModel(ILogger<TestPageModel> logger, IHttpClientFactory clientFactory) {
            this._client = clientFactory.CreateClient("default");
            this.logger = logger;
        }

        public void OnGet(int Id, string Name, bool Active) {

            this.StudentID = Id;
            if (Id == 123) {
                this.Name = "Testing";
            }
            else this.Name = Name;
            this.Active = Active;
        }

        public void OnPostAsync(Student student) {

            this.StudentID = student.Id;
            if (student.Id == 123) {
                this.Name = "Testing";
            } else {
                this.Name = student.Name;
            }
            this.Active = !this.Active;
        }

        public void OtherPost(Student student) {
            this.Name = "OTHER POST!!!";
            this.logger.LogInformation("ASDF");
        }

        public override string ToString() {
            return $"{this.StudentID},{this.Name},{this.Active}";
        }

    }

}
