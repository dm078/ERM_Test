using System.Diagnostics;

namespace Utilities
{
    class EventLogger
    {
        /// <summary>
        /// Log either a standard event log or an Error log based on the provided parameters.
        /// </summary>
        /// <param name="messageToLog"></param>
        /// <param name="errorLog"></param>
        public void LogMessage(string messageToLog,bool errorLog = false)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";

                if (errorLog)
                {
                    eventLog.WriteEntry(messageToLog, EventLogEntryType.Error, 101, 1);
                }
                else
                {
                    eventLog.WriteEntry(messageToLog, EventLogEntryType.Information, 101, 1);
                }
            }
        }
    }
}
