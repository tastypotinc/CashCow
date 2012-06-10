#region Namespaces

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion Namespaces

namespace CashCow.Provider.Utility
{
    /// <summary>
    /// SQL Utility class for the AirWatch application
    /// </summary>
    public static class SqlUtil
    {
        #region Parameter manipulation utilities

        /// <summary>
        ///     Return value parameter
        /// </summary>
        public const string RETURN_VALUE = "@RETURN_VALUE";

        /// <summary>
        ///     Creates a return parameter and inserts into the command list
        /// </summary>
        /// <param name="db">The database object</param>
        /// <param name="cmd">The command object</param>
        /// <remarks>The DBCommand parameter argument that is modified</remarks>
        public static void CreateReturnParameter(Database db, ref DbCommand cmd)
        {
            #region Defensive
		    
            #region db
		    
            Debug.Assert(db != null, "The argument supplied for parameter 'db' should not be null");

            if(db == null)
            {
                throw new ArgumentNullException("db", "The argument supplied for parameter 'db' should not be null");
            }

	        #endregion

            #region cmd
		    
            Debug.Assert(cmd != null, "The argument supplied for parameter 'cmd' should not be null");

            if(cmd == null)
            {
                throw new ArgumentNullException("cmd", "The argument supplied for parameter 'cmd' should not be null");
            }

	        #endregion

	        #endregion

            db.AddParameter(cmd, RETURN_VALUE, DbType.Int32, sizeof(int), ParameterDirection.ReturnValue,
                    false, 0, 0, string.Empty, DataRowVersion.Default, null);
        }

        #endregion

        #region Type conversion utilities

        /// <summary>
        /// Checks to see if the parameter is a SQL data type
        /// </summary>
        /// <param name="dataValue">The object parameter</param>
        /// <returns>A boolean value indicating if the parameter is a SQL data type or not</returns>
        private static bool _IsTypeSqlParameter(object dataValue)
        {
            if (dataValue == null)
                return false;

            bool retVal = dataValue.GetType() == typeof(SqlParameter);
            
            return retVal;
        }

        #region ushort?

