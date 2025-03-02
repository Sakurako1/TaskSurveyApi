using SurveyApi.Models;

namespace SurveyApi.Interfaces
{
    public interface IRepositorySurvey
    {
        Task<Question> GetQuestionAsync(int questionId, int surveyId);
        Task<int?> SaveAnswerAsync(int interviewId, int questionId, int answerId);
    }
}
