using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IntegracaoVendas.Dominio.Logger
{
    
    public class ColoredConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly ColoredConsoleLoggerConfiguration _config;

        public ColoredConsoleLogger(string name, ColoredConsoleLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = _config.Color;
                Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {LogLevelConverter(logLevel)} {formatter(state, exception)}");
                Console.ForegroundColor = color;
            }
        }

        private string LogLevelConverter(LogLevel logLevel) => logLevel switch
        {
            LogLevel.Error => "Erro:",
            LogLevel.Warning => "Sucesso:",
            LogLevel.Information => "Informação:",
            LogLevel.Critical => "Informação:"
        };


    }
}
