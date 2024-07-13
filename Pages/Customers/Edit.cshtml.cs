using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystemWebUI.Models;
using CRMSystemWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRMSystemWebUI.Pages_Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService customerService;

        public EditModel(ICustomerService customerService)
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
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await customerService.UpdateExistingCustomerDetails(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
