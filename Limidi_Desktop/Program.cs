using Limidi_Desktop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isUsingVirtualMidiSdk = true; // License dependent for final release
if (isUsingVirtualMidiSdk) builder.Services.AddSingleton<IMidiEventSender, TeVirtualMidiEventSender>();
else builder.Services.AddSingleton<IMidiEventSender, LegacyMidiEventSender>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
