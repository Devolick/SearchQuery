namespace SearchQuery
{
    public enum SearchOperation
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

    public enum SearchCase
    {
        Default = 0,
        Lower = 1,
        Upper = 2,
    }
}
