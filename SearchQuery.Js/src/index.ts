export enum Operation {
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
  NotBetween = 13,
}

export enum Case {
  Default = 0,
  Lower = 1,
  Upper = 2,
}

export enum Operator {
  And = 0,
  Or = 1,
}

export enum Types {
  Null = 0,
  String = 1,
  Number = 2,
  Boolean = 3,
  Date = 4,
}

export enum Format {
  // ISO Formats (..10)

  /**
   * yyyy-MM-dd
   */
  DateOnly = 1,
  /**
   * HH:mm:ss
   */
  TimeOnly = 2,
  /**
   * yyyy-MM-ddTHH:mm:ss
   */
  ISODateTime = 3,
  /**
   * yyyy-MM-ddTHH:mm:ss.fff
   */
  ISODateTimeWithMilliseconds = 4,
  /**
   * yyyy-MM-ddTHH:mm:sszzz
   */
  ISODateTimeWithOffset = 5,
  /**
   * yyyy-MM-ddTHH:mm:ssZ
   */
  ISODateTimeUTC = 6,
  /**
   * yyyy-MM-ddTHH:mm:ss.fffZ
   */
  ISODateTimeWithMillisecondsUTC = 7,

  // DateOnly Formats (..20)

  /**
   * dd
   */
  DateDays = 11,
  /**
   * MM
   */
  DateMonths = 12,
  /**
   * yyyy
   */
  DateYears = 13,

  // TimeOnly Formats (..30)

  /**
   * HH:mm
   */
  TimeOn = 21,
  /**
   * HH:mm:ss.fff
   */
  TimeFull = 22,
  /**
   * HH
   */
  TimeHours = 23,
  /**
   * mm
   */
  TimeMinutes = 24,
  /**
   * ss
   */
  TimeSeconds = 25,
  /**
   * fff
   */
  TimeMilliseconds = 26,
}

export interface Condition {
  readonly field: string;
  readonly operation: Operation;
  readonly values: (string | number | boolean)[];
  readonly format?: Format;
  readonly incase?: Case;
}

export interface Query {
  readonly operator: Operator;
  readonly conditions: (Condition | Query)[];
}

export interface Search extends Query {
  readonly format?: Format;
}
