using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;

namespace cat
{
    public class CustomTelemetryInitializer : ITelemetryInitializer
    {
        // solves an issue with localhost:<port> instances in debug
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; ;
        }
    }
}
