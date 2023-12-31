using EduArk.Infrastructure.Master.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEduArkMasterApplicationServices();
builder.Services.AddEduArkInfrastructureMasterServices(builder.Configuration);
builder.Services.AddEduArkMasterWebAPIServices(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<MasterDbContextInitialiser>();

    await initialiser.InitialiseAsync();

    await initialiser.SeedAsync();
}


app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
