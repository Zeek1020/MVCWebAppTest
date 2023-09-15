using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MVCWebAppTest.Pages
{

    public class CounterModel : PageModel
    {

        public int Counter { get; set; }

        public void OnGet()
        {
            Counter = 0;
        }

        public void Inc() { 
            this.Counter++;
        }

    }
}
