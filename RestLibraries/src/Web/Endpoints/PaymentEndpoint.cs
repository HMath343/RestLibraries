namespace RestLibraries.Web.Endpoints;

using MediatR;
using Microsoft.AspNetCore.Http;

using RestLibraries.Application.Payments;

public static class PaymentEndpoints
{    
    public static void AddPaymentEndpoints(this WebApplication app)
    {
        app.MapPost("httpclient/payment", SavePaymentHttpClient);
        app.MapPost("refit/payment", SavePaymentRefit);
        app.MapPost("restsharp/payment", SavePaymentRestsharp);

    } 

    private static async Task<IResult> SavePaymentHttpClient(AddPaymentHttpClientCommand payment, IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(payment);
            return Results.Ok(response);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    private static async Task<IResult> SavePaymentRefit(AddPaymentRefitCommand payment, IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(payment);
            return Results.Ok(response);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    private static async Task<IResult> SavePaymentRestsharp(AddPaymentRestsharpCommand payment, IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(payment);
            return Results.Ok(response);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }
}