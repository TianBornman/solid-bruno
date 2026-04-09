using Bruno.Domain.Enums;

namespace Bruno.Application.DTOs.Booking;

public record ListBookingDto(int Skip, int Take, BookingStatus? Status, string? Search);
