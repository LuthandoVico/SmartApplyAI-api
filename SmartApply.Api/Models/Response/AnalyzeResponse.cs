using System.Collections.Generic;

namespace SmartApply.Api.Models.Response
{
    public class AnalyzeResponse
    {
        public int MatchScore { get; set; }
        public List<string> MissingSkills { get; set; } = new();
        public List<string> Strengths { get; set; } = new();
        public string ImprovedCV { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public List<string> Insights { get; set; } = new();
    }
}