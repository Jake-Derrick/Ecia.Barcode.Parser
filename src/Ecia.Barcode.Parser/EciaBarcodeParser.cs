using System.Text.RegularExpressions;

namespace Ecia.Barcode.Parser;

/// <summary>
/// Implementation of <see cref="IEciaBarcodeParser"/>
/// </summary>
public partial class EciaBarcodeParser : IEciaBarcodeParser
{
    /// <inheritdoc />
    public ParsedBarcode ParseBarcode(string barcodeString)
    {
        ArgumentNullException.ThrowIfNull(barcodeString);
        var parsedBarcode = new ParsedBarcode();
        if (barcodeString.StartsWith(SpecialCharacters.Header, StringComparison.Ordinal))
        {
            parsedBarcode.CompliantHeader = true;
            barcodeString = barcodeString[SpecialCharacters.Header.Length..];
        }

        if (barcodeString.EndsWith(SpecialCharacters.Trailer, StringComparison.Ordinal))
        {
            parsedBarcode.CompliantTrailer = true;
            barcodeString = barcodeString[..^SpecialCharacters.Trailer.Length];
        }

        var groups = barcodeString.Split(SpecialCharacters.GroupSeparator);
        foreach (var group in groups)
        {
            var dataIdentifier = DataIdentifierMap.Keys.FirstOrDefault(group.StartsWith);
            if (dataIdentifier is null)
                continue;

            var value = group[dataIdentifier.Length..];
            // Map the value to the corresponding barcode property
            DataIdentifierMap[dataIdentifier](parsedBarcode, value);
        }

        return parsedBarcode;
    }

    /// <inheritdoc />
    public ValidationResult ValidateParsedBarcodeCompliance(ParsedBarcode parsedBarcode, LabelFormat labelFormat = LabelFormat.Unknown)
    {
        var result = new ValidationResult();

        if (!parsedBarcode.CompliantHeader)
            result.Errors.Add(nameof(parsedBarcode.CompliantHeader), "The barcode header is not compliant.");

        if (!parsedBarcode.CompliantTrailer)
            result.Errors.Add(nameof(parsedBarcode.CompliantTrailer), "The barcode trailer is not compliant.");

        ValidateField(nameof(parsedBarcode.CustomerPo), parsedBarcode.CustomerPo, 25, labelFormat is LabelFormat.Logistic or LabelFormat.PackingSlip, result.Errors);
        ValidateField(nameof(parsedBarcode.PackageIdIntermediateLabel), parsedBarcode.PackageIdIntermediateLabel, 25, false, result.Errors);
        ValidateField(nameof(parsedBarcode.PackageIdLogisticLabel), parsedBarcode.PackageIdLogisticLabel, 25, labelFormat is LabelFormat.Logistic, result.Errors);
        ValidateField(nameof(parsedBarcode.PackingListNumber), parsedBarcode.PackingListNumber, 25, labelFormat is LabelFormat.PackingSlip, result.Errors);
        ValidateField(nameof(parsedBarcode.ShipDate), parsedBarcode.ShipDate, 8, false, result.Errors, ShipDateRegex());
        ValidateField(nameof(parsedBarcode.CustomerPartNumber), parsedBarcode.CustomerPartNumber, 40, false, result.Errors);
        ValidateField(nameof(parsedBarcode.SupplierPartNumber), parsedBarcode.SupplierPartNumber, 40, true, result.Errors);
        ValidateField(nameof(parsedBarcode.CustomerPoLine), parsedBarcode.CustomerPoLine, 40, labelFormat is LabelFormat.PackingSlip, result.Errors);
        ValidateField(nameof(parsedBarcode.Quantity), parsedBarcode.Quantity, 9, true, result.Errors);
        ValidateField(nameof(parsedBarcode.DateCode), parsedBarcode.DateCode, 7, true, result.Errors, DateCodeRegex());
        ValidateField(nameof(parsedBarcode.LotCode), parsedBarcode.LotCode, 20, true, result.Errors);
        ValidateField(nameof(parsedBarcode.CountryOfOrigin), parsedBarcode.CountryOfOrigin, 2, true, result.Errors);
        ValidateField(nameof(parsedBarcode.SerialNumber), parsedBarcode.SerialNumber, 25, false, result.Errors);
        ValidateField(nameof(parsedBarcode.BinCode), parsedBarcode.BinCode, 35, false, result.Errors);
        ValidateField(nameof(parsedBarcode.PackageCount), parsedBarcode.PackageCount, int.MaxValue, false, result.Errors, PackageCountRegex());
        ValidateField(nameof(parsedBarcode.RevisionNumber), parsedBarcode.RevisionNumber, 6, false, result.Errors);
        ValidateField(nameof(parsedBarcode.Weight), parsedBarcode.Weight, int.MaxValue, false, result.Errors);
        ValidateField(nameof(parsedBarcode.Manufacturer), parsedBarcode.Manufacturer, int.MaxValue, false, result.Errors);
        ValidateField(nameof(parsedBarcode.RohsCC), parsedBarcode.RohsCC, int.MaxValue, false, result.Errors);

        return result;
    }

