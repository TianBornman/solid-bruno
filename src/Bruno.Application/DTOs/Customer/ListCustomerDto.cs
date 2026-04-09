namespace Bruno.Application.DTOs.Customer;

public record ListCustomerDto(int Skip, int Take, string? Search);
