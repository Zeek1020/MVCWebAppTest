using Microsoft.AspNetCore.CookiePolicy;
using System.Diagnostics;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("default", client => {
    client.BaseAddress = new Uri( @"https://localhost:7044" );
});

string secretsGUID = builder.Configuration.GetValue<string>("secretsGUID");

builder.Configuration.AddEnvironmentVariables().AddUserSecrets( secretsGUID );

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
