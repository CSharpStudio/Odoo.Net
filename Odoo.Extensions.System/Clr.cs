using System.Dynamic;

namespace System
{
    /// <summary>
    /// CLR工具类
    /// </summary>
    public static class Clr
    {
        /// <summary>
        /// <see cref="int"/>
        /// </summary>
        public static readonly Type IntType = typeof(int);
        /// <summary>
        /// <see cref="long"/>
        /// </summary>
        public static readonly Type LongType = typeof(long);
        /// <summary>
        /// <see cref="Guid"/>
        /// </summary>
        public static readonly Type GuidType = typeof(Guid);
        /// <summary>
        /// <see cref="bool"/>
        /// </summary>
        public static readonly Type BoolType = typeof(bool);
        /// <summary>
        /// <see cref="bool"/>?
        /// </summary>
        public static readonly Type BoolTypeNull = typeof(bool?);
        /// <summary>
        /// <see cref="byte"/>
        /// </summary>
        public static readonly Type ByteType = typeof(byte);
        /// <summary>
        /// <see cref="object"/>
        /// </summary>
        public static readonly Type ObjectType = typeof(object);
        /// <summary>
        /// <see cref="double"/>
        /// </summary>
        public static readonly Type DoubleType = typeof(double);
        /// <summary>
        /// <see cref="float"/>
        /// </summary>
        public static readonly Type FloatType = typeof(float);
        /// <summary>
        /// <see cref="short"/>
        /// </summary>
        public static readonly Type ShortType = typeof(short);
        /// <summary>
        /// <see cref="decimal"/>
        /// </summary>
        public static readonly Type DecType = typeof(decimal);
        /// <summary>
        /// <see cref="string"/>
        /// </summary>
        public static readonly Type StringType = typeof(string);
        /// <summary>
        /// <see cref="DateTime"/>
        /// </summary>
        public static readonly Type DateType = typeof(DateTime);
        /// <summary>
        /// <see cref="DateTimeOffset"/>
        /// </summary>
        public static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);
        /// <summary>
        /// <see cref="TimeSpan"/>
        /// </summary>
        public static readonly Type TimeSpanType = typeof(TimeSpan);
        /// <summary>
        /// <see cref="byte"/>[]
        /// </summary>
        public static readonly Type ByteArrayType = typeof(byte[]);
        /// <summary>
        /// <see cref="ExpandoObject"/>
        /// </summary>
        public static readonly Type DynamicType = typeof(ExpandoObject);
        /// <summary>
        /// 装箱的true值
        /// </summary>
        public static readonly object True = true;
        /// <summary>
        /// 装箱的false值
        /// </summary>
        public static readonly object False = false;
    }
}
