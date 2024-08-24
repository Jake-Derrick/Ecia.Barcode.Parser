namespace Ecia.Barcode.Parser;

/// <summary>
/// ECIA 2D barcode label formats
/// </summary>
public enum LabelFormat
{
    /// <summary>
    /// Unspecified label format
    /// </summary>
    Unknown,

    /// <summary>
    /// Product Label
    /// </summary>
    Product,

    /// <summary>
    /// Intermediate Label
    /// </summary>
    Intermediate,

    /// <summary>
    /// Logistic Label
    /// </summary>
    Logistic,

    /// <summary>
    /// Packing Slip
    /// </summary>
    PackingSlip
}
