using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Netcash.DTO.Requests
{
    public class CreateMandateRequest
    {
        [Required]
        [Description("A unique reference used to identify this mandate in future transactions.")]
        [StringLength(22, MinimumLength = 2, ErrorMessage = "AccountReference must be between 2 and 22 characters.")]
        public string AccountReference { get; set; }

        [Required]
        [Description("The name of your client associated with this mandate.")]
        public string MandateName { get; set; }

        [Required]
        [Description("The amount authorized in the mandate.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "MandateAmount must be a positive value.")]
        public decimal MandateAmount { get; set; }

        [Required]
        [Description("Indicates whether the mandate is for an individual (true) or a business (false).")]
        public bool IsConsumer { get; set; }

        [Description("First name of the account holder (Required if IsConsumer is true).")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Description("Surname of the account holder (Required if IsConsumer is true).")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Description("Business trading name (Required if IsConsumer is false).")]
        [StringLength(100)]
        public string TradingName { get; set; }

        [Description("Business registration number (Required if IsConsumer is false).")]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Description("Business registered name (Required if IsConsumer is false).")]
        [StringLength(100)]
        public string RegisteredName { get; set; }

        [Required]
        [Phone]
        [Description("Mobile phone number of the account holder.")]
        public string MobileNumber { get; set; }

        [Required]
        [Description(@"The frequency at which debits should occur.
        1 = Monthly, 2 = Bimonthly, 3 = ThreeMonthly, 4 = SixMonthly, 5 = Annually, 6 = Weekly, 7 = Biweekly.
        ")]
        [Range(1, 7, ErrorMessage = "Invalid DebitFrequency value.")]
        public int DebitFrequency { get; set; }

        [Required]
        [Description("The month when debits should commence (1 = January, 12 = December).")]
        [Range(1, 12, ErrorMessage = "CommencementMonth must be between 1 and 12.")]
        public int CommencementMonth { get; set; }

        [Required]
        [Description("The day(s) of the month for debits (e.g., \"05/15/25/LDOM\" for 5th, 15th, 25th, and Last Day of Month).")]
        public string CommencementDay { get; set; }

        [Required]
        [Description("The date when the agreement was made (Format: YYYYMMDD, e.g., 20250301 for March 1, 2025).")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "AgreementDate must be in YYYYMMDD format.")]
        public string AgreementDate { get; set; }

        [Required]
        [Description("A reference number for the agreement.")]
        public string AgreementReferenceNumber { get; set; }

        [Range(1, 60, ErrorMessage = "CancellationNoticePeriod must be between 1 and 60.")]
        [Description("Notice period for cancellation (Optional, range: 1 to 60 days).")]
        public int? CancellationNoticePeriod { get; set; }

        [Range(1, 2, ErrorMessage = "Invalid PublicHolidayOption value.")]
        [Description(@"Defines the action to take if a debit falls on a public holiday.
        1 = Preceding Business Day, 2 = Next Business Day.
        ")]
        public int? PublicHolidayOption { get; set; }

        [StringLength(255)]
        [Description("Additional notes related to the mandate.")]
        public string Notes { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Description("Email address of the account holder.")]
        public string EmailAddress { get; set; }

        [StringLength(255)]
        [Description("Physical address of the account holder.")]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
        [Description("Custom field 1 for additional data.")]
        public string Field1 { get; set; }

        [StringLength(50)]
        [Description("Custom field 2 for additional data.")]
        public string Field2 { get; set; }

        [StringLength(50)]
        [Description("Custom field 3 for additional data.")]
        public string Field3 { get; set; }

        [StringLength(50)]
        [Description("Custom field 4 for additional data.")]
        public string Field4 { get; set; }

        [StringLength(50)]
        [Description("Custom field 5 for additional data.")]
        public string Field5 { get; set; }

        [StringLength(50)]
        [Description("Custom field 6 for additional data.")]
        public string Field6 { get; set; }

        [StringLength(50)]
        [Description("Custom field 7 for additional data.")]
        public string Field7 { get; set; }

        [StringLength(50)]
        [Description("Custom field 8 for additional data.")]
        public string Field8 { get; set; }

        [StringLength(50)]
        [Description("Custom field 9 for additional data.")]
        public string Field9 { get; set; }

        [Description("Indicates if variable debit amounts are allowed.")]
        public bool AllowVariableDebitAmounts { get; set; }

        [Description(@"Indicates whether an Account Verification Service (AVS) check should be performed.
        false = No AVS check, true = Perform AVS check.
        ")]
        public bool? RequestAVS { get; set; }

        [Description("The ID number to be used on the mandate, regardless if RequestAVS is true or false")]
        public string AVSCheckNumber { get; set; }

        [Description(@"Indicates whether to include DebiCheck authentication with the EFT mandate.
        false = EFT mandate only, true = Include DebiCheck.
        ")]
        public bool IncludeDebiCheck { get; set; }

        [Description("The ID of a predefined DebiCheck mandate template.")]
        public string DebiCheckMandateTemplateId { get; set; }

        [Description("The amount of the DebiCheck mandate.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "DebiCheckCollectionAmount must be a positive value.")]
        public decimal? DebiCheckCollectionAmount { get; set; }

        [Description(@"Indicates whether the first collection differs from the mandate amount.
        false = First collection is the same as mandate, true = First collection is different."
        )]
        public bool? DebiCheckFirstCollectionDiffers { get; set; }

        [Description("The first collection date for the DebiCheck mandate (Format: YYYYMMDD).")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "DebiCheckFirstCollectionDate must be in YYYYMMDD format.")]
        public string DebiCheckFirstCollectionDate { get; set; }

        [Description("The amount of the first DebiCheck collection.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "DebiCheckFirstCollectionAmount must be a positive value.")]
        public decimal? DebiCheckFirstCollectionAmount { get; set; }

        [Description("The collection day code for the DebiCheck mandate.")]
        [Range(1, 31, ErrorMessage = "DebiCheckCollectionDayCode must be between 1 and 31.")]
        public int DebiCheckCollectionDayCode { get; set; }

        [Description(@"Indicates whether the mandate should be added to the master file.
        true = automatically add the mandate to the master file allowing debit orders to be conducted without manual approval. 
        false = the user must log into Netcash and manually accept/approve the master file before any debit transactions can be processed"
        )]
        public bool AddToMasterFile { get; set; }
    }
}
