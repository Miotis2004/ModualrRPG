using System.Collections.Generic;
using System.Linq;

namespace ModularRPG.Core
{
    public enum RPGValidationSeverity
    {
        Info,
        Warning,
        Error
    }

    public sealed class RPGValidationMessage
    {
        public RPGValidationMessage(RPGValidationSeverity severity, string message)
        {
            Severity = severity;
            Message = message;
        }

        public RPGValidationSeverity Severity { get; }
        public string Message { get; }
    }

    public sealed class RPGValidationReport
    {
        private readonly List<RPGValidationMessage> messages = new List<RPGValidationMessage>();
        public IReadOnlyList<RPGValidationMessage> Messages => messages;
        public bool IsValid => messages.All(message => message.Severity != RPGValidationSeverity.Error);
        public void Add(RPGValidationSeverity severity, string message) => messages.Add(new RPGValidationMessage(severity, message));
    }

    public static class RPGCoreValidator
    {
        public static RPGValidationReport ValidateRegistry(RPGSystemRegistry registry)
        {
            RPGValidationReport report = new RPGValidationReport();
            if (registry == null)
            {
                report.Add(RPGValidationSeverity.Error, "System registry is missing.");
                return report;
            }

            HashSet<string> ids = new HashSet<string>();
            foreach (IRPGSystem system in registry.Systems)
            {
                if (string.IsNullOrWhiteSpace(system.SystemId))
                {
                    report.Add(RPGValidationSeverity.Error, "A registered system has an empty SystemId.");
                }
                else if (!ids.Add(system.SystemId))
                {
                    report.Add(RPGValidationSeverity.Error, $"Duplicate SystemId '{system.SystemId}' detected.");
                }
            }

            if (report.Messages.Count == 0)
            {
                report.Add(RPGValidationSeverity.Info, "Core registry validation passed.");
            }

            return report;
        }
    }
}
