namespace Netcash.Common.Exceptions
{
    public class NetcashGatewayException : Exception
    {
        public NetcashGatewayException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
