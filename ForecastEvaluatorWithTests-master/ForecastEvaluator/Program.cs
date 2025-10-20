using ForecastEvaluator.DataModels;
using ForecastEvaluator.Endpoints;
using ForecastEvaluator.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using ForecastEvaluator.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.RegisterServices();
var app = builder.Build();
app.RegisterMiddlewares();
RecurringJob.AddOrUpdate("AnemometerCheck", (IAnemometerService anemometerService) =>
anemometerService.UpdateAnemometer("ANEMOMETER"), Cron.Hourly);
RecurringJob.AddOrUpdate("IconUpdate", (IWeatherService weatherService) =>
weatherService.UpdateForecast("ICON"), "0 10 * * *");
RecurringJob.AddOrUpdate("FranceUpdate", (IWeatherService weatherService) =>
weatherService.UpdateForecast("METEO-FRANCE"), "0 10 * * *");
// Aplication insights co do logow w app
// Wzorzec strukturalny circut braker co do zatrzymania odpytywan jak niedostepna baza
app.Run();

// W��czy� code coverage - znale�� narz�dzie do sprawdzenia pokrycia test�w, przypadkowo nie podpi��
// projektu test�w, ciekawostka
// Wszystko co jest publiczne powinno by� przetestowane
//  - Dost�p do aplikacji, w moim przypadku endpointy
//  * Na tym poziomie bardziej testy integracyjne
//  * W naszym za�o�eniu je�li co� przetestowali�my jednostkowo to w integracyjnym nie 
// trzeba tego testowa� tak szczeg�owo
//  * W takim razie trzeba przetestowa�:
//  * Czy po podaniu poprawnych danych jest odpowied� 200
//  * W rozszerzonym produkcie testuj� te� walidacje, inne kody b��d�w

// Testy seriwsu, wszystkie publiczne metody musz� by� przetestowane, tutaj testy jednostkowe
// Ka�de rozga��zienie w metodzie ma sw�j test (np. wszystkie if)






// xunit testy, plus moq, plus inmemory baza danych, flueant assertion

// Co dalej?
// 