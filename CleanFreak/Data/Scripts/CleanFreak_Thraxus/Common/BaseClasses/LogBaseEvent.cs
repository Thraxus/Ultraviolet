using Ultraviolet.CleanFreak_Thraxus.Common.DataTypes;

namespace Ultraviolet.CleanFreak_Thraxus.Common.BaseClasses
{
	public abstract class LogBaseEvent
	{
		public event TriggerLog OnWriteToLog;
		public string Id;
		public delegate void TriggerLog(string caller, string message, LogType logType);

		public void WriteToLog(string caller, string message, LogType logType)
		{
			OnWriteToLog?.Invoke(string.IsNullOrWhiteSpace(Id) ? caller : $"({Id}) {caller}", message, logType);
		}
	}
}
