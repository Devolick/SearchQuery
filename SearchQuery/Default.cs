namespace SearchQuery {
    public enum Operation
    {
        // Operations

        Equal = 0,
        NotEqual = 1,
        LessThan = 2,
        LessThanOrEqual = 3,
        GreaterThan = 4,
        GreaterThanOrEqual = 5,
        Contains = 6,
        NotContains = 7,
        StartsWith = 8,
        NotStartsWith = 9,
        EndsWith = 10,
        NotEndsWith = 11,

        // Between
        
        Between = 12,
        NotBetween = 13
    }

    public enum Case
    {
        Default = 0,
        Lower = 1,
        Upper = 2,
    }

    public enum Operator {
        And = 0, Or = 1
    }

    public enum Types
    {
        Null = 0,
        String = 1,
        Number = 2,
        Boolean = 3,
        Date = 4
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
