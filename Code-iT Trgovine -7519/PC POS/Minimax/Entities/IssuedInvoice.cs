using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    public class IssuedInvoice : RootObject<IssuedInvoice>
    {
        // Issued invoice id.
        // Mandatory field. Ignored on create request. 
        public long IssuedInvoiceId { get; set; }
        // Invoice year.
        public int? Year { get; set; }
        // Invoice number.
        public long? InvoiceNumber { get; set; }
        // Code from numbering codelist.
        // Max length: 30 
        public string Numbering { get; set; }
        // Document numbering.
        public mMApiFkField DocumentNumbering { get; set; }
        // Customer.
        public mMApiFkField Customer { get; set; }
        // Invoice date.
        // Mandatory field. 
        public string DateIssued { get; set; }
        // Date of transaction.
        public string DateTransaction { get; set; }
        // Start date of transaction.
        public string DateTransactionFrom { get; set; }
        // Invoice due date.
        // Mandatory field. 
        public string DateDue { get; set; }
        // Addressee name.
        // Max length: 250 
        public string AddresseeName { get; set; }
        // Addressee address.
        // Max length: 250 
        public string AddresseeAddress { get; set; }
        // Addressee postal code.
        // Max length: 30 
        public string AddresseePostalCode { get; set; }
        // Addressee city.
        // Max length: 250 
        public string AddresseeCity { get; set; }
        // Addressee country name. Prohibited use when AddresseeCountry is set as home country.
        // Max length: 250 
        public string AddresseeCountryName { get; set; }
        // Addressee country.
        public mMApiFkField AddresseeCountry { get; set; }
        // Recipient name.
        // Max length: 250 
        public string RecipientName { get; set; }
        // Recipient address.
        // Max length: 250 
        public string RecipientAddress { get; set; }
        // Recipient postal code.
        // Max length: 30 
        public string RecipientPostalCode { get; set; }
        // Recipient city.
        // Max length: 250 
        public string RecipientCity { get; set; }
        // Recipient country name. Prohibited use when RecipientCountry is set as home country.
        // Max length: 250 
        public string RecipientCountryName { get; set; }
        // Recipient country.
        public mMApiFkField RecipientCountry { get; set; }
        // Rabate percent.
        public Decimal Rabate { get; set; }
        // Exchange rate.
        public Decimal ExchangeRate { get; set; }
        // Document reference.
        public string DocumentReference { get; set; }
        // Payment reference.
        public string PaymentReference { get; set; }
        // Currency.
        public mMApiFkField Currency { get; set; }
        // Analytic.
        public mMApiFkField Analytic { get; set; }
        // Document.
        public mMApiFkField Document { get; set; }
        // Report settings for issued invoices:
        // <ul>
        //     <li>IR – for issued invoice,</li>
        //     <li>DP – for credit note,</li>
        //     <li>UP – for issued invoice with order for payment.</li>
        // </ul>
        // Report settings for proforma invoices:
        // <ul>
        //     <li>PR – for proforma invoice,</li>
        //     <li>PUPN – for proforma invoice with order for payment.</li>
        // </ul>
        public mMApiFkField IssuedInvoiceReportTemplate { get; set; }
        // Report settings for delivery note:
        // <ul>
        //     <li>DO – for delivery note </li>
        // </ul>
        public mMApiFkField DeliveryNoteReportTemplate { get; set; }
        // Issued invoice and proforma invoice status:
        // <ul>
        //     <li>O – Draft,</li>
        //     <li>I - Issued</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string Status { get; set; }
        // The description that appears on issued invoice and proforma invoice above.
        // Max length: 8000 
        public string DescriptionAbove { get; set; }
        // The description that appears on issued invoice and proforma invoice below.
        // Max length: 8000 
        public string DescriptionBelow { get; set; }
        // The description that appears on delivery note above.
        // Max length: 8000 
        public string DeliveryNoteDescriptionAbove { get; set; }
        // The description that appears on delivery note below.
        // Max length: 8000 
        public string DeliveryNoteDescriptionBelow { get; set; }
        // Notes.
        // Max length: 1000 
        public string Notes { get; set; }
        // Payment type. Data input for cash payed invoices:
        // <ul>
        //     <li>G - Cash</li>
        //     <li>K - Credit card</li>
        //     <li>T – Transaction account</li>
        //     <li>O - Other</li>
        // </ul>
        // Max length: 250 
        public string PaymentType { get; set; }
        // Employee.
        public mMApiFkField Employee { get; set; }
        // Price calculation type(VAT):
        // <ul>
        //     <li>D - VAT is included in the price</li>
        //     <li>N - VAT is added to the prices</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string PricesOnInvoice { get; set; }
        // Recurring Invoice:
        // <ul>
        //     <li>D – yes,</li>
        //     <li>N – no.</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string RecurringInvoice { get; set; }
        // Sales value(for retail trade turnover).
        public Decimal SalesValue { get; set; }
        // VAT value ( for retail trade turnover).
        public Decimal SalesValueVAT { get; set; }
        // Invoice attachment (PDF invoice document).
        public mMApiFkField InvoiceAttachment { get; set; }
        public mMApiFkField EInvoiceAttachment { get; set; }
        // Input type:
        // <ul>
        //     <li>R – issued invoice,</li>
        //     <li>P – proforma invoice.</li>
        // </ul>
        // Mandatory field. Max length: 1 
        public string InvoiceType { get; set; }
        // Payment status:
        // <ul>
        //     <li>Placan – Paid</li>
        //     <li>DelnoZapadel – Partially paid, Overdue</li>
        //     <li>DelnoNezapadel – Partially paid, Not yet due</li>
        //     <li>NeplacanZapadel – Unpaid, Overdue</li>
        //     <li>NeplacanNezapadel – Unpaid, Not yet due</li>
        //     <li>Osnutek – Draft</li>
        //     <li>Avans – Advance payment</li>
        // </ul>
        // Mandatory field. Max length: 20 
        public string PaymentStatus { get; set; }
        // Invoice value (domestic currency).
        public Decimal InvoiceValue { get; set; }
        // Paid value (domestic currency).
        public Decimal PaidValue { get; set; }
        // Invoice rows .
        public List<IssuedInvoiceRow> IssuedInvoiceRows { get; set; }
        public string RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }

    }
}
