namespace Bruno.Features.Booking.Models;

public class BookingFormModel
{
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime? StartDate { get; set; } = DateTime.UtcNow.Date;
    public DateTime? EndDate { get; set; } = DateTime.UtcNow.Date.AddDays(1);
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Active;

    public int Days => StartDate.HasValue && EndDate.HasValue && EndDate > StartDate
        ? (int)(EndDate.Value - StartDate.Value).TotalDays : 0;

    public CreateBookingRequest ToCreateRequest() =>
        new(DateTime.SpecifyKind(StartDate!.Value, DateTimeKind.Utc), DateTime.SpecifyKind(EndDate!.Value, DateTimeKind.Utc), TotalPrice, Status, VehicleId, CustomerId);

    public UpdateBookingRequest ToUpdateRequest(Guid id) =>
        new(id, DateTime.SpecifyKind(StartDate!.Value, DateTimeKind.Utc), DateTime.SpecifyKind(EndDate!.Value, DateTimeKind.Utc), TotalPrice, Status, VehicleId, CustomerId);

    public static BookingFormModel FromDto(BookingDto dto) => new()
    {
        VehicleId = dto.VehicleId,
        CustomerId = dto.CustomerId,
        StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
        EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc),
        TotalPrice = dto.TotalPrice,
        Status = dto.Status
    };
}
