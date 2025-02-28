namespace Netcash.DTO.Enums
{
    public enum BatchStatusEnum
    {
        Pending,
        Successful,
        Unsuccessful,
        SuccessfulWithErrors
    }

    public enum NetCashKeyEnum
    {
        AccountReference = 101,
        MandateName = 102,
        IsConsumer = 110,
        Surname = 113,
        FirstName = 114,
        DefaultMandateAmount = 161,
        Amount = 162,
        MobileNumber = 202,
        Extra1 = 301,
        DebitFrequency = 530,
        CommencementMonth = 531,
        CommencementDay = 532,
        DecemberDebitDay = 533,
        AgreementDate = 534
    }
}
