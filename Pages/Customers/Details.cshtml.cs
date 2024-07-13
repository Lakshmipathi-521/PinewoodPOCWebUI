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
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService customerService;

        public DetailsModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

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
    }
}
