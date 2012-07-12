#region Namespaces

using System;
using System.Globalization;
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
            return (dateToFormat != null) ? string.Format(ConfigHelper.DateTimeFormatForString, dateToFormat) : null;
        }

        /// <summary>
        /// Method to format string to DateTime object.
        /// </summary>
        /// <param name="stringDateToFormat">String to be converted to DateTime.</param>
        /// <returns>DateTime converted from string.</returns>
        public static DateTime? FormatStringToDate(string stringDateToFormat)
        {
            DateTime? dateTime = null;
 
            if (stringDateToFormat != null)
            {
                dateTime = DateTime.ParseExact(stringDateToFormat, ConfigHelper.DateTimeFormat,
                                               CultureInfo.CurrentCulture);
            }

            return dateTime;
        }

        /// <summary>
        /// Changes DateTime to format as defined in the configuration file.
        /// </summary>
        /// <param name="dateTimeToConvert">DateTime in UTC format.</param>
        /// <returns>DateTime in format defined in configuration file.</returns>
        public static DateTime? GetDateTimeInLocalFormat(DateTime? dateTimeToConvert)
        {
            DateTime? localTime = null;

            if (dateTimeToConvert != null)
            {
                localTime = TimeZone.CurrentTimeZone.ToLocalTime(dateTimeToConvert.Value);
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
            DateTime? utcTime = null;

            if(dateTimeToConvert != null)
            {
                utcTime = dateTimeToConvert.Value.ToUniversalTime();
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
