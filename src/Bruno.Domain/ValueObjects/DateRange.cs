using Bruno.Domain.Exceptions;

namespace Bruno.Domain.ValueObjects;

public record DateRange
{
	public DateTime StartDate { get; init; }
	public DateTime EndDate { get; init; }

	private DateRange() { }

	public DateRange(DateTime startDate, DateTime endDate)
	{
		if (endDate <= startDate)
			throw new DomainException("EndDate must be greater than StartDate.");

		StartDate = startDate;
		EndDate = endDate;
	}

	public bool IsInFuture() => StartDate >= DateTime.UtcNow;
	public bool IsInPast() => EndDate <= DateTime.UtcNow;
}
