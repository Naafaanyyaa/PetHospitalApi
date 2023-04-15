﻿using PetHospital.Business.Exceptions;
using ILogger = Serilog.ILogger;

namespace PetHospital.Api.Middleware;

public class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger logger)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            logger.Information(exception, "Validation exception is occured");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (NotFoundException exception)
        {
            logger.Information(exception, "Resource not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        catch (Exception exception)
        {
            HandleStatus500Exception(context, exception, logger);
        }
    }

    private void HandleStatus500Exception(HttpContext context, Exception exception, ILogger logger)
    {

        logger.Error(exception, "An exception was thrown as a result of the request");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }
}