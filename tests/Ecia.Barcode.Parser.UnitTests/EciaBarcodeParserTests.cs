
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

    [TestCase("")]
    public void ParseBarcode_NoHeader_StillParses_CompliantHeadedFalse(string badHeader)
    {
        (var expectedParsedBarcode, var barcodeString) = GetCompleteBarcode();
        barcodeString = barcodeString[SpecialCharacters.Header.Length..]; // Remove the good header
        barcodeString = badHeader + barcodeString;
        expectedParsedBarcode.CompliantHeader = false;

        var parser = new EciaBarcodeParser();
        var actualParsedBarcode = parser.ParseBarcode(barcodeString);

        actualParsedBarcode.Should().BeEquivalentTo(expectedParsedBarcode);
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
}
