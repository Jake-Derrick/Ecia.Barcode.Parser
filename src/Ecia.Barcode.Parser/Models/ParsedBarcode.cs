namespace Ecia.Barcode.Parser;

/// <summary>
/// Represents a parsed barcode with various data fields.
/// </summary>
public class ParsedBarcode
{
    /// <summary>
    /// Customer Purchase Order.
    /// </summary>
    public string? CustomerPo { get; set; }

    /// <summary>
    /// Package Id for intermediate labels.
    /// </summary>
    public string? PackageIdIntermediateLabel { get; set; }

    /// <summary>
    /// Package Id for logistic labels.
    /// </summary>
    public string? PackageIdLogisticLabel { get; set; }

    /// <summary>
    /// Packing List Number.
    /// </summary>
    public string? PackingListNumber { get; set; }

    /// <summary>
    /// Ship Date.
    /// </summary>
    public string? ShipDate { get; set; }

    /// <summary>
    /// Customer Part Number.
    /// </summary>
    public string? CustomerPartNumber { get; set; }

    /// <summary>
    /// Supplier Part Number.
    /// </summary>
    public string? SupplierPartNumber { get; set; }

    /// <summary>
    /// Customer Purchase Order Line.
    /// </summary>
    public string? CustomerPoLine { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public string? Quantity { get; set; }

    /// <summary>
    /// Date Code.
    /// </summary>
    public string? DateCode { get; set; }

    /// <summary>
    /// Lot Code.
    /// </summary>
    public string? LotCode { get; set; }

    /// <summary>
    /// Country of Origin.
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// Serial Number.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Bin Code.
    /// </summary>
    public string? BinCode { get; set; }

    /// <summary>
    /// Package Count.
    /// </summary>
    public string? PackageCount { get; set; }

    /// <summary>
    /// Revision Number.
    /// </summary>
    public string? RevisionNumber { get; set; }

    /// <summary>
    /// Weight.
    /// </summary>
    public string? Weight { get; set; }

    /// <summary>
    /// Manufacturer.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// ROHS Compliance Code.
    /// </summary>
    public string? RohsCC { get; set; }

    /// <summary>
    /// Indicates whether the header was compliant.
    /// </summary>
    public bool CompliantHeader { get; set; }

    /// <summary>
    /// Indicates whether the trailer was compliant.
    /// </summary>
    public bool CompliantTrailer { get; set; }
}
