namespace Bruno.Features.Customer.Models;

public record ListCustomerRequest(int Skip, int Take, string? Search);
