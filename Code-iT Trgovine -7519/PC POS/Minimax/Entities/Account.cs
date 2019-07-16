using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    public class Account
    {
        // Account id.
        // Mandatory field. Ignored on create request. 
        public long AccountId { get; set; }
        // Account code.
        public string Code { get; set; }
        // Account name.
        public string Name { get; set; }
        // Other language account name.
        public string NameInOtherLanguage { get; set; }
        // English account name.
        public string NameInEnglish { get; set; }
        // Account description.
        public string Description { get; set; }
        // Posting side:
        // <ul>
        //     <li>V – debit and credit,</li>
        //     <li>B – only debit,</li>
        //     <li>D – only credit,</li>
        //     <li>N – Posting is not allowed.</li>
        // </ul>
        public string AllowedPosting { get; set; }
        // Account posting:
        // <ul>
        //     <li>N –  No account,</li>
        //     <li>B – debit</li>
        //     <li>D – credit</li>
        // </ul>
        // 
        public string InvoiceAccounting { get; set; }
        // Analytics entry:
        // <ul>
        //     <li>V – Input allowed,</li>
        //     <li>N – Input not allowed,</li>
        //     <li>D – Mandatory input.</li>
        // </ul>
        // 
        public string AnalyticsEntry { get; set; }
        // Employee entry:
        // <ul>
        //     <li>V – Input allowed,</li>
        //     <li>N – Input not allowed,</li>
        //     <li>D – Mandatory input.</li>
        // </ul>
        // 
        public string EmployeeEntry { get; set; }
        // Customer entry:
        // <ul>
        //     <li>V – Input allowed,</li>
        //     <li>N – Input not allowed,</li>
        //     <li>D – Mandatory input.</li>
        // </ul>
        // 
        public string CustomerEntry { get; set; }
        // Unrecognized account in terms of tax:
        // <ul>
        //     <li>N – No,</li>
        //     <li>D – Yes.</li>
        // </ul>
        // 
        public string NonTaxable { get; set; }
        // Application:
        // <ul>
        //     <li>N – No,</li>
        //     <li>D – Yes.</li>
        // </ul>
        // 
        public string Application { get; set; }
        // Account validity from year.
        public int ValidFromYear { get; set; }
        // Account validity to year.
        public int ValidToYear { get; set; }
        public DateTime RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }

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
}
