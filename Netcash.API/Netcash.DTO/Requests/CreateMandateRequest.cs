using System.ComponentModel.DataAnnotations;

namespace Netcash.DTO.Requests
{
    /// <summary>
    /// Represents a request to create an eMandate with Netcash.
    /// This request is used to initiate a debit order mandate.
    /// </summary>
    public class CreateMandateRequest
    {
        /// <summary>
        /// A unique reference used to identify this mandate in future transactions.
        /// </summary>
        [Required]
        [StringLength(22, MinimumLength = 2, ErrorMessage = "AccountReference must be between 2 and 22 characters.")]
        public string AccountReference { get; set; }

        /// <summary>
        /// The name of your client associated with this mandate.
        /// </summary>
        [Required]
        public string MandateName { get; set; }

        /// <summary>
        /// The amount authorized in the mandate.
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "MandateAmount must be a positive value.")]
        public decimal MandateAmount { get; set; }

        /// <summary>
        /// Indicates whether the mandate is for an individual (true) or a business (false).
        /// </summary>
        [Required]
        public bool IsConsumer { get; set; }

        /// <summary>
        /// First name of the account holder (Required if IsConsumer is true).
        /// </summary>
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Surname of the account holder (Required if IsConsumer is true).
        /// </summary>
        [StringLength(50)]
        public string Surname { get; set; }

        /// <summary>
        /// Business trading name (Required if IsConsumer is false).
        /// </summary>
        [StringLength(100)]
        public string TradingName { get; set; }

        /// <summary>
        /// Business registration number (Required if IsConsumer is false).
        /// </summary>
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Business registered name (Required if IsConsumer is false).
        /// </summary>
        [StringLength(100)]
        public string RegisteredName { get; set; }

        /// <summary>
        /// Mobile phone number of the account holder.
        /// </summary>
        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        /// <summary>
        /// The frequency at which debits should occur.
        /// 1 = Monthly, 2 = Bimonthly, 3 = ThreeMonthly, 4 = SixMonthly, 5 = Annually, 6 = Weekly, 7 = Biweekly.
        /// </summary>
        [Required]
        [Range(1, 7, ErrorMessage = "Invalid DebitFrequency value.")]
        public int DebitFrequency { get; set; }

        /// <summary>
        /// The month when debits should commence (1 = January, 12 = December).
        /// </summary>
        [Required]
        [Range(1, 12, ErrorMessage = "CommencementMonth must be between 1 and 12.")]
        public int CommencementMonth { get; set; }

        /// <summary>
        /// The day(s) of the month for debits (e.g., "05/15/25/LDOM" for 5th, 15th, 25th, and Last Day of Month).
        /// </summary>
        [Required]
        public string CommencementDay { get; set; }

        /// <summary>
        /// The date when the agreement was made (Format: YYYYMMDD, e.g., 20250301 for March 1, 2025).
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "AgreementDate must be in YYYYMMDD format.")]
        public string AgreementDate { get; set; }

        /// <summary>
        /// A reference number for the agreement.
        /// </summary>
        [Required]
        public string AgreementReferenceNumber { get; set; }

        /// <summary>
        /// Notice period for cancellation (Optional, range: 1 to 60 days).
        /// </summary>
        [Range(1, 60, ErrorMessage = "CancellationNoticePeriod must be between 1 and 60.")]
        public int? CancellationNoticePeriod { get; set; }

        /// <summary>
        /// Defines the action to take if a debit falls on a public holiday.
        /// 1 = Preceding Business Day, 2 = Next Business Day.
        /// </summary>
        [Range(1, 2, ErrorMessage = "Invalid PublicHolidayOption value.")]
        public int? PublicHolidayOption { get; set; }

        /// <summary>
        /// Additional notes related to the mandate.
        /// </summary>
        [StringLength(255)]
        public string Notes { get; set; }

        /// <summary>
        /// The bank account name.
        /// </summary>
        [StringLength(50)]
        public string BankAccountName { get; set; }

        /// <summary>
        /// The bank account number.
        /// </summary>
        [StringLength(50)]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// The branch code of the bank.
        /// </summary>
        [StringLength(10)]
        public string BranchCode { get; set; }

        /// <summary>
        /// Type of bank account.
        /// 1 = Current, 2 = Savings, 3 = Transmission.
        /// </summary>
        [Range(1, 3, ErrorMessage = "Invalid BankAccountType value.")]
        public int? BankAccountType { get; set; }

        /// <summary>
        /// Email address of the account holder.
        /// </summary>
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Physical address of the account holder.
        /// </summary>
        [StringLength(255)]
        public string PhysicalAddress { get; set; }

        /// <summary>
        /// Custom field 1 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field1 { get; set; }

        /// <summary>
        /// Custom field 2 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field2 { get; set; }

        /// <summary>
        /// Custom field 3 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field3 { get; set; }

        /// <summary>
        /// Custom field 4 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field4 { get; set; }

        /// <summary>
        /// Custom field 5 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field5 { get; set; }

        /// <summary>
        /// Custom field 6 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field6 { get; set; }

        /// <summary>
        /// Custom field 7 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field7 { get; set; }

        /// <summary>
        /// Custom field 8 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field8 { get; set; }

        /// <summary>
        /// Custom field 9 for additional data.
        /// </summary>
        [StringLength(50)]
        public string Field9 { get; set; }

        /// <summary>
        /// Indicates if variable debit amounts are allowed.
        /// </summary>
        public bool AllowVariableDebitAmounts { get; set; }

        /// <summary>
        /// Indicates whether an Account Verification Service (AVS) check should be performed.
        /// false = No AVS check, true = Perform AVS check.
        /// </summary>
        public bool? RequestAVS { get; set; }

        /// <summary>
        /// The ID number to be used on the mandate, regardless if RequestAVS is true or false
        /// </summary>
        public string AVSCheckNumber { get; set; }

        /// <summary>
        /// Indicates whether to include DebiCheck authentication with the EFT mandate.
        /// false = EFT mandate only, true = Include DebiCheck.
        /// </summary>
        public bool IncludeDebiCheck { get; set; }

        /// <summary>
        /// The ID of a predefined DebiCheck mandate template.
        /// </summary>
        public string DebiCheckMandateTemplateId { get; set; }

        /// <summary>
        /// The amount of the DebiCheck mandate.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "DebiCheckCollectionAmount must be a positive value.")]
        public decimal? DebiCheckCollectionAmount { get; set; }

        /// <summary>
        /// Indicates whether the first collection differs from the mandate amount.
        /// false = First collection is the same as mandate, true = First collection is different.
        /// </summary>
        public bool? DebiCheckFirstCollectionDiffers { get; set; }

        /// <summary>
        /// The first collection date for the DebiCheck mandate (Format: YYYYMMDD).
        /// </summary>
        [RegularExpression(@"^\d{8}$", ErrorMessage = "DebiCheckFirstCollectionDate must be in YYYYMMDD format.")]
        public string DebiCheckFirstCollectionDate { get; set; }


        /// <summary>
        /// The amount of the first DebiCheck collection.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "DebiCheckFirstCollectionAmount must be a positive value.")]
        public decimal? DebiCheckFirstCollectionAmount { get; set; }

        /// <summary>
        /// The collection day code for the DebiCheck mandate.
        /// </summary>
        [Range(1, 31, ErrorMessage = "DebiCheckCollectionDayCode must be between 1 and 31.")]

        /// <summary>
        /// Indicates whether the mandate should be added to the master file.
        /// Setting this to <c>true</c> will automatically add the mandate to the master file, 
        /// allowing debit orders to be conducted without manual approval.
        /// 
        /// If set to <c>false</c>, the user must log into Netcash and manually accept/approve 
        /// the master file before any debit transactions can be processed.
        /// </summary>
        public bool? AddToMasterFile { get; set; }
    }
}
