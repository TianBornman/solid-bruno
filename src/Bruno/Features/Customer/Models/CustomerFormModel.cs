namespace Bruno.Features.Customer.Models;

public class CustomerFormModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public CreateCustomerRequest ToCreateRequest() =>
        new(FirstName.Trim(), LastName.Trim(), Email.Trim(), PhoneNumber.Trim());

    public UpdateCustomerRequest ToUpdateRequest(Guid id) =>
        new(id, FirstName.Trim(), LastName.Trim(), Email.Trim(), PhoneNumber.Trim());

    public static CustomerFormModel FromDto(CustomerDto dto) => new()
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Email = dto.Email,
        PhoneNumber = dto.PhoneNumber
    };
}
