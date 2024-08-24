namespace Ecia.Barcode.Parser;

public static class SpecialCharacters
{
    public const char RecordSeparator = (char)30;
    public const char GroupSeparator = (char)29;
    public const char EndOfTransmission = (char)4;
    public const string ComplianceIndicator = "[)>";

    // TODO: Do I want these in SpecialCharacters?
    public static string Header => $"{ComplianceIndicator}{RecordSeparator}06{GroupSeparator}";
    public static string Trailer => $"{RecordSeparator}{EndOfTransmission}";
}
