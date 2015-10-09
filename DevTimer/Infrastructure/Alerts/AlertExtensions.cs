﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace DevTimer.Infrastructure.Alerts
{
    public static class AlertExtensions
    {
        private const string Alerts = "_Alerts";

        public static ICollection<Alert> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Alerts))
            {
                tempData[Alerts] = new List<Alert>();
            }

            return (List<Alert>) tempData[Alerts];
        }

        public static ActionResult WithSuccess(this ActionResult result, string message)
        {
            return new AlertDecoratorResult(result, "alert-success", message);
        }

        public static ActionResult WithInfo(this ActionResult result, string message)
        {
            return new AlertDecoratorResult(result, "alert-info", message);
        }

        public static ActionResult WithWarning(this ActionResult result, string message)
		{
			return new AlertDecoratorResult(result, "alert-warning", message);
		}

		public static ActionResult WithError(this ActionResult result, string message)
		{
			return new AlertDecoratorResult(result, "alert-danger", message);
		}
    }
}