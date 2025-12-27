using Microsoft.EntityFrameworkCore;
using FreshDeskAPI;


var builder = WebApplication.CreateBuilder(args);


// 👇👇👇 YEH CODE ADD KARO (DB Context ke neeche kahin bhi) 👇👇👇
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
}); 




// 👇 2. YEH LINE ADD KARNI HAI (Database Service)
// 👇 2. YEH WALI LINE ADD KARO (Sir ne yehi likhwaya hoga)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Step 1: Hotel ko batao ki hum Controllers use karenge
// 1. Services Add karo
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- NEW CODE: CORS (Bridge) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()                      // WithOrigins("http://localhost:4200")   //Angular app ka Address
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
//----------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();
//---New Code : Bridge ko ON karo ----
app.UseCors("AllowAll");

app.UseAuthorization();




// Step 2: Controllers ka raasta kholo
app.MapControllers();

app.Run();