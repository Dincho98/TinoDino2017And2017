using System;

namespace PCPOS.Minimax.Entities
{
    public class Employee
    {
        // Employee id.
        // Mandatory field. Ignored on create request. 
        public long EmployeeId { get; set; }
        // Employee code.
        // Max length: 10 
        public string Code { get; set; }
        // Employee Tax number.
        // Max length: 12 
        public string TaxNumber { get; set; }
        // Employee first name.
        // Mandatory field. Max length: 30 
        public string FirstName { get; set; }
        // Employee last name.
        // Mandatory field. Max length: 250 
        public string LastName { get; set; }
        // Employee address.
        // Max length: 250 
        public string Address { get; set; }
        // Employee postal code.
        // Max length: 30 
        public string PostalCode { get; set; }
        // Employee city.
        // Max length: 250 
        public string City { get; set; }
        // Employee country.
        public mMApiFkField Country { get; set; }
        // Employee residence country.
        public mMApiFkField CountryOfResidence { get; set; }
        // Employee date of birth.
        public DateTime? DateOfBirth { get; set; }
        // Employee gender:
        // <ul>
        //     <li>M - Man</li>
        //     <li>Z - Woman</li>
        // </ul>
        // 
        // Mandatory field. Max length: 1 
        public string Gender { get; set; }
        // Date of employment.
        public DateTime? EmploymentStartDate { get; set; }
        // Employment end date.
        public DateTime? EmploymentEndDate { get; set; }
        // Notes.
        public string Notes { get; set; }
        // Employment type:
        // <ul>
        //     <li>ZD - Employed worker</li>
        //     <li>SSD - A regular seasonal worker</li>
        //     <li>SZ - The self-employed</li>
        //     <li>CU - A board member and an executive director</li>
        //     <li>VO - A volunteer</li>
        //     <li>PENZ - Pensioner</li>
        //     <li>ZAP - Employed elsewhere</li>
        //     <li>DD - Seconded worker</li>
        // </ul>
        // 
        // Mandatory field. Max length: 30 
        public string EmploymentType { get; set; }
        // Employee Personal identification number.
        // Max length: 30 
        public string PersonalIdenficationNumber { get; set; }
        // Employee Insurance base for employment type SZ and organisation type »Obrtnik«:
        // <ul>
        //     <li>OB – Craft,</li>
        //     <li>SZ - Liberal professions,</li>
        //     <li>SP - An athlete,</li>
        //     <li>PG - A farmer and a forester,</li>
        //     <li>SD - Other independent business activities,</li>
        //     <li>SU - Freelance artists</li>
        // </ul>
        // Max length: 30 
        public string InsuranceBasis { get; set; }
        public DateTime RecordDtModified { get; set; }
        // Row version is used for concurrency check.
        // Mandatory field. Ignored on create request. 
        public string RowVersion { get; set; }

    }
}
