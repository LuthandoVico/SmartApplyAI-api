using OpenAI.Chat;
using System.Text.Json;

namespace SmartApply.Api.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;

        public OpenAIService(IConfiguration config)
        {
            _apiKey = config["OpenAI:ApiKey"];
        }

        public async Task<AIResult> GenerateAsync(
            string cv,
            string job,
            List<string> matched,
            List<string> missing)
        {
            var client = new ChatClient(model: "gpt-4o-mini", apiKey: _apiKey);

            var prompt = $@"
You are an AI career assistant.

IMPORTANT:
Return ONLY valid JSON. Do not include explanations or extra text.

Format:
{{
  ""improvedCV"": ""string"",
  ""coverLetter"": ""string"",
  ""insights"": [""string"", ""string"", ""string""]
}}

CV:
{cv}

Job Description:
{job}

Matched Skills: {string.Join(", ", matched)}
Missing Skills: {string.Join(", ", missing)}

Tasks:
1. Rewrite and improve the CV professionally
2. Generate a tailored cover letter
3. Provide exactly 3 insights why candidate may be rejected
";

            // ✅ CALL AI
            var response = await client.CompleteChatAsync(prompt);

            var content = response.Value.Content[0].Text;

            // 🔥 Clean response
            content = content.Replace("```json", "").Replace("```", "").Trim();

            try
            {
                var result = JsonSerializer.Deserialize<AIResult>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception)
            {
                // fallback
            }

            // ⚠️ fallback response
            return new AIResult
            {
                ImprovedCV = "Could not generate improved CV.",
                CoverLetter = "Could not generate cover letter.",
                Insights = new List<string>
                {
                    "AI response parsing failed.",
                    "Try again.",
                    "Check input formatting."
                }
            };
        }
    }

    public class AIResult
    {
        public string ImprovedCV { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public List<string> Insights { get; set; } = new();
    }
}