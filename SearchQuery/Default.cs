namespace SearchQuery
{
    /// <summary>
    /// Represents the operations that can be used in a search query condition.
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// Equal operation.
        /// </summary>
        Equal = 0,

        /// <summary>
        /// Not equal operation.
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// Less than operation.
        /// </summary>
        LessThan = 2,

        /// <summary>
        /// Less than or equal operation.
        /// </summary>
        LessThanOrEqual = 3,

        /// <summary>
        /// Greater than operation.
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// Greater than or equal operation.
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// Contains operation.
        /// </summary>
        Contains = 6,

        /// <summary>
        /// Not contains operation.
        /// </summary>
        NotContains = 7,

        /// <summary>
        /// Starts with operation.
        /// </summary>
        StartsWith = 8,

        /// <summary>
        /// Not starts with operation.
        /// </summary>
        NotStartsWith = 9,

        /// <summary>
        /// Ends with operation.
        /// </summary>
        EndsWith = 10,

        /// <summary>
        /// Not ends with operation.
        /// </summary>
        NotEndsWith = 11,

        /// <summary>
        /// Between operation.
        /// </summary>
        Between = 12,

        /// <summary>
        /// Not between operation.
        /// </summary>
        NotBetween = 13
    }

    /// <summary>
    /// Represents the case sensitivity options for a search query condition.
    /// </summary>
    public enum Case
    {
        /// <summary>
        /// Default case sensitivity.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Lowercase sensitivity.
        /// </summary>
        Lower = 1,

        /// <summary>
        /// Uppercase sensitivity.
        /// </summary>
        Upper = 2,
    }

    /// <summary>
    /// Represents the logical operators that can be used to combine search query conditions.
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// And operator.
        /// </summary>
        And = 0,

        /// <summary>
        /// Or operator.
        /// </summary>
        Or = 1
    }

    public enum Types
    {
        Null = 0,
        String = 1,
        Number = 2,
        Boolean = 3,
        Date = 4,
        Enum = 5,
    }

    public enum Format
    {
        // ISO Formats (..10)

        DateOnly = 1,
        TimeOnly = 2,
        ISODateTime = 3,
        ISODateTimeWithMilliseconds = 4,
        ISODateTimeWithOffset = 5,
        ISODateTimeUTC = 6,
        ISODateTimeWithMillisecondsUTC = 7,

        // DateOnly Formats (..20)

        DateDays = 11,
        DateMonths = 12,
        DateYears = 13,

        // TimeOnly Formats (..30)

        TimeOn = 21,
        TimeFull = 22,
        TimeHours = 23,
        TimeMinutes = 24,
        TimeSeconds = 25,
        TimeMilliseconds = 26,
    }

    internal static class Formats
    {
        internal static readonly Dictionary<Format, string> Values = new Dictionary<Format, string>
        {
            // ISO Formats

            { Format.DateOnly, "yyyy-MM-dd" },
            { Format.TimeOnly, "HH:mm:ss" },
            { Format.ISODateTime, "yyyy-MM-ddTHH:mm:ss" },
            { Format.ISODateTimeUTC, "yyyy-MM-ddTHH:mm:ssZ" },
            { Format.ISODateTimeWithMilliseconds, "yyyy-MM-ddTHH:mm:ss.fff" },
            { Format.ISODateTimeWithOffset, "yyyy-MM-ddTHH:mm:sszzz" },
            { Format.ISODateTimeWithMillisecondsUTC, "yyyy-MM-ddTHH:mm:ss.fffZ" },
        
            // DateOnly Formats

            { Format.DateDays, "dd" },
            { Format.DateMonths, "MM" },
            { Format.DateYears, "yyyy" },

            // TimeOnly Formats

            { Format.TimeOn, "HH:mm" },
            { Format.TimeHours, "HH" },
            { Format.TimeMinutes, "mm" },
            { Format.TimeSeconds, "ss" },
            { Format.TimeMilliseconds, "fff" },
            { Format.TimeFull, "HH:mm:ss.fff" },
        };
    }
}
