namespace Aryap.Shared.Exceptions
{
    public class CustomException : Exception
    {
        private List<string> _messages { get; set; } = new List<string> { };

        public List<string> Messages => _messages;

        public CustomException()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public CustomException(List<string> messages)
        {
            if (messages != null && messages.Count > 0)
            {
                _messages = messages;
            }
        }
    }
}