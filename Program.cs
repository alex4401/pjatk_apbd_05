namespace APBD4;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<Database>();
        builder.Services.AddControllers();
        
        var app = builder.Build();
        app.UseDeveloperExceptionPage();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}