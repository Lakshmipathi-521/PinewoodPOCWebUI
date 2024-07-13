using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystemWebUI.Models;
using CRMSystemWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRMSystemWebUI.Pages_Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService customerService;

        public CreateModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await customerService.AddNewCustomerDetails(Customer);

            return RedirectToPage("./Index");
        }
    }
}
