using Bruno.Domain.Exceptions;

namespace Bruno.Domain.ValueObjects;

public class DateRange
{
	public DateTime StartDate { get; }
	public DateTime EndDate { get; }

	public DateRange() { }

	public DateRange(DateTime startDate, DateTime endDate)
	{
		if (endDate <= startDate)
			throw new DomainException("EndDate must be greater than StartDate.");

		StartDate = startDate;
		EndDate = endDate;
	}

	public bool IsInFuture()
	{
		return StartDate >= DateTime.UtcNow;
	}

	public bool IsInPast()
	{
		return EndDate <= DateTime.UtcNow;
	}
}
