namespace Netcash.DTO.Responses
{
    public class MandateResponse
    {
        //TODO: Omit if no error
        public string ErrorMessage { get; set; }

        //TODO: Omit if error
        public string MandateUrl { get; set; }
    }
}
