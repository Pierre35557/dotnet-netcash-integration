namespace Netcash.DTO.Responses
{
    public class NetcashMandateWebhookResponse
    {
        // Account Information
        public string AccountRef { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
        public bool IsFromWebService { get; set; }
        public int MandateStatus { get; set; }

        // Company Information
        public string CompTradingName { get; set; }
        public string CompRegName { get; set; }
        public string CompRegNo { get; set; }

        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string CellNo { get; set; }
        public string TelephoneNumber { get; set; }
        public bool IsRSAId { get; set; }
        public string IdentityNumber { get; set; }

        // Physical Address
        public string PhysicalComplex { get; set; }
        public string PhsicalStreetAddress { get; set; }
        public string PhysicalSuburb { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalAddressPostcode { get; set; }

        // Custom Fields
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }

        // Notification Preferences
        public string NotificationEmail { get; set; }
        public bool NotificationByEmailActive { get; set; }
        public string NotificationCellNo { get; set; }
        public bool NotificationByCellNoActive { get; set; }

        // Mandate Details
        public DateTime AgreementDate { get; set; }
        public string DebitDay { get; set; }
        public string DecemberDebitDay { get; set; }
        public string MandateReferenceNumber { get; set; }
        public int NoticeDays { get; set; }
        public int LuMandatePublicHolidayOptionId { get; set; }
        public int MandateDebitFrequencyId { get; set; }
        public bool DoAVS { get; set; }

        // Mandate Amount (in cents)
        public string DefaultAmount { get; set; }
        public bool AllowVariableAmounts { get; set; }

        // Signature Details
        public string SignBy_FirstName { get; set; }
        public string SignBy_LastName { get; set; }
        public string SignBy_Email { get; set; }
        public string SignBy_CellNo { get; set; }

        // Credit Card / Decline Details
        public bool IsCreditCard { get; set; }
        public int IsDeclined { get; set; }
        public string ReasonForDecline { get; set; }

        // Mandate Status & Result
        public int MandateSuccessful { get; set; }

        // Additional Information
        public string AdditionalClauses { get; set; }
        public string MandatePDFLink { get; set; }
    }

}
