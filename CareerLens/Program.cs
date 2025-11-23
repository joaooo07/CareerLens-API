using CareerLens.Application.Interfaces.AnalysisResults;
using CareerLens.Application.Interfaces.JobAnalyses;
using CareerLens.Application.Interfaces.LearningResources;
using CareerLens.Application.Interfaces.Resumes;
using CareerLens.Application.Interfaces.ResumeSkills;
using CareerLens.Application.Interfaces.Skills;
using CareerLens.Application.Interfaces.Users;
using CareerLens.Application.UseCases.AnalysisResults;
using CareerLens.Application.UseCases.JobAnalyses;
using CareerLens.Application.UseCases.LearningResources;
using CareerLens.Application.UseCases.Resumes;
using CareerLens.Application.UseCases.ResumeSkills;
using CareerLens.Application.UseCases.Skills;
using CareerLens.Application.UseCases.Users;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using CareerLens.Infrastructure.Data.Repositories;

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserUseCase, UserUseCase>();

builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IResumeUseCase, ResumeUseCase>();

builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ISkillUseCase, SkillUseCase>();

builder.Services.AddScoped<ILearningResourceRepository, LearningResourceRepository>();
builder.Services.AddScoped<ILearningResourceUseCase, LearningResourceUseCase>();

builder.Services.AddScoped<IResumeSkillRepository, ResumeSkillRepository>();
builder.Services.AddScoped<IResumeSkillUseCase, ResumeSkillUseCase>();

builder.Services.AddScoped<IJobAnalysisRepository, JobAnalysisRepository>();
builder.Services.AddScoped<IJobAnalysisUseCase, JobAnalysisUseCase>();

builder.Services.AddScoped<IAnalysisResultRepository, AnalysisResultRepository>();
builder.Services.AddScoped<IAnalysisResultUseCase, AnalysisResultUseCase>();

builder.Services.AddControllers();


builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V"; 
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CareerLens API v1",
        Version = "v1"
    });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CareerLens API v2",
        Version = "v2"
    });
});


builder.Services.AddHealthChecks()
    .AddOracle(
        builder.Configuration.GetConnectionString("Oracle")!,
        name: "oracle",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: new[] { "db", "oracle" });


builder.Services.AddOpenTelemetry()
    .WithTracing(tracer =>
    {
        tracer
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CareerLens.API"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CareerLens V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "CareerLens V2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/health/db", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("db"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.MapControllers();

app.Run();
