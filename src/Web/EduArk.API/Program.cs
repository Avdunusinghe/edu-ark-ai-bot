using EduArk.API.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEduArkApplicationServices();

builder.Services.AddEduArkInfrastructureMasterServices(builder.Configuration);
builder.Services.AddEduArkInfrastructureTenantServices(builder.Configuration);
builder.Services.AddEduArkWebAPIServices(builder.Configuration);



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.WebHost.UseSentry();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

 //Initialise and seed database
/*using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<TenantDbContextInitialiser>();

    await initialiser.InitialiseAsync();

    await initialiser.SeedAsync();
}*/





app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<TenantSelectionMiddleware>();




app.Run();


