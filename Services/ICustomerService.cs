using CRMSystemWebUI.Models;

namespace CRMSystemWebUI.Services;

public interface ICustomerService
{
    const int DEFAULT_NO_OF_RECORDS = 10;
    Task<IEnumerable<Customer>> GetCustomers(
        string? customerName = default,
        int noOfRecords = DEFAULT_NO_OF_RECORDS
    );
    Task<Customer> GetCustomerDetails(int customerId);
    Task<Customer> AddNewCustomerDetails(Customer newCustomerDetails);
    Task<Customer> UpdateExistingCustomerDetails(Customer existingCustomerDetails);
    Task<Customer> DeleteExistingCustomerDetails(Customer existingCustomerDetails);
}
