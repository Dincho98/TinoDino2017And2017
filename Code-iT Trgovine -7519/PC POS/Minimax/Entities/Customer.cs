using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    public class Customer : RootObject<Customer>
    {
        // Customer id.
        // Mandatory field. Ignored on create request. 
        public long CustomerId { get; set; }
        // Customer code, unique within organisation.
        // Max length: 10 
        public string Code { get; set; }
        // Customer name.
        // Max length: 250 
        public string Name { get; set; }
        // Customer address.
        // Max length: 250 
        public string Address { get; set; }
        // Customer postal code.
        // Max length: 30 
        public string PostalCode { get; set; }
        // Customer city.
        // Max length: 250 
        public string City { get; set; }
        // Customer country.
        public mMApiFkField Country { get; set; }
        // Country name.
        // Max length: 250 
        public string CountryName { get; set; }
        // Customer tax number.
        // Max length: 30 
        public string TaxNumber { get; set; }
        public string InternalCustomerNumber { get; set; }
        public string EInvoiceIssuing { get; set; }
        // Customer registration number.
        // Max length: 30 
        public string RegistrationNumber { get; set; }
        // Customer VAT Identification Number.
        // Max length: 30 
        public string VATIdentificationNumber { get; set; }
        // Customer VAT settings.<br/>
        // For EU customers:
        // <ul>
        //     <li>D - Legal person or a person with business who is subject to VAT,</li>
        //     <li>M - Legal person or a person with business who is NOT subject to VAT,</li>
        //     <li>N – end user.</li>
        // </ul>
        // For customers outside EU:
        // <ul>
        //     <li>D - Legal person (VAT on the issued invoice is not to be accounted for),</li>
        //     <li>N – end user.</li>
        // </ul>
        // 
        // Mandatory field. Max length: 1 
        public string SubjectToVAT { get; set; }
        // Default currency.
        public mMApiFkField Currency { get; set; }
        // Invoice expiration days.
        // Mandatory field. 
        public int ExpirationDays { get; set; }
        // Rebate (%)
        // Mandatory field. 
        public Decimal RebatePercent { get; set; }
        // Web site.
        // Max length: 100 
        public string WebSiteURL { get; set; }
        // Usage:
        // <ul>
        //     <li>D - Yes</li>
        //     <li>N - No</li>
        // </ul>
        // 
        // Mandatory field. Max length: 1 
        public string Usage { get; set; }
        public string RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }

    }

    /// Link with id, name and url to related data.
    public class mMApiFkField
    {
        // Record id.
        public long? ID { get; set; }
        // Record name.
        public string Name { get; set; }
        // Url to full record details.
        public string ResourceUrl { get; set; }

    }
}
