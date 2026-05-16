using SmartApply.Api.Models.Request;
using SmartApply.Api.Models.Response;

namespace SmartApply.Api.Services
{
    public class JobAnalysisService
    {
        private readonly OpenAIService _aiService;

        public JobAnalysisService(OpenAIService aiService)
        {
            _aiService = aiService;
        }

        public async Task<AnalyzeResponse> AnalyzeAsync(AnalyzeRequest request)
        {
            // 1. Extract keywords (simple version)
            var jobKeywords = ExtractKeywords(request.JobDescription);
            var cvKeywords = ExtractKeywords(request.CvText);

            // 2. Compare
            var matched = cvKeywords.Intersect(jobKeywords).ToList();
            var missing = jobKeywords.Except(cvKeywords).ToList();

            // 3. Score calculation
            int score = 0;
            if (jobKeywords.Count > 0)
            {
                score = (int)((double)matched.Count / jobKeywords.Count * 100);
            }

            // 4. Call AI
            var aiResult = await _aiService.GenerateAsync(
                request.CvText,
                request.JobDescription,
                matched,
                missing
            );

            // 5. Return final response
            return new AnalyzeResponse
            {
                MatchScore = score,
                MissingSkills = missing,
                Strengths = matched,
                ImprovedCV = aiResult.ImprovedCV,
                CoverLetter = aiResult.CoverLetter,
                Insights = aiResult.Insights
            };
        }

        // 🔍 SIMPLE KEYWORD EXTRACTOR (Hackathon version)
        private List<string> ExtractKeywords(string text)
        {
            var keywords = new List<string>
            {
                "C#", "Java", "SQL", "React", "ASP.NET",
                "API", "REST", "Docker", "Git", "Agile", "TypeScript"
            };

            return keywords
                .Where(k => text.Contains(k, StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .ToList();
        }
    }
}