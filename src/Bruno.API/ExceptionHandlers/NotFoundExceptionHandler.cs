using Bruno.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Bruno.API.ExceptionHandlers;

public sealed class NotFoundExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		if (exception is not NotFoundException notFoundException)
			return false;

		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status404NotFound,
			Title = "Resource not found.",
			Detail = notFoundException.Message
		};

		httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}
