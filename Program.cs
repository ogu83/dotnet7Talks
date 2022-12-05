using dotnet7Talks.Helpers;
using dotnet7Talks.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddTransient<IPersonService, PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGet("/person/get", () =>
{
    var personService = app.Services.GetService<IPersonService>();
    if (personService != null)
    {
        var retval = personService.GetPerson();
        return Results.Ok(retval.ToString());
    }
    else
        return Results.NotFound("Person Service Not Found");
});

app.MapGet("/person/getbio", () =>
{
    var personService = app.Services.GetService<IPersonService>();
    if (personService != null)
    {
        var bio = personService.GetPersonBiography();
        bio = bio.ToHtml();
        var retval = Results.Text(bio, "text/html", System.Text.Encoding.UTF8);
        return (retval);
    }
    else
        return Results.NotFound("Person Service Not Found");
});

app.MapGet("/person/getmath", () =>
{
    var myValues = new float[] { 1f, 2f, 3f, 4f, 5f, };
    var sum = MathExt.Sum<float, float>(myValues);
    var avg = MathExt.Average<float, float>(myValues);
    var std = MathExt.StandardDeviation<float, float>(myValues);
    var retval = $$"""
    {
       "sum" : {{sum}},
       "avg" : {{avg}},
       "std" : {{std}}
    }
    """;
    return Results.Ok(retval);
});

app.Run();
