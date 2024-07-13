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
    public class IndexModel : PageModel
    {
        private readonly ICustomerService customerService;

        public IndexModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await this.customerService.GetCustomers(noOfRecords: 100);

            Customer = result.ToList();
        }
    }
}