    private static void ValidateField(string fieldName, string? value, int maxLength, bool isRequired, Dictionary<string, string> errors, Regex? regex = null)
    {
        if (string.IsNullOrEmpty(value) && isRequired)
        {
            errors.Add(fieldName, "Field is required.");
            return;
        }

        if (string.IsNullOrEmpty(value))
            return;

        if (value?.Length > maxLength)
        {
            errors.Add(fieldName, $"Exceeds max length of {maxLength}.");
            return;
        }

        if (regex is not null && !regex.IsMatch(value!))
        {
            errors.Add(fieldName, "Value is not formatted correctly.");
            return;
        }

        return;
    }

    private static readonly Dictionary<string, Action<ParsedBarcode, string>> DataIdentifierMap = new()
    {
        { DataIdentifiers.CustomerPo, (model, value) => model.CustomerPo = value },
        { DataIdentifiers.PackageIdIntermediateLabel, (model, value) => model.PackageIdIntermediateLabel = value },
        { DataIdentifiers.PackageIdLogisticLabel, (model, value) => model.PackageIdLogisticLabel = value },
        { DataIdentifiers.PackageIdLogisticLabelMixed, (model, value) => model.PackageIdLogisticLabel = value },
        { DataIdentifiers.PackingListNumber, (model, value) => model.PackingListNumber = value },
        { DataIdentifiers.ShipDate, (model, value) => model.ShipDate = value },
        { DataIdentifiers.CustomerPartNumber, (model, value) => model.CustomerPartNumber = value },
        { DataIdentifiers.SupplierPartNumber, (model, value) => model.SupplierPartNumber = value },
        { DataIdentifiers.CustomerPoLine, (model, value) => model.CustomerPoLine = value },
        { DataIdentifiers.Quantity, (model, value) => model.Quantity = value },
        { DataIdentifiers.DateCode9D, (model, value) => model.DateCode = value },
        { DataIdentifiers.DateCode10D, (model, value) => model.DateCode = value },
        { DataIdentifiers.LotCode, (model, value) => model.LotCode = value },
        { DataIdentifiers.CountryOfOrigin, (model, value) => model.CountryOfOrigin = value },
        { DataIdentifiers.SerialNumber, (model, value) => model.SerialNumber = value },
        { DataIdentifiers.BinCode, (model, value) => model.BinCode = value },
        { DataIdentifiers.PackageCount, (model, value) => model.PackageCount = value },
        { DataIdentifiers.RevisionNumber, (model, value) => model.RevisionNumber = value },
        { DataIdentifiers.Weight, (model, value) => model.Weight = value },
        { DataIdentifiers.Manufacturer, (model, value) => model.Manufacturer = value },
        { DataIdentifiers.RohsCC, (model, value) => model.RohsCC = value }
    };

    // Matches YYYYMMDD
    [GeneratedRegex(@"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$")]
    private static partial Regex ShipDateRegex();

    // Matches #/# or # of #
    [GeneratedRegex(@"^\d+\s*(\/|of)\s*\d+$")]
    private static partial Regex PackageCountRegex();

    // Matches YYWW with an optional letter M. Also matches N/T or MIXED
    [GeneratedRegex(@"^(?:\d{2}[0-5]\dM?|N\/T|MIXED)$")]
    private static partial Regex DateCodeRegex();
}
