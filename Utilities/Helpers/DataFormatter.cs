#region Namespaces

using System;
using System.Web.Script.Serialization;

#endregion Namespaces

namespace Helpers
{
    /// <summary>
    /// Class to deal with all kinds of data formatting.
    /// </summary>
    public class DataFormatter
    {
        #region Public Methods

        /// <summary>
        /// Method to format date into configured format.
        /// </summary>
        /// <param name="dateToFormat">Date to be formatted.</param>
        /// <returns>Formatted date in form of string.</returns>
        public static string FormatDateToString(DateTime? dateToFormat)
        {
            return (dateToFormat != null) ? string.Format(ConfigHelper.DateTimeFormat, dateToFormat) : null;
        }

        /// <summary>
        /// Changes DateTime to format as defined in the configuration file.
        /// </summary>
        /// <param name="dateTimeToConvert">DateTime in UTC format.</param>
        /// <returns>DateTime in format defined in configuration file.</returns>
        public static DateTime? GetDateTimeInLocalFormat(DateTime? dateTimeToConvert)
        {
            DateTime? localTime;

            if (dateTimeToConvert != null)
            {
                localTime = TimeZone.CurrentTimeZone.ToLocalTime(dateTimeToConvert.Value);
            }
            else
            {
                localTime = null;
            }

            return localTime;
        }

        /// <summary>
        /// Changes DateTime to Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="dateTimeToConvert">DateTime to be formatted to UTC.</param>
        /// <returns>DateTime in UTC format.</returns>
        public static DateTime? GetDateTimeInUtcFormat(DateTime? dateTimeToConvert)
        {
            DateTime? utcTime;

            if(dateTimeToConvert != null)
            {
                utcTime = dateTimeToConvert.Value.ToUniversalTime();
            }
            else
            {
                utcTime = null;
            }

            return utcTime;
        }

        public static string SerializeToJson(object objectToSerialize)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(objectToSerialize);
        }

        #endregion Public Methods
    }
}
