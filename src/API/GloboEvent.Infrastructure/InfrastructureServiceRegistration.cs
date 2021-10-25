using GloboEvent.Application.Contrats.Infrastructure;
using GloboEvent.Application.Model.Mail;
using GloboEvent.Infrastructure.FileExport;
using GloboEvent.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICsvExporterService, CsvExporterService>();
            return services;
        }
    }
}
