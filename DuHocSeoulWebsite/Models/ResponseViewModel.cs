namespace DuHocSeoulWebsite.Models
{
    public class ResponseViewModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string FieldName { get; set; }
        public bool HasError { get; set; }
    }
}
