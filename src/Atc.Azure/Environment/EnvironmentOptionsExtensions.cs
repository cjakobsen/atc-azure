using System;
using System.Diagnostics.CodeAnalysis;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Atc.Azure.Environment
{
    public static class EnvironmentOptionsExtensions
    {
        public static IConfigurationBuilder ConfigureKeyVault<TServiceOptions>(this IConfigurationBuilder config, string tenantId)
            where TServiceOptions : EnvironmentOptions, new()
        {
            var serviceOptions = config
                .Build()
                .GetServiceOptions<TServiceOptions>();

            return config.AddAzureKeyVault(
                serviceOptions.GetKeyVault(),
                new DefaultAzureCredential(
                    new DefaultAzureCredentialOptions
                    {
                        // We need to explicit specify the tenant id for this to work locally.
                        // Issue https://github.com/Azure/azure-sdk-for-net/issues/8957
                        SharedTokenCacheTenantId = tenantId,
                        VisualStudioTenantId = tenantId,
                        VisualStudioCodeTenantId = tenantId,
                    }));
        }

        public static string GetCosmosEndpoint(this EnvironmentOptions options)
            => options.EnvironmentType == EnvironmentType.Local
            ? "https://localhost:8081/"
            : $"https://{options.GetEnvResourceName()}.documents.azure.com:443/";

        public static Uri GetKeyVault(this EnvironmentOptions options)
            => new($"https://{options.GetResourceName()}.vault.azure.net/");

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "ByDesign")]
        public static string GetEnvResourceName(this EnvironmentOptions options, string? ext = null) => string.Concat(new[]
        {
            options.CompanyAbbreviation,
            options.SystemAbbreviation,
            options.EnvironmentName?.ToLowerInvariant() ?? string.Empty,
            ext ?? string.Empty,
        });

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "ByDesign")]
        public static string GetResourceName(this EnvironmentOptions options, string? ext = null) => string.Concat(new[]
        {
            options.CompanyAbbreviation,
            options.SystemAbbreviation,
            options.EnvironmentName?.ToLowerInvariant() ?? string.Empty,
            options.ServiceAbbreviation,
            ext ?? string.Empty,
        });

        public static TServiceOptions GetServiceOptions<TServiceOptions>(
            this IConfiguration config)
            where TServiceOptions : EnvironmentOptions, new()
        {
            var serviceOptions = new TServiceOptions();
            config
                .GetSection(serviceOptions.SectionName)
                .Bind(serviceOptions);
            return serviceOptions;
        }
    }
}
