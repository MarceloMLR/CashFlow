using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace CashFlow.Api.Middlewares;

public class CultureMiddlewarecs
{
    private readonly RequestDelegate _next;
    public CultureMiddlewarecs(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var supportedLanguagues = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestCulture) == false 
            && supportedLanguagues.Exists(lang => lang.Name == requestCulture))
        {
            cultureInfo = new CultureInfo(requestCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);   

    }
}
