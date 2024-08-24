namespace Ecia.Barcode.Parser;

public class ParsedBarcode
{
    public string? CustomerPo { get; set; }
    public string? PackageIdIntermediateLabel { get; set; }
    public string? PackageIdLogisticLabel { get; set; }
    public string? PackingListNumber { get; set; }
    public string? ShipDate { get; set; }
    public string? CustomerPartNumber { get; set; }
    public string? SupplierPartNumber { get; set; }
    public string? CustomerPoLine { get; set; }
    public string? Quantity { get; set; }
    public string? DateCode { get; set; }
    public string? LotCode { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? SerialNumber { get; set; }
    public string? BinCode { get; set; }
    public string? PackageCount { get; set; }
    public string? RevisionNumber { get; set; }
    public string? Weight { get; set; }
    public string? Manufacturer { get; set; }
    public string? RohsCC { get; set; }

    public bool CompliantHeader { get; set; }
    public bool CompliantTrailer { get; set; }
}
