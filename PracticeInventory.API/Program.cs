using PracticeInventory.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerService();
builder.Services.AddPresentation();
builder.Services.AddIdentity();
builder.Services.AddJWTAuthentication();
builder.Services.AddAuthorizationPolicy();
builder.Services.AddDBContext(builder.Configuration);
builder.Services.AddDapperConnection();

var app = builder.Build();

Task<IApplicationBuilder> task = app.AddDbMigration();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();