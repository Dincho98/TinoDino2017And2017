using System;

namespace PCPOS.Minimax.Entities
{
    public class ReportTemplate
    {
        // Report template id.
        // Mandatory field. Ignored on create request. 
        public long ReportTemplateId { get; set; }
        // Report template name.
        public string Name { get; set; }
        // Report template display type:
        // <ul>
        //     <li>BB - Gross balance</li>
        //     <li>BK - Compensation</li>
        //     <li>DN - Work order</li>
        //     <li>DO - Delivery note</li>
        //     <li>DOP - Calculation sheet</li>
        //     <li>DP – credit note</li>
        //     <li>IN – Issued order</li>
        //     <li>IO – Open items report</li>
        //     <li>IR – Issued invoice</li>
        //     <li>OP - Payment reminder</li>
        //     <li>PL - Salaries, payslips</li>
        //     <li>PN – Received order confirmation</li>
        //     <li>PUPN – proforma invoice with order for payment</li>
        //     <li>UP - Issued invoice with order for payment</li>
        //     <li>ZO - Interest for delay.</li>
        // <ul>
        public string DisplayType { get; set; }
        // Default report template:
        // <ul>
        //     <li>D - Yes,</li>
        //     <li>N - No.</li>
        // <ul>
        public string Default { get; set; }
        public DateTime RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }

    }
}
