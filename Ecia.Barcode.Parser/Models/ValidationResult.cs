namespace Ecia.Barcode.Parser;

/// <summary>
/// Represents the result of a validation operation, including any errors encountered.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// A dictionary containing validation errors, where the key is the field name and the value is the error message.
    /// </summary>
    public Dictionary<string, string> Errors { get; set; } = [];

    /// <summary>
    /// Indicates whether the validation was successful, which is true if there are no errors.
    /// </summary>
    public bool Success => Errors.Count == 0;
}
