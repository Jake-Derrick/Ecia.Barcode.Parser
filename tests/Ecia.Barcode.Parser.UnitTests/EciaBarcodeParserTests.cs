
namespace Ecia.Barcode.Parser.UnitTests;

public class EciaBarcodeParserTests
{
    [Test]
    public void ParseBarcode_AllDataIdentifiersParsed()
    {
        (var expectedParsedBarcode, var barcodeString) = GetCompleteBarcode();

        var parser = new EciaBarcodeParser();
        var actualParsedBarcode = parser.ParseBarcode(barcodeString);

        actualParsedBarcode.Should().BeEquivalentTo(expectedParsedBarcode);
    }

    [Test]
    public void ParseBarcode_NoHeader_StillParses_CompliantHeadedFalse()
    {
        (var expectedParsedBarcode, var barcodeString) = GetCompleteBarcode();
        barcodeString = barcodeString[SpecialCharacters.Header.Length..]; // Remove the header
        expectedParsedBarcode.CompliantHeader = false;

        var parser = new EciaBarcodeParser();
        var actualParsedBarcode = parser.ParseBarcode(barcodeString);

        actualParsedBarcode.Should().BeEquivalentTo(expectedParsedBarcode);
    }

    [Test]
    public void ParseBarcode_NoTrailer_StillParses_CompliantTrailerFalse()
    {
        (var expectedParsedBarcode, var barcodeString) = GetCompleteBarcode();
        barcodeString = barcodeString[..^SpecialCharacters.Trailer.Length]; // Remove the trailer
        expectedParsedBarcode.CompliantTrailer = false;

        var parser = new EciaBarcodeParser();
        var actualParsedBarcode = parser.ParseBarcode(barcodeString);

        actualParsedBarcode.Should().BeEquivalentTo(expectedParsedBarcode);
    }

    [TestCase(LabelFormat.Unknown)]
    [TestCase(LabelFormat.Product)]
    [TestCase(LabelFormat.Intermediate)]
    [TestCase(LabelFormat.Logistic, nameof(ParsedBarcode.CustomerPo), nameof(ParsedBarcode.PackageIdLogisticLabel))]
    [TestCase(LabelFormat.PackingSlip, nameof(ParsedBarcode.CustomerPo), nameof(ParsedBarcode.PackingListNumber))]
    public void ValidateParsedBarcodeCompliance_MissingRequiredFields(LabelFormat label, params string[] requiredFields)
    {
        requiredFields = [.. requiredFields, .. AlwaysRequiredFields];
        var emptyBarcode = new ParsedBarcode();

        var parser = new EciaBarcodeParser();
        var validationResult = parser.ValidateParsedBarcodeCompliance(emptyBarcode, label);


        validationResult.Success.Should().BeFalse();
        foreach (var requiredField in requiredFields)
        {
            validationResult.Errors.ContainsKey(requiredField).Should().BeTrue();
            validationResult.Errors[requiredField].Should().Contain("Field is required.");
        }
    }

    [TestCase("20240829", true)]    // Valid date: August 29, 2024
    [TestCase("19991231", true)]    // Valid date: December 31, 1999
    [TestCase("20240401", true)]    // Valid date: April 1, 2024
    [TestCase("20231301", false)]   // Invalid month: 13 is not a valid month
    [TestCase("2340", false)]       // Invalid format: YYWW
    [TestCase("20230800", false)]   // Invalid day: 00 is not a valid day
    [TestCase("20231501", false)]   // Invalid month: 15 is not a valid month
    [TestCase("abcd0815", false)]   // Non-numeric year: Year must be numeric
    public void ValidateParsedBarcodeCompliance_ShipDateFormat(string value, bool success)
    {
        var parsedBarcode = new ParsedBarcode()
        {
            ShipDate = value
        };

        var parser = new EciaBarcodeParser();
        var validationResult = parser.ValidateParsedBarcodeCompliance(parsedBarcode);

        if (success)
        {
            validationResult.Errors.ContainsKey(nameof(ParsedBarcode.ShipDate)).Should().BeFalse();
        }
        else
        {
            validationResult.Errors.ContainsKey(nameof(ParsedBarcode.ShipDate)).Should().BeTrue();
            validationResult.Errors[nameof(ParsedBarcode.ShipDate)].Should().Contain("Value is not formatted correctly.");
        }
    }

    [TestCase("2301", true)]     // Valid date code: year 23, week 01
    [TestCase("2301M", true)]    // Valid date code with optional M: year 23, week 01
    [TestCase("2019", true)]     // Valid date code: year 20, week 19
    [TestCase("N/T", true)]      // Valid literal string
    [TestCase("MIXED", true)]    // Valid literal string
    [TestCase("2060", false)]    // Invalid date code: week is too high
    [TestCase("N/T/1", false)]   // Invalid format: extra characters
    [TestCase("MIXEDUP", false)] // Invalid format: extra characters
    [TestCase("241M", false)]    // Invalid format: missing week digit
    [TestCase("241", false)]     // Invalid format: missing week digit
    public void ValidateParsedBarcodeCompliance_DateCodeFormat(string value, bool success)
    {
        var parsedBarcode = new ParsedBarcode()
        {
            DateCode = value
        };

        var parser = new EciaBarcodeParser();
        var validationResult = parser.ValidateParsedBarcodeCompliance(parsedBarcode);

        if (success)
        {
            validationResult.Errors.ContainsKey(nameof(ParsedBarcode.DateCode)).Should().BeFalse();
        }
        else
        {
            validationResult.Errors.ContainsKey(nameof(ParsedBarcode.DateCode)).Should().BeTrue();
            validationResult.Errors[nameof(ParsedBarcode.DateCode)].Should().Contain("Value is not formatted correctly.");
        }
    }

