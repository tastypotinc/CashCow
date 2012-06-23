#region Namespaces

using System.Web.Script.Serialization;

#endregion Namespaces

namespace ShowSwingPoint.Classes
{
    /// <summary>
    /// Class to deal with all kinds of data formatting.
    /// </summary>
    public class DataFormatter
    {
        #region Public Methods
        
        public static string SerializeToJson(object objectToSerialize)
        {
            var serializer = new JavaScriptSerializer();
            
            return serializer.Serialize(objectToSerialize);
        }

        #endregion Public Methods
    }
}
