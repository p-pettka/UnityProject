using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : Singleton<AnalyticsManager>
{
    public void SendEvent(string eventName, string parameterName, object parameterValue)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add(parameterName, parameterValue);
        SendEvent(eventName, parameters);
    }

    public void SendEvent(string eventName, Dictionary<string, object> parameters = null)
    {
        AnalyticsResult result;
        if (parameters == null)
        {
            result = AnalyticsEvent.Custom(eventName);
        }
        else
        {
            result = AnalyticsEvent.Custom(eventName, parameters);
        }
        Debug.LogFormat("Event {0} Result : {1}", eventName, result);
    }

    public int GetIntParameter(string name)
    {
        return RemoteSettings.GetInt(name, 0);
    }

    private void OnApplicationQuit()
    {
        TimeSpan time = TimeSpan.FromMilliseconds(AnalyticsSessionInfo.sessionElapsedTime);
        string parameterValue = time.ToString("g");
        SendEvent("SessionTime", "time", parameterValue);
    }
}
