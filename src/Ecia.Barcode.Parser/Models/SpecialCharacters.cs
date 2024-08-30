namespace Ecia.Barcode.Parser;

/// <summary>
/// Contains special characters used in barcode parsing.
/// </summary>
public static class SpecialCharacters
{
    /// <summary>
    /// Represents the Record Separator character (ASCII 30).
    /// </summary>
    public const char RecordSeparator = (char)30;

    /// <summary>
    /// Represents the Group Separator character (ASCII 29).
    /// </summary>
    public const char GroupSeparator = (char)29;

    /// <summary>
    /// Represents the End of Transmission character (ASCII 4).
    /// </summary>
    public const char EndOfTransmission = (char)4;

    /// <summary>
    /// Represents the Compliance Indicator string.
    /// </summary>
    public const string ComplianceIndicator = "[)>";

    /// <summary>
    /// Represents the header format used in barcodes.
    /// </summary>
    public static string Header => $"{ComplianceIndicator}{RecordSeparator}06{GroupSeparator}";

    /// <summary>
    /// Represents the trailer format used in barcodes.
    /// </summary>
    public static string Trailer => $"{RecordSeparator}{EndOfTransmission}";
}
