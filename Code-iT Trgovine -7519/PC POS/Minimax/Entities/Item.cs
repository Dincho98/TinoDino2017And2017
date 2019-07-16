using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    public class Item : RootObject<Item>
    {
        // Item id.
        // Mandatory field. Ignored on create request. 
        public long ItemId { get; set; }
        // Item name.
        // Mandatory field. Max length: 250 
        public string Name { get; set; }
        // Item code.
        // Max length: 30 
        public string Code { get; set; }
        // EAN code.
        // Max length: 30 
        public string EANCode { get; set; }
        // Item description.
        // Max length: 8000 
        public string Description { get; set; }
        // <br />Item type:
        // <ul>
        //     <li>B – Goods,</li>
        //     <li>M – Material,</li>
        //     <li>P - Semifinished product,</li>
        //     <li>I – Product,</li>
        //     <li>E - Packing material,</li>
        //     <li>NM - Client material,</li>
        //     <li>NP - Client semifinished product,</li>
        //     <li>NI - Client product,</li>
        //     <li>S – Services,</li>
        //     <li>A - Advance payment,</li>
        //     <li>AS - Pre payments for services</li>
        // </ul>
        // Mandatory field. Max length: 2 
        public string ItemType { get; set; }
        // Item unit of measurement.
        // Max length: 3 
        public string UnitOfMeasurement { get; set; }
        // Item VAT rate.
        public mMApiFkField VatRate { get; set; }
        // Item selling price.
        // Mandatory field. 
        public Decimal Price { get; set; }
        // Margin percent.
        // Mandatory field. 
        public Decimal RebatePercent { get; set; }
        // Usage:
        // <ul>
        //     <li>D – yes,</li>
        //     <li>N – no.</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string Usage { get; set; }
        // Selling price currency.
        public mMApiFkField Currency { get; set; }
        // SerialNumbers:
        // <ul>
        //     <li>D – yes,</li>
        //     <li>N – no.</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string SerialNumbers { get; set; }
        // BatchNumbers:
        // <ul>
        //     <li>D – yes,</li>
        //     <li>N – no.</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string BatchNumbers { get; set; }
        // Domestic market revenue account.
        public mMApiFkField RevenueAccountDomestic { get; set; }
        // Revenue account for EU markets.
        public mMApiFkField RevenueAccountEU { get; set; }
        // Revenue account outside EU markets.
        public mMApiFkField RevenueAccountOutsideEU { get; set; }
        // Stock account.
        public mMApiFkField StocksAccount { get; set; }
        public string RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }
    }
}
