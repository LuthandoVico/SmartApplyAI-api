namespace SmartApply.Api.Models.Request
{
    public class AnalyzeRequest
    {
        public string CvText { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
    }
}