namespace CRMSystemWebUI.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerAddress { get; set; }
    public int CreditLimit { get; set; }
    public bool IsActive { get; set; }
    public required string Remarks { get; set; }

    public override string ToString()
    {
        return string.Format(
            @"{0}, {1}, {2}, {3}, {4}, {5}",
            CustomerId,
            CustomerName,
            CustomerAddress,
            CreditLimit,
            IsActive,
            Remarks
        );
    }
}