    [TestCase("1/2", true)]
    [TestCase("10/20", true)]
    [TestCase("3 of 5", true)]
    [TestCase("12 of 34", true)]
    [TestCase("0/100", true)]
    [TestCase("99 of 100", true)]
    [TestCase("1/", false)]
    [TestCase("/2", false)]
    [TestCase("1 of two", false)]
    [TestCase("ten/20", false)]
    [TestCase("1 of", false)]
    public void ValidateParsedBarcodeCompliance_PackageCountFormat(string value, bool success)
    {
        var parsedBarcode = new ParsedBarcode()
        {
            PackageCount = value
        };

        var parser = new EciaBarcodeParser();
        var validationResult = parser.ValidateParsedBarcodeCompliance(parsedBarcode);

        if (success)
        {
            validationResult.Errors.ContainsKey(nameof(ParsedBarcode.PackageCount)).Should().BeFalse();
        }
        else
        {
            validationResult.Errors.ContainsKey(nameof(ParsedBarcode.PackageCount)).Should().BeTrue();
            validationResult.Errors[nameof(ParsedBarcode.PackageCount)].Should().Contain("Value is not formatted correctly.");
        }
    }

    [Test]
    public void ValidateParsedBarcodeCompliance_OverMaxLength()
    {
        var parsedBarcode = new ParsedBarcode
        {
            CustomerPo = "1234567890123456789012345A", // 26 characters
            PackageIdIntermediateLabel = "1234567890123456789012345A", // 26 characters
            PackageIdLogisticLabel = "1234567890123456789012345A", // 26 characters
            PackingListNumber = "1234567890123456789012345A", // 26 characters
            ShipDate = "20240829A", // 9 characters
            CustomerPartNumber = "1234567890123456789012345678901234567890A", // 41 characters
            SupplierPartNumber = "1234567890123456789012345678901234567890A", // 41 characters
            CustomerPoLine = "1234567890123456789012345678901234567890A", // 41 characters
            Quantity = "123456789A", // 10 characters
            DateCode = "2203011A", // 8 characters
            LotCode = "12345678901234567890A", // 21 characters
            CountryOfOrigin = "USA", // 3 characters
            SerialNumber = "1234567890123456789012345A", // 26 characters
            BinCode = "123456789012345678901234567890123456789012345A", // 36 characters
            RevisionNumber = "123456A", // 7 characters
            CompliantHeader = true,
            CompliantTrailer = true
        };

        var parser = new EciaBarcodeParser();
        var validationResult = parser.ValidateParsedBarcodeCompliance(parsedBarcode);

        validationResult.Errors.Count(x => x.Value.StartsWith("Exceeds max length")).Should().Be(15);
    }

    [Test]
    public void ValidateParsedBarcodeCompliance_Success()
    {
        (var expectedParsedBarcode, _) = GetCompleteBarcode();

        var parser = new EciaBarcodeParser();
        var validationResult = parser.ValidateParsedBarcodeCompliance(expectedParsedBarcode);

        validationResult.Success.Should().BeTrue();
    }

    private static (ParsedBarcode parsedBarcode, string barcodeString) GetCompleteBarcode()
    {
        var parsedBarcode = new ParsedBarcode()
        {
            CustomerPo = "11111",
            PackageIdIntermediateLabel = "22222",
            PackageIdLogisticLabel = "3333",
            PackingListNumber = "444",
            ShipDate = "20240823",
            CustomerPartNumber = "customer-part",
            SupplierPartNumber = "supplier-part",
            CustomerPoLine = "5",
            Quantity = "6666",
            DateCode = "2248",
            LotCode = "LOT",
            CountryOfOrigin = "US",
            SerialNumber = "7777",
            BinCode = "Bin",
            PackageCount = "8 of 9",
            RevisionNumber = "AAA",
            Weight = "10 lbs",
            Manufacturer = "mfg",
            RohsCC = "Compliant",
            CompliantHeader = true,
            CompliantTrailer = true
        };

        var barcodeString = $"{SpecialCharacters.Header}" +
            $"{DataIdentifiers.CustomerPo}{parsedBarcode.CustomerPo}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.PackageIdIntermediateLabel}{parsedBarcode.PackageIdIntermediateLabel}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.PackageIdLogisticLabel}{parsedBarcode.PackageIdLogisticLabel}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.PackingListNumber}{parsedBarcode.PackingListNumber}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.ShipDate}{parsedBarcode.ShipDate}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.CustomerPartNumber}{parsedBarcode.CustomerPartNumber}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.SupplierPartNumber}{parsedBarcode.SupplierPartNumber}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.CustomerPoLine}{parsedBarcode.CustomerPoLine}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.Quantity}{parsedBarcode.Quantity}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.DateCode9D}{parsedBarcode.DateCode}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.LotCode}{parsedBarcode.LotCode}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.CountryOfOrigin}{parsedBarcode.CountryOfOrigin}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.SerialNumber}{parsedBarcode.SerialNumber}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.BinCode}{parsedBarcode.BinCode}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.PackageCount}{parsedBarcode.PackageCount}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.RevisionNumber}{parsedBarcode.RevisionNumber}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.Weight}{parsedBarcode.Weight}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.Manufacturer}{parsedBarcode.Manufacturer}{SpecialCharacters.GroupSeparator}" +
            $"{DataIdentifiers.RohsCC}{parsedBarcode.RohsCC}" +
            $"{SpecialCharacters.Trailer}";

        return (parsedBarcode, barcodeString);
    }

    private static readonly string[] AlwaysRequiredFields =
    [
        nameof(ParsedBarcode.SupplierPartNumber),
        nameof(ParsedBarcode.Quantity),
        nameof(ParsedBarcode.DateCode),
        nameof(ParsedBarcode.CountryOfOrigin)
    ];
}
