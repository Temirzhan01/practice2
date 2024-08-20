using Serilog.Formatting.Elasticsearch;
using Serilog;
using Template.Extensions.Configuration;
using Template.Extensions.MiddleWares;
using Template.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

Serilog.Debugging.SelfLog.Enable(Console.Error);

Log.Logger = new LoggerConfiguration() // Настраиваем глобальный логгер проекта Serilog, с выводом в консоль логов, читаем для Еластика формате
    .Enrich.FromLogContext()
    .WriteTo.Console(new ElasticsearchJsonFormatter())
    .CreateLogger();

builder.Host.UseSerilog();

builder.AddConsulConfiguration(); // Получаем конфигурации проекта из консула, при первичном запуске проекта

builder.AddServices(); // Внедряем все зависимости проекта

var app = builder.Build();

app.MapHealthChecks("/healthz", new HealthCheckOptions // Добавляем entrypoint для получения хелзчека работы приложения, и настраиваем для вывода в едином формате 
{
    ResponseWriter = async (context, report) => {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new { name = x.Key, response = x.Value.Status.ToString(), description = x.Value.Description })
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
});

if (builder.Configuration["SwaggerEnvironment"] == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>(); // Используем кастомный компонент middleware для аутентификации на основе апи ключа

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.Run();


namespace Template.Extensions.Configuration
{
    public static class ConsulConfigurationExtensions
    {
        public static WebApplicationBuilder AddConsulConfiguration(this WebApplicationBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            var builtConfig = configBuilder.Build();
            configBuilder.Add(new ConsulConfigurationSource(builtConfig["Host"], builtConfig["Environment"]));

            var config = configBuilder.Build();
            builder.Configuration.AddConfiguration(config);

            return builder;
        }
    }

    public class ConsulConfigurationSource : IConfigurationSource
    {
        private readonly string _consulAddress;
        private readonly string _key;

        public ConsulConfigurationSource(string consulAddress, string key)
        {
            _consulAddress = consulAddress;
            _key = key;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(_consulAddress, _key);
        }
    }
}


using Consul;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Template.Extensions.Configuration
{
    public class ConsulConfigurationProvider : ConfigurationProvider
    {
        private readonly string _consulAddress;
        private readonly string _key;
        private readonly ILogger<ConsulConfigurationProvider> _logger; // хочу тут использовать логгер, но тут внедрение не работает, так как я явно создаю объект данного класса через конструктор

        public ConsulConfigurationProvider(string consulAddress, string key, ILogger<ConsulConfigurationProvider> logger)
        {
            _consulAddress = consulAddress;
            _key = key;
            _logger = logger;
        }

        public override void Load()
        {
            try 
            {
                var client = new ConsulClient(config =>
                {
                    config.Address = new Uri(_consulAddress);
                });

                Task<Dictionary<string, string>> getConsulData = GetConsulData(client, _key);
                getConsulData.Wait();

                foreach (var kvp in getConsulData.Result)
                {
                    LoadJsonConfiguration(kvp.Value);
                }
            } 
            catch (Exception ex)
            {
                string Action = "LoadConsulConfiguration";
                _logger.LogError($"Action: {Action} Error message: {ex.Message}");
            }

        }

        private async Task<Dictionary<string, string>> GetConsulData(ConsulClient client, string key)
        {
            var result = new Dictionary<string, string>();
            var queryResult = await client.KV.Get(key);

            if (queryResult.Response != null)
            {
                string consulValue = Encoding.UTF8.GetString(queryResult.Response.Value);
                result[key] = consulValue;
            }

            return result;
        }

        private void LoadJsonConfiguration(string json)
        {
            var jObject = JObject.Parse(json);
            foreach (var kvp in jObject)
            {
                ParseJson(kvp, string.Empty);
            }
        }

        private void ParseJson(KeyValuePair<string, JToken> kvp, string parentPath)
        {
            var currentPath = parentPath == string.Empty ? kvp.Key : $"{parentPath}:{kvp.Key}";

            if (kvp.Value.Type == JTokenType.Object)
            {
                foreach (var childKvp in (JObject)kvp.Value)
                {
                    ParseJson(childKvp, currentPath);
                }
            }
            else
            {
                Data[currentPath] = kvp.Value.ToString();
            }
        }
    }
}


Как бы мне прологировать в случае ошибки? 
