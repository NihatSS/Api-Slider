namespace Api_intro.Helpers.Exceptions
{
    public class AlreadyHasException : Exception
    {
        public AlreadyHasException(string message) : base(message) { }
        public AlreadyHasException() : base("This data already has!") { }
    }
}
