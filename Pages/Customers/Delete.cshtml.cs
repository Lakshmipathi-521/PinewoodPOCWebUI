using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystemWebUI.Models;
using CRMSystemWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRMSystemWebUI.Pages_Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerService customerService;

        public DeleteModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerService.GetCustomerDetails(id ?? default);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await customerService.GetCustomerDetails(id ?? default);

            var customer = await customerService.DeleteExistingCustomerDetails(Customer);

            if (customer != null)
            {
                Customer = customer;
            }

            return RedirectToPage("./Index");
        }
    }
}
