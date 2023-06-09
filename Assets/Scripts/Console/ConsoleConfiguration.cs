using System.Collections.Generic;

public static class ConsoleConfiguration
{
    // For each log category, we set to what level the logs should be shown.
    public static Dictionary<LogCategory, LogDisplaySetting> LogCategories = new Dictionary<LogCategory, LogDisplaySetting>()
    {
        {  LogCategory.Data, LogDisplaySetting.Medium },
        {  LogCategory.Events, LogDisplaySetting.Medium },
        {  LogCategory.General, LogDisplaySetting.Medium },
        {  LogCategory.Initialisation, LogDisplaySetting.Medium }
    };
}