namespace IndependentTree.Application.Requests.Magazine.Read.GetAll
{
    public class GetAllJournalResponce
    {
        public Guid ExceptionId { get; set; }
        public int Timestamp { get; set; }
        public int StatusCode { get; set; }
        public string TypeRequest { get; set; }
        public string QueryParameters { get; set; }
        public string BodyParameters { get; set; }
        public string StackTrace { get; set; }
    }
}
