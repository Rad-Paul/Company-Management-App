using CompanyManagementAPI.Extensions;
using Contracts;
using CompanyManagementAPI.ActionFilters;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using CompanyManagementAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters()
  .AddCustomCSVFormatter();
builder.Services.AddSignalR();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CompanyManagementHub>("/hubs/companyManagementHub");

app.Run();

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
    .Services.BuildServiceProvider()
    .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
    .OfType<NewtonsoftJsonPatchInputFormatter>().First();
