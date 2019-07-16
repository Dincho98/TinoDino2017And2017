using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    public class IssuedInvoiceRow
    {
        // Issued invoice id.
        // Mandatory field. Ignored on create request. 
        public long IssuedInvoiceRowId { get; set; }
        // Issued invoice.
        public mMApiFkField IssuedInvoice { get; set; }
        // Item.
        public mMApiFkField Item { get; set; }
        // Item name.
        // Max length: 250 
        public string ItemName { get; set; }
        // Issued invoice row number.
        // Mandatory field. 
        public int? RowNumber { get; set; }
        // Item code.
        // Max length: 30 
        public string ItemCode { get; set; }
        // Serial number.
        // Max length: 30 
        public string SerialNumber { get; set; }
        // Batch number.
        // Max length: 30 
        public string BatchNumber { get; set; }
        // Item description.
        // Max length: 8000 
        public string Description { get; set; }
        // Item quantity.
        // Mandatory field. 
        public Decimal Quantity { get; set; }
        // Item unit of measurement.
        // Max length: 3 
        public string UnitOfMeasurement { get; set; }
        // Price.
        // Mandatory field. 
        public Decimal Price { get; set; }
        // Price with included VAT.
        // Mandatory field. 
        public Decimal PriceWithVAT { get; set; }
        // Item VAT percent.
        // Mandatory field. 
        public Decimal VATPercent { get; set; }
        // Discount value in currency.
        // Mandatory field. 
        public Decimal Discount { get; set; }
        // Discount value in percent.
        // Mandatory field. 
        public Decimal DiscountPercent { get; set; }
        // Row value.
        // Mandatory field. 
        public Decimal Value { get; set; }
        // Item VAT rate.
        public mMApiFkField VatRate { get; set; }
        // Warehouse. Input allowed if settings are set for direct discharge(Inventory).
        public mMApiFkField Warehouse { get; set; }
        // VAT accounting type.
        // Max length: 5 
        public string VatAccountingType { get; set; }
        public string RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }

    }
}
