namespace Odoo.Net.Data
{
    /// <summary>
    /// C#数据类型
    /// </summary>
    public enum DataType
    {
        None,
        /// <summary>
        /// A simple type representing Boolean values of true or false.
        /// </summary>
        Boolean,
        /// <summary>
        /// A type representing Unicode characters between 1 and 4,000
        /// </summary>
        String,
        /// <summary>
        /// A type representing Unicode characters
        /// </summary>
        Text,
        /// <summary>
        /// A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.
        /// </summary>
        Decimal,
        /// <summary>
        /// A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.
        /// </summary>
        Double,
        /// <summary>
        /// A globally unique identifier (or GUID).
        /// </summary>
        Guid,
        /// <summary>
        /// An integral type representing signed 8-bit integers with values between -128 and 127.
        /// </summary>
        Byte,
        /// <summary>
        /// An integral type representing signed 16-bit integers with values between -32768 and 32767.
        /// </summary>
        Int16,
        /// <summary>
        /// An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.
        /// </summary>
        Int32,
        /// <summary>
        /// An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.
        /// </summary>
        Int64,
        /// <summary>
        /// A general type representing any reference or value type not explicitly represented by another DbType value.
        /// </summary>
        Object,
        /// <summary>
        /// A variable-length stream of binary data ranging between 1 and 8,000 bytes.
        /// </summary>
        Binary,
        /// <summary>
        /// A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.
        /// </summary>
        Single,
        /// <summary>
        /// A type representing a date
        /// </summary>
        Date,
        /// <summary>
        /// A type representing a SQL Server DateTime value. If you want to use a SQL Server time value, use System.Data.SqlDbType.Time.
        /// </summary>
        Time,
        /// <summary>
        /// A type representing a date and time value.
        /// </summary>
        DateTime,
        /// <summary>
        /// Date and time data with time zone awareness. Date value range is 
        /// from January 1,1 AD through December 31, 9999 AD. Time value range 
        /// is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 
        /// nanoseconds. Time zone value range is -14:00 through +14:00.
        /// </summary>
        DateTimeOffset,
        /// <summary>
        /// 
        /// </summary>
        TimeSpan
    }
}
