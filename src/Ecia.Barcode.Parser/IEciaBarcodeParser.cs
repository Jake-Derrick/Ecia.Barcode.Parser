namespace Ecia.Barcode.Parser;

public interface IEciaBarcodeParser
{
    /// <summary>
    /// Parses a barcode string into a <see cref="ParsedBarcode"/>.
    /// </summary>
    /// <returns>A <see cref="ParsedBarcode"/> object populated with values extracted from the barcode string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="barcodeString"/> is null.</exception>
    ParsedBarcode ParseBarcode(string barcodeString);

    /// <summary>
    /// Validates the compliance of a parsed barcode against a specific label format.
    /// </summary>
    /// <param name="parsedBarcode">The parsed barcode object that contains the decoded data to be validated.</param>
    /// <param name="labelFormat">The label format against which the barcode's compliance will be checked.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> object containing any validation errors found during the compliance check.
    /// If the validation is successful, the <see cref="ValidationResult.Success"/> property will be true.
    /// </returns>
    ValidationResult ValidateParsedBarcodeCompliance(ParsedBarcode parsedBarcode, LabelFormat labelFormat = LabelFormat.Unknown);
}
