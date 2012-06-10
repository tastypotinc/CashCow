#region Namespaces

using System.ComponentModel;

#endregion Namespaces

namespace CashCow.Web.MvcHelpers
{

    //public class AwRequiredAttribute : RequiredAttribute
    //{

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "RequiredField";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }
    //}

    
    //public class AwRangeAttribute : RangeAttribute
    //{
    //    public AwRangeAttribute(int minimum, int maximum)
    //        : base(minimum, maximum)
    //    {
    //    }

    //    public AwRangeAttribute(int minimum, int maximum, string errorMessage)
    //        : base(minimum, maximum)
    //    {
    //        ErrorMessage = errorMessage;
    //    }

    //    public AwRangeAttribute(double minimum, double maximum)
    //        : base(minimum, maximum)
    //    {
    //    }

    //    public AwRangeAttribute(double minimum, double maximum, string errorMessage)
    //        : base(minimum, maximum)
    //    {
    //        ErrorMessage = errorMessage;
    //    }

    //    public AwRangeAttribute(Type type, string minimum, string maximum)
    //        : base(type, minimum, maximum)
    //    {
    //    }

    //    public AwRangeAttribute(Type type, string minimum, string maximum, string errorMessage)
    //        : base(type, minimum, maximum)
    //    {
    //        ErrorMessage = errorMessage;
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "FieldShouldBeBetweenArgAndArg";
    //        return LanguageLabel.GetLabelText(ErrorMessage, Minimum, Maximum);
    //    }
    //}

    //public class AwRegularExpressionAttribute : RegularExpressionAttribute
    //{
    //    public AwRegularExpressionAttribute(string pattern)
    //        : base(pattern)
    //    {
    //    }

    //    public AwRegularExpressionAttribute(string pattern, string errorMessage) : base(pattern)
    //    {
    //        ErrorMessage = errorMessage;
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "InvalidDataEntered";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }
    //}


    ///// <summary>
    ///// This class validates an email entry.  If you whish to allow leading and trailing whitespaces 
    ///// in the validation, but do not want to have the whitespaces saved in the database, then
    ///// instead of doing a Trim() in the controller, add the attribute AwTrimTransformAttribute to
    ///// the property.  The value will be trimed AFTER the validation, so that when it is sent to the action method
    ///// in the controller, the values will be trimmed. 
    ///// </summary>
    ///// 
    ///// <remarks>
    ///// If a property has an instance of <seealso cref="AwStringTransformAttribute"/> set on it, if 
    ///// a property fails validation, it will still be transformed.
    ///// </remarks>
    //public class AwEmailAttribute : AwRegularExpressionAttribute
    //{
    //    private const string RegExEmailNoSpaces = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\'+)|([A-Za-z0-9]+\&+)|([A-Za-z0-9]+\=+)|([A-Za-z0-9]+\/+)|([A-Za-z0-9]+\}+)|([A-Za-z0-9]+\~+)|([A-Za-z0-9]+\^+)|([A-Za-z0-9]+\{+)|([A-Za-z0-9]+\$+)|([A-Za-z0-9]+\%+)|([A-Za-z0-9]+\*+)|([A-Za-z0-9]+\?+)|([A-Za-z0-9]+\|+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\!+)|([A-Za-z0-9]+\#+)|([A-Za-z0-9]+\`+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";
    //    private const string RegExEmailSpaces = @"^(\s*)?(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\'+)|([A-Za-z0-9]+\&+)|([A-Za-z0-9]+\=+)|([A-Za-z0-9]+\/+)|([A-Za-z0-9]+\}+)|([A-Za-z0-9]+\~+)|([A-Za-z0-9]+\^+)|([A-Za-z0-9]+\{+)|([A-Za-z0-9]+\$+)|([A-Za-z0-9]+\%+)|([A-Za-z0-9]+\*+)|([A-Za-z0-9]+\?+)|([A-Za-z0-9]+\|+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\!+)|([A-Za-z0-9]+\#+)|([A-Za-z0-9]+\`+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)?$";

    //    /// <summary>
    //    /// Constructor that defines whether or not to allow leading and trailing whitespaces.
    //    /// </summary>
    //    /// <param name="allowLeadingTrailingWhitespace"></param>
    //    public AwEmailAttribute(bool allowLeadingTrailingWhitespace)
    //        : base(allowLeadingTrailingWhitespace ? RegExEmailSpaces : RegExEmailNoSpaces)
    //    {
    //    }
    //    /// <summary>
    //    /// Default constructor that does not allow leading and trailing whitespaces.  
    //    /// </summary>
    //    public AwEmailAttribute()
    //        : this(false)
    //    {
    //    }
            
    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "InvalidEmailAddress";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}

    //public class AwOnlyUnderscoreSpecialCharacterAttribute : AwRegularExpressionAttribute
    //{
    //    public AwOnlyUnderscoreSpecialCharacterAttribute() :
    //        base(@"^\w+$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "OnlyUnderscoreIsAllowed";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}

    //public class AwOnlyUnderscoreSpaceSlashSpecialCharacterAttribute : AwRegularExpressionAttribute
    //{
    //    public AwOnlyUnderscoreSpaceSlashSpecialCharacterAttribute() :
    //        base(@"^[a-zA-Z0-9_\-\\/\' ']*$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "UnderscoreSpaceSlashesAreAllowed";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}

    //public class AwDomainNameNotAllowedAttribute : AwRegularExpressionAttribute
    //{
    //    public AwDomainNameNotAllowedAttribute() :
    //        base(@"^([^\/\\]*)$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "DomainNameNotAllowed";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}

    //public class AwPositiveFloatAttribute : AwRegularExpressionAttribute
    //{
    //    public AwPositiveFloatAttribute() :
    //        base(@"0*[0-9]\d{0,36}(\.\d{0,2})?$|0?\.[1-9]\d?|0?\.\d[1-9]") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "PositiveFloatOfTwoDecimalsIsAllowed";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}


    //public class AwServerAttribute : AwRegularExpressionAttribute
    //{
    //    public AwServerAttribute() :
    //        base(@"(((http|https):\/\/))?[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "InvalidServerName";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}

    //public class AwDomainNameAttribute : AwRegularExpressionAttribute
    //{
    //    public AwDomainNameAttribute() :
    //        base(@"^[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "InvalidDomainName";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }

    //}

    //public class AwStringLengthAttribute : StringLengthAttribute
    //{
    //    public AwStringLengthAttribute(int maximumLength)
    //        : base(maximumLength)
    //    {
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "FieldShouldBeMaximumOfArgCharacters";
    //        return LanguageLabel.GetLabelText(ErrorMessage, MaximumLength);
    //    }
    //}

    //public class AwNumberAttribute : RegularExpressionAttribute
    //{
    //    public AwNumberAttribute() :
    //        base(@"^\d+$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "FieldShouldBeValidNumber";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }
    //}

    //public class AwPhoneNumberAttribute : RegularExpressionAttribute
    //{
    //    public AwPhoneNumberAttribute() :
    //        base(@"^[0-9' '\.+\-\(\)]*$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "InvalidPhoneNumber";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }
    //}


    // public class AwPhoneNumberLengthAttribute : RegularExpressionAttribute
    //{
    //     public AwPhoneNumberLengthAttribute() :
    //        base(@"^[0-9A-Z' '\.+\-\(\)]{10,20}$") { }    //@"^[0-9A-Z' '\.+\-\(\)]{10,20}$"  

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "InvalidPhoneNumberFormat";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }
    //}
   



    //public class AwAlphaNumericAttribute : RegularExpressionAttribute
    //{
    //    public AwAlphaNumericAttribute() :
    //        base(@"^\w+$") { }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        if (String.IsNullOrEmpty(ErrorMessage))
    //            ErrorMessage = "RequireAlphanumericValue";
    //        return LanguageLabel.GetLabelText(ErrorMessage);
    //    }
    //}

    ///// <summary>
    ///// Expects a CSV of file extensions which will be validated client side via Regex
    ///// No spaces and no period just the alphanumeric
    ///// eg. jpg,png,gif 
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwAllowedFileTypesAttribute:Attribute {
    //    public string AllowedFileTypes{ get; set;}
    //    public AwAllowedFileTypesAttribute(string s)
    //    {
    //        AllowedFileTypes = s;
    //    }
    //}

    ///// <summary>
    ///// Expects a CSV of file extensions which will be validated client side via Regex
    ///// No spaces and no period just the alphanumeric
    ///// eg. jpg,png,gif 
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwPasswordRequiredAttribute: Attribute
    //{
    //    public bool PasswordRequired { get; set; }
    //    public AwPasswordRequiredAttribute(bool b)
    //    {
    //        PasswordRequired = b;
    //    }
    //}

    //public class AwMaximumFileSizeAttribute:Attribute
    //{
    //    public string MaximumFileSize { get; set; }
    //    public double MaxFileInt { get; set; }

    //    public AwMaximumFileSizeAttribute(string maximumfileSize, double maxfileint)
    //    {
    //        MaximumFileSize = LanguageLabel.GetLabelText("MaxAllowedFileSizeArg", maximumfileSize);
    //        MaxFileInt = maxfileint;
    //    }
    //}

    ////Class added as attribute to denote that the blob is private
    //public class AwPrivateBlob : Attribute
    //{
    //    public bool IsPrivate { get; set; }
    //    public AwPrivateBlob()
    //    {
    //        IsPrivate = true;
    //    }
    //}

    //public class AwBlobLimitTypeAttribute : Attribute
    //{
    //    public string BlobLimitType { get; set; }
    //    public AwBlobLimitTypeAttribute(string blobLimitType)
    //    {
    //        BlobLimitType = blobLimitType;
    //    }
    //}

    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    //public class AwModuleAttribute : Attribute
    //{
    //    public string ModuleName { get; set; }
    //}

    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    //public class AwSettingsCategoryAttribute : Attribute
    //{
    //    public string CategoryName { get; set; }
    //}

    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    //public class AwNamespaceAttribute : Attribute
    //{
    //    public string SystemCode { get; set; }
    //    public string Globalization { get; set; }
    //    public string Payload { get; set; }
    //}

    //// Trying...
    //public class RequiredIfAttribute : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    /// <summary>
    /// This is a workaround. Refer to http://aspnet.codeplex.com/workitem/5515
    /// </summary>
    public class CcDisplayName : DisplayNameAttribute
    {
        private readonly string key;
        public CcDisplayName(string key)
            : base()
        {
            this.key = key;
        }

        public override string DisplayName
        {
            get
            {
                return key;
            }
        }
    }

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwExcludeAttribute : Attribute
    //{
    //    public bool Exclude { get; set; }

    //    public void AwExclude()
    //    {
    //        Exclude = true;
    //    }
    //}

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwIOS5Attribute : Attribute
    //{
    //    public bool enableIOS5 { get; set; }

    //    public void AwIOS5()
    //    {
    //        enableIOS5 = true; 
    //    }
    //}

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwAndroid3Attribute : Attribute
    //{
    //    public bool enableAndroid3 { get; set; }

    //    public void AwAndroid3()
    //    {
    //        enableAndroid3 = true;
    //    }
    //}


    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwAndroid4Attribute : Attribute
    //{
    //    public bool enableAndroid4 { get; set; }

    //    public void AwAndroid4()
    //    {
    //        enableAndroid4 = true;
    //    }
    //}

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwDisplaytagAttribute : Attribute
    //{
    //    public string DisplayKey { get; set; }

    //    public AwDisplaytagAttribute(string key)
    //    {
    //        DisplayKey = key;
    //    }

       
    //}

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class AwValidateCaptchaAttribute : ValidationAttribute
    //{
    //    public AwValidateCaptchaAttribute()
    //    {
    //        ErrorMessage = LanguageLabel.GetLabelText("YourResponseDidNotMatchPleaseTryAgain");
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        bool valid = false;
    //        if(HttpContext.Current.Session != null && value != null)
    //        {
    //            if(HttpContext.Current.Session["captcha"] != null)
    //            {
    //                valid = HttpContext.Current.Session["captcha"].ToString().CompareTo(value.ToString()) == 0;
    //            }
    //        }
    //        return valid;
    //    }
    //}
    ///// <summary>
    ///// This validator is for properties that are <see cref="IEnumerable"/>.  You may 
    ///// specify a minimum and a maximum value.  The minimum is 1.  
    ///// </summary>
    //public class AwIEnumerableRangeCountAttribute : ValidationAttribute
    //{
    //    private const int VALUE_MIN = 1;
    //    private const int VALUE_MAX = int.MaxValue;

    //    private int _min = VALUE_MIN;
    //    private int _max = VALUE_MAX;

    //    /// <summary>
    //    /// Constructor that specifies the minimum and maximum number of
    //    /// items that can be enumerated.
    //    /// </summary>
    //    /// <param name="minimum">The minimum number of items.  Cannot be less than one.</param>
    //    /// <param name="maximum">The maximum number of items.</param>
    //    public AwIEnumerableRangeCountAttribute(int minimum, int maximum)
    //    {
    //        this.MinItems = minimum;
    //        this.MaxItems = maximum;
    //    }

    //    /// <summary>
    //    /// Constructor that only specifies the minimum number of items 
    //    /// that can be enumerated.
    //    /// </summary>
    //    /// <param name="minimum">The minimum number of items.  Cannot be less than one.</param>
    //    public AwIEnumerableRangeCountAttribute(int minimum)
    //        : this(minimum, VALUE_MAX)
    //    {
    //    }

    //    /// <summary>
    //    /// Default constructor that specifies that there must be at least
    //    /// one item that can be enumerated.
    //    /// </summary>
    //    public AwIEnumerableRangeCountAttribute()
    //        : this(VALUE_MIN, VALUE_MAX)
    //    {
    //    }

    //    /// <summary>
    //    /// The minimum number of items.  Cannot be less than one and cannot be greater than <see cref="MaxItems"/>
    //    /// </summary>
    //    public int MinItems
    //    {
    //        get { return _min; }
    //        set
    //        {
    //            if (value < VALUE_MIN || value > this.MaxItems)
    //                throw new ArgumentOutOfRangeException("MinItems", "MinItems must be between 0 and MaxItems");
    //            _min = value;
    //        }
    //    }

    //    /// <summary>
    //    /// The maximum number of items that can be enumerated.  Cannot be less than <see cref="MinItems"/>
    //    /// </summary>
    //    public int MaxItems
    //    {
    //        get { return _max; }
    //        set
    //        {
    //            if (value < this.MinItems)
    //                throw new ArgumentOutOfRangeException("MaxItems", "MaxItems cannot be less than MinItems");
    //            _max = value;
    //        }
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        IEnumerable enumObject = value as IEnumerable;
    //        if (enumObject == null)
    //            throw new AwValidationException("Attribute is applied to a property that does not implement the interface IEnumerable");

    //        IEnumerator enumerator = enumObject.GetEnumerator();
    //        int count = 0;
    //        while (enumerator.MoveNext())
    //            count++;

    //        if (count < this.MinItems || count > this.MaxItems)
    //            return false;
    //        else
    //            return true;
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        //specifying only a minimum number of items
    //        if (this.MaxItems == VALUE_MAX)
    //        {
    //            if (String.IsNullOrEmpty(ErrorMessage))
    //                ErrorMessage = this.MinItems == 1 ? "AtLeastOneItemIsRequired" : "MinimumOfArgItemsAreRequired";

    //            return this.MinItems ==1 ? 
    //                LanguageLabel.GetLabelText(ErrorMessage): LanguageLabel.GetLabelText(ErrorMessage, this.MinItems);
    //        }
    //        //specifying both a minimum and maximum
    //        else
    //        {
    //            if(String.IsNullOrEmpty(ErrorMessage))
    //                ErrorMessage = "NumberOfItemsMustBeBetweenArgAndArg";

    //            return LanguageLabel.GetLabelText(ErrorMessage, this.MinItems, this.MaxItems);
    //        }

    //    }

    //}

    ///// <summary>
    ///// This validator is for properties that are <see cref="IEnumerable"/> 
    ///// there are a minimum number of items that can be enumerated but has no limit on the maximum
    ///// number of items.
    ///// </summary>
    //public class AwMinimumIEnumerableCountAttribute : AwIEnumerableRangeCountAttribute
    //{
    //    /// <summary>
    //    /// Constructor that specifies the minimum number of items that can be enumerated.
    //    /// </summary>
    //    /// <param name="minimum"></param>
    //    public AwMinimumIEnumerableCountAttribute(int minimum)
    //        : base(minimum)
    //    {
    //    }

    //    /// <summary>
    //    /// Constructor that specifies that there should be at least one item that can be enumerated.
    //    /// </summary>
    //    public AwMinimumIEnumerableCountAttribute()
    //        : base()
    //    {
    //    }
    //}
}