        /// <summary>
        /// Converts the input object parameter into a nullable <c>ushort</c> value.
        /// </summary>
        /// <param name="dataValue">The input object parameter.</param>
        /// <param name="defaultValue">The nullable output <c>ushort</c> value parameter.</param>
        /// <returns>A nullable <c>ushort</c> value.</returns>
        public static ushort? SetValue(object dataValue, ushort? defaultValue)
        {
            return _IsTypeSqlParameter(dataValue)
                       ? ConvertToUShortValue(((SqlParameter) dataValue).Value, defaultValue)
                       : ConvertToUShortValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a nullable <c>ushort</c> instance to a vlaue suitable for assignment to a <c>SqlParameter</c>.
        /// </summary>
        /// <param name="value">The nullable <c>ushort</c> instance to be</param>
        /// <returns>The converted nullable <c>ushort</c> as an <c>object</c> type.</returns>
        /// <remarks></remarks>
        public static object ParameterValue(ushort? value)
        {
            object ret = value;
            if (value == null || ushort.MinValue == value)
            {
                ret = DBNull.Value;
            }

            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a nullable <c>ushort</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The nullable output <c>ushort</c> value parameter</param>
        /// <returns>A nullable <c>ushort</c> value</returns>
        private static ushort? ConvertToUShortValue(object val, ushort? defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToUInt16(val);
        }

        #endregion

        #region long

        /// <summary>
        /// Converts the input object parameter into a <c>long</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>long</c> value parameter</param>
        /// <returns>A <c>long</c> value</returns>
        public static long SetValue(object dataValue, long defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToInt64Value(((SqlParameter)dataValue).Value, defaultValue) : ConvertToInt64Value(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>long</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>long</c> instance to be converted</param>
        /// <returns>The converted <c>long</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(long value)
        {
            object ret = value;
            if (long.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>long</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>long</c> value parameter</param>
        /// <returns>A <c>long</c> value</returns>
        private static long ConvertToInt64Value(object val, long defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToInt64(val);
        }

        #endregion

        #region long?

        /// <summary>
        /// Converts the input object parameter into a nullable <c>long</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The nullable output <c>int</c> value parameter</param>
        /// <returns>A nullable <c>int</c> value</returns>
        public static long? SetValue(object dataValue, long? defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToInt64Value(((SqlParameter)dataValue).Value, defaultValue) : ConvertToInt64Value(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a nullable <c>long</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The nullable <c>long</c> instance to be converted</param>
        /// <returns>The converted nullable <c>long</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(long? value)
        {
            object ret = value;
            if (value == null || long.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a nullable <c>long</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The nullable output <c>int</c> value parameter</param>
        /// <returns>A nullable <c>long</c> value</returns>
        private static long? ConvertToInt64Value(object val, long? defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToInt64(val);
        }

        #endregion

        #region string

        /// <summary>
        /// Converts the input object parameter into a <c>string</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>string</c> value parameter</param>
        /// <returns>A <c>string</c> value</returns>
        public static string SetValue(object dataValue, string defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToStringValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToStringValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>string</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>string</c> instance to be converted</param>
        /// <returns>The converted <c>string</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(string value)
        {
            object ret = value;
            if (string.IsNullOrEmpty(value))
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Converts a <c>string</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>string</c> instance to be converted</param>
        /// <param name="convertEmptyStringToDbNull">Specifies converting empty string to dbnull.</param>
        /// <returns>The converted <c>string</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(string value, bool convertEmptyStringToDbNull)
        {
            object ret = value;
            
            if ( value == null || ( convertEmptyStringToDbNull && string.IsNullOrEmpty(value)))
            {
                ret = DBNull.Value;
            }

            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>string</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>string</c> value parameter</param>
        /// <returns>A <c>string</c> value</returns>
        private static string ConvertToStringValue(object val, string defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToString(val);
        }


        #endregion

        #region int

        /// <summary>
        /// Converts the input object parameter into a <c>int</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>int</c> value parameter</param>
        /// <returns>An <c>int</c> value</returns>
        public static int SetValue(object dataValue, int defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToInt32Value(((SqlParameter)dataValue).Value, defaultValue) : ConvertToInt32Value(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>int</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>int</c> instance to be converted</param>
        /// <returns>The converted <c>int</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(int value)
        {
            object ret = value;
            if (int.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>int</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>int</c> value parameter</param>
        /// <returns>An <c>int</c> value</returns>
        private static int ConvertToInt32Value(object val, int defaultTo)
        {
            int retVal;

            if (val == null || val == DBNull.Value || !int.TryParse(val.ToString(), out retVal))
            {
                retVal = defaultTo;
            }

            return retVal;
        }


        #endregion

        #region int?

        /// <summary>
        /// Converts the input object parameter into a nullable <c>int</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The nullable output <c>int</c> value parameter</param>
        /// <returns>A nullable <c>int</c> value</returns>
        public static int? SetValue(object dataValue, int? defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToInt32Value(((SqlParameter)dataValue).Value, defaultValue) : ConvertToInt32Value(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a nullable <c>int</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The nullable <c>int</c> instance to be converted</param>
        /// <returns>The converted nullable <c>int</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(int? value)
        {
            object ret = value;
            if (value == null || int.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a nullable <c>int</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The nullable output <c>int</c> value parameter</param>
        /// <returns>A nullable <c>int</c> value</returns>
        private static int? ConvertToInt32Value(object val, int? defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToInt32(val);
        }

        #endregion

        #region short

        /// <summary>
        /// Converts the input object parameter into a <see cref="short"/> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <see cref="short"/> value parameter</param>
        /// <returns>An <see cref="short"/> value</returns>
        public static short SetValue(object dataValue, short defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToShortValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToShortValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <see cref="short"/> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <see cref="short"/> instance to be converted</param>
        /// <returns>The converted <see cref="short"/> as an <see cref="object"/> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(short value)
        {
            object ret = value;
            if (short.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <see cref="short"/> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <see cref="short"/> value parameter</param>
        /// <returns>An <see cref="short"/> value</returns>
        private static short ConvertToShortValue(object val, short defaultTo)
        {
            short retVal;

            if (val == null || val == DBNull.Value || !short.TryParse(val.ToString(), out retVal))
            {
                retVal = defaultTo;
            }

            return retVal;
        }


        #endregion

        #region uint

        /// <summary>
        /// Converts the input object parameter into a <c>uint</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>uint</c> value parameter</param>
        /// <returns>A <c>uint</c> value</returns>
        public static uint SetValue(object dataValue, uint defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToUIntValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToUIntValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>uint</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>uint</c> instance to be converted</param>
        /// <returns>The converted <c>uint</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(uint value)
        {
            object ret = value;
            if (uint.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>uint</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>uint</c> value parameter</param>
        /// <returns>A <c>uint</c> value</returns>
        private static uint ConvertToUIntValue(object val, uint defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToUInt32(val);
        }


        #endregion

        #region decimal

        /// <summary>
        /// Converts the input object parameter into a <c>decimal</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>decimal</c> value parameter</param>
        /// <returns>A <c>decimal</c> value</returns>
        public static decimal SetValue(object dataValue, decimal defaultValue)
        {
            decimal decRetVal = _IsTypeSqlParameter(dataValue) ? ConvertToDecimalValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToDecimalValue(dataValue, defaultValue);

            return decRetVal;
        }

        /// <summary>
        /// Converts a <c>decimal</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>decimal</c> instance to be converted</param>
        /// <returns>The converted <c>decimal</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(decimal value)
        {
            object ret = value;
            if (decimal.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>decimal</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>decimal</c> value parameter</param>
        /// <returns>A <c>decimal</c> value</returns>
        private static decimal ConvertToDecimalValue(object val, decimal defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToDecimal(val);
        }


        #endregion

        #region decimal?

        /// <summary>
        /// Converts the input object parameter into a nullable <c>decimal</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The nullable output <c>decimal</c> value parameter</param>
        /// <returns>A nullable <c>decimal</c> value</returns>
        public static decimal? SetValue(object dataValue, decimal? defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToDecimalValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToDecimalValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a nullable <c>decimal</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The nullable <c>decimal</c> instance to be converted</param>
        /// <returns>The converted nullable <c>decimal</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(decimal? value)
        {
            object ret = value;
            if (value == null || int.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a nullable <c>decimal</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The nullable output <c>decimal</c> value parameter</param>
        /// <returns>A nullable <c>decimal</c> value</returns>
        private static decimal? ConvertToDecimalValue(object val, decimal? defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToDecimal(val);
        }

        #endregion

        #region float

        /// <summary>
        /// Converts the input object parameter into a <c>double</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>double</c> value parameter</param>
        /// <returns>A <c>double</c> value</returns>
        public static float SetValue(object dataValue, float defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToFloatValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToFloatValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>float</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>float</c> instance to be converted</param>
        /// <returns>The converted <c>float</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(float value)
        {
            object ret = value;
            if (float.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>double</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>double</c> value parameter</param>
        /// <returns>A <c>double</c> value</returns>
        private static float ConvertToFloatValue(object val, float defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : (float)val;
        }

        #endregion

        #region double

        /// <summary>
        /// Converts the input object parameter into a <c>double</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>double</c> value parameter</param>
        /// <returns>A <c>double</c> value</returns>
        public static double SetValue(object dataValue, double defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToDoubleValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToDoubleValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>double</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>double</c> instance to be converted</param>
        /// <returns>The converted <c>double</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(double value)
        {
            object ret = value;
            if (double.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>double</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>double</c> value parameter</param>
        /// <returns>A <c>double</c> value</returns>
        private static double ConvertToDoubleValue(object val, double defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToDouble(val);
        }

        #endregion

        #region ushort

        /// <summary>
        /// Converts the input object parameter into a <c>ushort</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>ushort</c> value parameter</param>
        /// <returns>A <c>ushort</c> value</returns>
        public static double SetValue(object dataValue, ushort defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToUShortValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToUShortValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <see cref="ushort"/> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <see cref="ushort"/> instance to be converted</param>
        /// <returns>The converted <see cref="ushort"/> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(ushort value)
        {
            object ret = value;
            if (ushort.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>double</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>ushort</c> value parameter</param>
        /// <returns>A <c>double</c> value</returns>
        private static double ConvertToUShortValue(object val, ushort defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToUInt32(val);
        }


        #endregion

        #region byte

        /// <summary>
        /// Converts the input object parameter into a <c>byte</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>byte</c> value parameter</param>
        /// <returns>A <c>byte</c> value</returns>
        public static byte SetValue(object dataValue, byte defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToByteValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToByteValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>byte</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>byte</c> instance to be converted</param>
        /// <returns>The converted <c>byte</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(byte value)
        {
            object ret = value;
            if (byte.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>byte</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>byte</c> value parameter</param>
        /// <returns>A <c>byte</c> value</returns>
        private static byte ConvertToByteValue(object val, byte defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToByte(val);
        }


        #endregion

        #region DateTime

        /// <summary>
        /// Converts the input object parameter into a <c>DateTime</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>DateTime</c> value parameter</param>
        /// <returns>A <c>DateTime</c> value</returns>
        public static DateTime SetValue(object dataValue, DateTime defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToDateTimeValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToDateTimeValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>DateTime</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>DateTime</c> instance to be converted</param>
        /// <returns>The converted <c>DateTime</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(DateTime value)
        {
            object ret = value;
            if (DateTime.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>DateTime</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>DateTime</c> value parameter</param>
        /// <returns>A <c>DateTime</c> value</returns>
        private static DateTime ConvertToDateTimeValue(object val, DateTime defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToDateTime(val);
        }


        #endregion

        #region DateTime?

        /// <summary>
        /// Converts the input object parameter into a nullable <c>DateTime</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The nullable output <c>DateTime</c> value parameter</param>
        /// <returns>A nullable <c>DateTime</c> value</returns>
        public static DateTime? SetValue(object dataValue, DateTime? defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToDateTimeValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToDateTimeValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a nullable <c>DateTime</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The nullable <c>DateTime</c> instance to be converted</param>
        /// <returns>The converted nullable <c>DateTime</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(DateTime? value)
        {
            object ret = value;
            if (value == null || DateTime.MinValue == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a nullable <c>DateTime</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The nullable output <c>DateTime</c> value parameter</param>
        /// <returns>A nullable <c>DateTime</c> value</returns>
        private static DateTime? ConvertToDateTimeValue(object val, DateTime? defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToDateTime(val);
        }

        #endregion

        #region bool

        /// <summary>
        /// Converts the input object parameter into a <c>bool</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>bool</c> value parameter</param>
        /// <returns>A <c>bool</c> value</returns>
        public static bool SetValue(object dataValue, bool defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToBoolValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToBoolValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>bool</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>bool</c> instance to be converted</param>
        /// <returns>The converted <c>bool</c> as an <c>object</c> type</returns>
        public static object ParameterValue(bool value)
        {
            return value;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>bool</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>bool</c> value parameter</param>
        /// <returns>A <c>bool</c> value</returns>
        private static bool ConvertToBoolValue(object val, bool defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToBoolean(val);
        }


        #endregion

        #region bool?

        /// <summary>
        /// Converts the input object parameter into a nullable <c>bool</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The nullable output <c>bool</c> value parameter</param>
        /// <returns>A nullable <c>bool</c> value</returns>
        public static bool? SetValue(object dataValue, bool? defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToBoolValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToBoolValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a nullable <c>bool</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The nullable <c>bool</c> instance to be converted</param>
        /// <returns>The converted nullable <c>bool</c> as an <c>object</c> type</returns>
        public static object ParameterValue(bool? value)
        {
            return value;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a nullable <c>bool</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The nullable output <c>bool</c> value parameter</param>
        /// <returns>A nullable <c>bool</c> value</returns>
        private static bool? ConvertToBoolValue(object val, bool? defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : Convert.ToBoolean(val);
        }


        #endregion

        #region Guid

        /// <summary>
        /// Converts the input object parameter into a <c>Guid</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>Guid</c> value parameter</param>
        /// <returns>A <c>Guid</c> value</returns>
        public static Guid SetValue(object dataValue, Guid defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToGuidValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToGuidValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>Guid</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>Guid</c> instance to be converted</param>
        /// <returns>The converted <c>Guid</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(Guid value)
        {
            object ret = value;
            if (Guid.Empty == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>Guid</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>Guid</c> value parameter</param>
        /// <returns>A <c>Guid</c> value</returns>
        private static Guid ConvertToGuidValue(object val, Guid defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : new Guid(val.ToString());
        }


        #endregion

        #region byte[]

        /// <summary>
        /// Converts the input object parameter into a <c>byte</c> array value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>byte</c> array value parameter</param>
        /// <returns>A <c>byte</c> array value</returns>
        public static byte[] SetValue(object dataValue, byte[] defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToByteArrayValue(((SqlParameter)dataValue).Value, defaultValue) : ConvertToByteArrayValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>byte</c> array instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>byte</c> array instance to be converted</param>
        /// <returns>The converted <c>byte</c> array as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(byte[] value)
        {
            object returnValue;

            if (null == value)
            {
                returnValue = DBNull.Value;
            }
            else
            {
                returnValue = value;
            }

            return returnValue;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>byte</c> array value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>byte</c> array value parameter</param>
        /// <returns>A <c>byte</c> array value</returns>
        private static byte[] ConvertToByteArrayValue(object val, byte[] defaultTo)
        {
            return (val == null || val == DBNull.Value) ? defaultTo : (byte[])(val);
        }

        #endregion

        #region XmlDocument

        /// <summary>
        /// Converts the input object parameter into an <c>XmlDocument</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>XmlDocument</c> value parameter</param>
        /// <returns>An <c>XmlDocument</c> value</returns>
        public static XmlDocument SetValue(object dataValue, XmlDocument defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? ConvertToXmlValue(((SqlParameter)dataValue).Value, defaultValue)
                : ConvertToXmlValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts an <c>XmlDocument</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>XmlDocument</c> instance to be converted</param>
        /// <returns>The converted <c>XmlDocument</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(XmlDocument value)
        {
            object ret;

            if (value == null || value.DocumentElement == null)
            {
                ret = DBNull.Value;
            }
            else
            {
                ret = value.OuterXml;
            }

            return ret; 
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>XmlDocument</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>XmlDocument</c> value parameter</param>
        /// <returns>An <c>XmlDocument</c></returns>
        private static XmlDocument ConvertToXmlValue(object val, XmlDocument defaultTo)
        {
            const string EMPTY_XML = "<xml/>";

            XmlDocument retVal = defaultTo; 

            if (val != null && val != DBNull.Value)
            {
                retVal = new XmlDocument();
                
                retVal.LoadXml(ConvertToStringValue(val,EMPTY_XML));
            }

            return retVal; 
        }

        #endregion

        #region IPAddress

        /// <summary>
        /// Converts the input object parameter into a <c>string</c> value
        /// </summary>
        /// <param name="dataValue">The input object parameter</param>
        /// <param name="defaultValue">The output <c>string</c> value parameter</param>
        /// <returns>A <c>string</c> value</returns>
        public static IPAddress SetValue(object dataValue, IPAddress defaultValue)
        {
            return _IsTypeSqlParameter(dataValue) ? _ConvertToIpAddressValue(((SqlParameter)dataValue).Value, defaultValue) : _ConvertToIpAddressValue(dataValue, defaultValue);
        }

        /// <summary>
        /// Converts a <c>string</c> instance to a value suitable for assignment to a <c>SqlParameter</c>
        /// </summary>
        /// <param name="value">The <c>IPAddress</c> instance to be converted</param>
        /// <returns>The converted <c>IPAddress</c> as an <c>object</c> type</returns>
        /// <remarks>Converts null arguments to <c>DBNull.Value</c></remarks>
        public static object ParameterValue(IPAddress value)
        {
            object ret = value;
            if (IPAddress.None == value)
            {
                ret = DBNull.Value;
            }
            return ret;
        }

        /// <summary>
        /// Helper function for converting the input object parameter into a <c>string</c> value
        /// </summary>
        /// <param name="val">The input object parameter</param>
        /// <param name="defaultTo">The output <c>IPAddress</c> value parameter</param>
        /// <returns>A <c>string</c> value</returns>
        private static IPAddress _ConvertToIpAddressValue(object val, IPAddress defaultTo)
        {
            IPAddress retVal; 

            if (val == null || val == DBNull.Value)
            {
                retVal = defaultTo; 
            }
            else
            {
                string ip = val.ToString();
                if (!IPAddress.TryParse(ip, out retVal))
                {
                    retVal = defaultTo; 
                }
            }

            return retVal; 
        }


        #endregion

        #endregion

        #region Logging utilities

        /// <summary>
        /// Formats the supplied command's <c>CommandType</c>, <c>CommandText</c> and <see cref="DbParameter"/> collection into a string suitable for logging
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to format</param>
        /// <returns>A <see cref="string"/> instance containing the text representation of the <see cref="IDbCommand"/>.</returns>
        public static string FormatCommandAsLogText(IDbCommand cmd)
        {
            StringBuilder logText = new StringBuilder().AppendFormat("DB: Calling '{0}': '{1}'",
                cmd.CommandType, cmd.CommandType == CommandType.Text ? "<Embedded SQL omitted for security purposes.>" : cmd.CommandText);
            
            foreach(DbParameter dbParam in cmd.Parameters)
            {
                logText.AppendFormat(" '{0}' : '{1}'", dbParam.ParameterName, dbParam.Value);
            }

            return logText.ToString();
        }

        ///<summary>
        /// Helper function to format an <see cref="SqlException"/>.
        ///</summary>
        ///<param name="sqlException">The exception to format.</param>
        ///<returns>A formatted string containing the exception information.</returns>
        public static string FormatSqlException(SqlException sqlException)
        {
            string message = string.Format("{0}:\r\nClass: '{1}' LineNumber: '{2}' Number: '{3}' Procedure: '{4}' Server: '{5}' Source: '{6}'\r\nStackTrace: {7}",
                sqlException.Message, 
                sqlException.Class, 
                sqlException.LineNumber,
                sqlException.Number,
                sqlException.Procedure,
                sqlException.Server,
                sqlException.Source,
                sqlException.StackTrace);

            return message;
        }


        #endregion
    }
}
