using NIWS;

namespace Netcash.Data.Requests
{
    public class AddMandateRequest
    {
        public string AccountReference { get; set; }
        public string MandateName { get; set; }
        public decimal MandateAmount { get; set; }
        public bool IsConsumer { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string TradingName { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegisteredName { get; set; }
        public string MobileNumber { get; set; }
        public MandateOptionsMandateDebitFrequency DebitFrequency { get; set; }
        public int CommencementMonth { get; set; }
        public string CommencementDay { get; set; }
        public string AgreementDate { get; set; }
        public string AgreementReferenceNumber { get; set; }
        public int? CancellationNoticePeriod { get; set; }
        public MandateOptionsMandatePublicHolidayOption? PublicHolidayOption { get; set; }
        public string Notes { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        public bool? AllowVariableDebitAmounts { get; set; }
        public int? BankDetailType { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BranchCode { get; set; }
        public MandateOptionsBankAccountType? BankAccountType { get; set; }
        public string CreditCardToken { get; set; }
        public int? CreditCardType { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public bool? IsIdNumber { get; set; }
        public MandateOptionsTitle? Title { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string DecemberDebitDay { get; set; }
        public string DebitMasterfileGroup { get; set; }
        public string PhysicalAddressLine1 { get; set; }
        public string PhysicalAddressLine2 { get; set; }
        public string PhysicalAddressLine3 { get; set; }
        public string PhysicalSuburb { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalProvince { get; set; }
        public string PhysicalPostalCode { get; set; }
        public bool? MandateActive { get; set; }
        public bool? RequestAVS { get; set; }
        public string AVSCheckNumber { get; set; }
        public bool IncludeDebiCheck { get; set; }
        public string DebiCheckMandateTemplateId { get; set; }
        public decimal? DebiCheckCollectionAmount { get; set; }
        public bool? DebiCheckFirstCollectionDiffers { get; set; }
        public string DebiCheckFirstCollectionDate { get; set; }
        public decimal? DebiCheckFirstCollectionAmount { get; set; }
        public DebiCheckOptionsCollectionFrequencyDayCodes? DebiCheckCollectionDayCode { get; set; }
        public bool? AddToMasterFile { get; set; }
    }
}
