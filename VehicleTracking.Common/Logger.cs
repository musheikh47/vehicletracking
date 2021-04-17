using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common
{
    public static class Logger
    {
        // Impementation of logging can be changed later without modifying the core logic.
        public static void LogDebug(string message)
        {
#if DEBUG
            Console.WriteLine($"DEBUG: {DateTime.Now} {message}");
#endif
        }
        public static void LogError(Exception ex, string message)
        {
            var logString = $"ERROR: {DateTime.Now} {message} {Environment.NewLine} {ex?.Message} {Environment.NewLine} {ex?.StackTrace} ";
            Console.WriteLine(logString);
        }
    }
}
