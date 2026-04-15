using Bruno.Domain.Exceptions;
using Bruno.Domain.ValueObjects;

namespace Bruno.Tests.Domain;

public class DateRangeTests
{
    [Fact]
    public void Constructor_WhenEndDateBeforeStartDate_ThrowsDomainException()
    {
        var start = DateTime.UtcNow.AddDays(5);
        var end = DateTime.UtcNow.AddDays(1);

        Assert.Throws<DomainException>(() => new DateRange(start, end));
    }

    [Fact]
    public void Constructor_WhenEndDateEqualsStartDate_ThrowsDomainException()
    {
        var date = DateTime.UtcNow.AddDays(2);

        Assert.Throws<DomainException>(() => new DateRange(date, date));
    }

    [Fact]
    public void Constructor_WhenValid_CreatesDateRange()
    {
        var start = DateTime.UtcNow.AddDays(1);
        var end = DateTime.UtcNow.AddDays(3);

        var range = new DateRange(start, end);

        Assert.Equal(start, range.StartDate);
        Assert.Equal(end, range.EndDate);
    }

    [Fact]
    public void IsInFuture_WhenStartDateIsInFuture_ReturnsTrue()
    {
        var range = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3));

        Assert.True(range.IsInFuture());
    }

    [Fact]
    public void IsInFuture_WhenStartDateIsInPast_ReturnsFalse()
    {
        var range = new DateRange(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(-1));

        Assert.False(range.IsInFuture());
    }

    [Fact]
    public void IsInPast_WhenEndDateIsInPast_ReturnsTrue()
    {
        var range = new DateRange(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(-1));

        Assert.True(range.IsInPast());
    }

    [Fact]
    public void IsInPast_WhenEndDateIsInFuture_ReturnsFalse()
    {
        var range = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3));

        Assert.False(range.IsInPast());
    }
}
