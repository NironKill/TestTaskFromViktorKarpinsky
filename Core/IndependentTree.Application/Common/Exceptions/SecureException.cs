namespace IndependentTree.Application.Common.Exceptions
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message) { }

        public SecureException(string message, Exception exception) : base(message, exception) { }
    }
}
