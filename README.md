# SmartApply API (Backend)

SmartApply API is a .NET 8 Web API that powers the SmartApply AI system. It analyzes a user's CV against a job description and returns a match score, missing skills, and AI-generated improvements.

## Features

- CV vs Job Description matching
- Skill extraction and comparison
- Match score calculation (0–100%)
- Missing skills detection
- AI-powered CV rewriting (OpenAI GPT-4o-mini)
- AI-generated cover letters
- RESTful API design

## Tech Stack

- ASP.NET Core 8 Web API
- C#
- OpenAI GPT-4o-mini
- LINQ for keyword analysis
- Swagger for API testing

## Project Structure

```
SmartApply.Api/
├── Controllers/
│   └── AnalysisController.cs
├── Models/
│   ├── Request/
│   └── Response/
├── Services/
│   ├── JobAnalysisService.cs
│   └── OpenAIService.cs
├── Program.cs
└── appsettings.json
```

## API Endpoint

### POST `/api/analysis/analyze`

Analyzes a CV against a job description.

**Request:**
```json
{
  "cvText": "string",
  "jobDescription": "string"
}
```

**Response:**
```json
{
  "matchScore": 75,
  "strengths": [],
  "missingSkills": [],
  "improvedCV": "",
  "coverLetter": "",
  "insights": []
}
```

## Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/LuthandoVico/SmartApplyAI-api.git
cd SmartApply.Api
```

### 2. Configure environment variables

Create a `.env` file:

```
OPENAI_API_KEY=your-api-key
```

Make sure `.env` is added to `.gitignore`.

### 3. Run the project

```bash
dotnet restore
dotnet run
```

API runs at:
```
https://localhost:7001
```

Swagger UI:
```
https://localhost:7001/swagger
```

## Security Note

- Never commit API keys or secrets
- Use `.env` or environment variables for sensitive data

## Author

Luthando Cele
