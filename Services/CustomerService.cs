using System.Text;
using System.Text.Json;
using CRMSystemWebUI.Models;
using CRMSystemWebUI.Services;

public class CustomerService : ICustomerService
{
    private HttpClient httpClient;
    private string? baseUrl;
    private string? customerServiceBaseUrl;

    public CustomerService()
    {
        var apiPrefix = "api/customers";

        baseUrl = Environment.GetEnvironmentVariable("CUSTOMERS_SERVICE_BASE_URL");

        if (string.IsNullOrEmpty(baseUrl))
            throw new ArgumentException("Invalid Customers Service Base URL Specified!");

        customerServiceBaseUrl = $"{baseUrl}/{apiPrefix}";

        httpClient = new HttpClient();
    }

    public async Task<Customer> AddNewCustomerDetails(Customer newCustomerDetails)
    {
        var customerServiceUrl = customerServiceBaseUrl;
        using StringContent jsonContent =
            new(JsonSerializer.Serialize(newCustomerDetails), Encoding.UTF8, "application/json");

        using var response = await httpClient.PostAsync(customerServiceUrl, jsonContent);

        response.EnsureSuccessStatusCode().WriteRequestToConsole();

        var addedCustomerRecord = await response.Content.ReadFromJsonAsync<Customer>();

#pragma warning disable CS8603 // Possible null reference return.
        return addedCustomerRecord;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Customer> DeleteExistingCustomerDetails(Customer existingCustomerDetails)
    {
        var customerServiceUrl = customerServiceBaseUrl;
        using StringContent jsonContent =
            new(
                JsonSerializer.Serialize(existingCustomerDetails),
                Encoding.UTF8,
                "application/json"
            );

        var request = new HttpRequestMessage(HttpMethod.Delete, customerServiceUrl)
        {
            Content = jsonContent
        };

        using var response = await httpClient.SendAsync(request);

        response.WriteRequestToConsole();

        var deletedCustomerRecord = await response.Content.ReadFromJsonAsync<Customer>();

#pragma warning disable CS8603 // Possible null reference return.
        return deletedCustomerRecord;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Customer> GetCustomerDetails(int customerId)
    {
        var customerServiceUrl = $"{customerServiceBaseUrl}/details/{customerId}";
        var filteredCustomerDetail = await httpClient.GetFromJsonAsync<Customer>(
            customerServiceUrl
        );

#pragma warning disable CS8603 // Possible null reference return.
        return filteredCustomerDetail;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<IEnumerable<Customer>> GetCustomers(
        string? customerName = null,
        int noOfRecords = 10
    )
    {
        var customerServiceUrl = customerServiceBaseUrl;

        if (string.IsNullOrEmpty(customerName))
        {
            customerServiceUrl = $"{customerServiceUrl}/?noOfRecords={noOfRecords}";
        }
        else
        {
            customerServiceUrl = $"{customerServiceUrl}/search/{customerName}";
        }

        var response = httpClient.GetFromJsonAsAsyncEnumerable<Customer>(customerServiceUrl);
        var customers = await response.ToListAsync<Customer>();

        return customers;
    }

    public async Task<Customer> UpdateExistingCustomerDetails(Customer existingCustomerDetails)
    {
        var customerServiceUrl = customerServiceBaseUrl;
        using StringContent jsonContent =
            new(
                JsonSerializer.Serialize(existingCustomerDetails),
                Encoding.UTF8,
                "application/json"
            );

        using var response = await httpClient.PutAsync(customerServiceUrl, jsonContent);

        response.EnsureSuccessStatusCode().WriteRequestToConsole();

        var updatedCustomerRecord = await response.Content.ReadFromJsonAsync<Customer>();

#pragma warning disable CS8603 // Possible null reference return.
        return updatedCustomerRecord;
#pragma warning restore CS8603 // Possible null reference return.
    }
}
