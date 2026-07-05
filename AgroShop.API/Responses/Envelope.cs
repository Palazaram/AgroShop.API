namespace AgroShop.API.Responses
{
    public record Envelope
    {
        public object? Result { get; }
        public List<ResponseError> Errors { get; }
        public DateTime TimeGenerated { get; }

        private Envelope(object? result, IEnumerable<ResponseError> errors) 
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok(object? result = null) => new Envelope(result, []);

        public static Envelope Error(IEnumerable<ResponseError> errors) => new Envelope(null, errors);
    }
}
