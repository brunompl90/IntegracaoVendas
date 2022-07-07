using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IntegracaoVendas.Dominio.Logger
{
    public class ColoredConsoleLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
        public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
    }
}
