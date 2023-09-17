using Limidi_Desktop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
TODO: Make this OS Dependant instead of 
*/
var isWindows = true;
if (isWindows) builder.Services.AddSingleton<IMidiEventSender, TeVirtualMidiEventSender>();
else /*MacOS*/ builder.Services.AddSingleton<IMidiEventSender, CoreMidiMidiEventSender>();



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
