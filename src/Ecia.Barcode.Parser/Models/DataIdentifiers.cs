namespace Ecia.Barcode.Parser;

/// <summary>
/// ECIA Data Identifiers
/// This class contains constants representing various data identifiers used in ECIA (Electronic Components Industry Association) standards.
/// </summary>
public static class DataIdentifiers
{
    /// <summary>
    /// Identifier for Customer Purchase Order.
    /// </summary>
    public const string CustomerPo = "K";

    /// <summary>
    /// Identifier for Package Id on intermediate labels.
    /// </summary>
    public const string PackageIdIntermediateLabel = "3S";

    /// <summary>
    /// Identifier for Package Id on logistic labels.
    /// </summary>
    public const string PackageIdLogisticLabel = "4S";

    /// <summary>
    /// Identifier for Package Id on mixed logistic labels.
    /// </summary>
    public const string PackageIdLogisticLabelMixed = "5S";

    /// <summary>
    /// Identifier for Packing List Number.
    /// </summary>
    public const string PackingListNumber = "11K";

    /// <summary>
    /// Identifier for Ship Date.
    /// </summary>
    public const string ShipDate = "6D";

    /// <summary>
    /// Identifier for Customer Part Number.
    /// </summary>
    public const string CustomerPartNumber = "P";

    /// <summary>
    /// Identifier for Supplier Part Number.
    /// </summary>
    public const string SupplierPartNumber = "1P";

    /// <summary>
    /// Identifier for Customer Purchase Order Line.
    /// </summary>
    public const string CustomerPoLine = "4K";

    /// <summary>
    /// Identifier for Quantity.
    /// </summary>
    public const string Quantity = "Q";

    /// <summary>
    /// Identifier for Date Code with 9 digits.
    /// </summary>
    public const string DateCode9D = "9D";

    /// <summary>
    /// Identifier for Date Code with 10 digits.
    /// </summary>
    public const string DateCode10D = "10D";

    /// <summary>
    /// Identifier for Lot Code.
    /// </summary>
    public const string LotCode = "1T";

    /// <summary>
    /// Identifier for Country of Origin.
    /// </summary>
    public const string CountryOfOrigin = "4L";

    /// <summary>
    /// Identifier for Serial Number.
    /// </summary>
    public const string SerialNumber = "S";

    /// <summary>
    /// Identifier for Bin Code.
    /// </summary>
    public const string BinCode = "33P";

    /// <summary>
    /// Identifier for Package Count.
    /// </summary>
    public const string PackageCount = "13Q";

    /// <summary>
    /// Identifier for Revision Number.
    /// </summary>
    public const string RevisionNumber = "2P";

    /// <summary>
    /// Identifier for Weight.
    /// </summary>
    public const string Weight = "7Q";

    /// <summary>
    /// Identifier for Manufacturer.
    /// </summary>
    public const string Manufacturer = "1V";

    /// <summary>
    /// Identifier for ROHS Compliance Code.
    /// </summary>
    public const string RohsCC = "E";
}
