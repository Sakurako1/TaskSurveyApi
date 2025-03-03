using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Interfaces;
using SurveyApi.Models;

namespace SurveyApi.Repository
{
    public class SurveyRepository : IRepositorySurvey
    {
        private readonly DataContext _db;

        private readonly ILogger<SurveyRepository> _logger;
        public SurveyRepository(DataContext db, ILogger<SurveyRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Question> GetQuestionAsync(int questionId , int surveyId)
        {
            try
            {
                _logger.LogInformation("Fetching question with ID {QuestionId}", questionId);
                var question = await _db.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == questionId && q.SurveyId == surveyId);
                
                if (question == null)
                {
                    _logger.LogWarning("Question with ID {QuestionId} not found", questionId);
                }

                return question;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching question with ID {QuestionId}", questionId);
                throw;
            }
        }

        public async Task<int?> SaveAnswerAsync(int interviewId, int questionId, int answerId)
        {
            try
            {
            var checkResult = await _db.Questions.Where(q => q.Id == questionId)
                                        .Join(_db.Answers, q => q.Id, a => a.QuestionId, (q, a) => new { q, a })
                                        .Join(_db.Interviews, qa => qa.q.SurveyId, i => i.SurveyId, (qa, i) => new { qa, i })
                                        .Where(x => x.qa.q.Id == questionId && x.qa.a.Id == answerId && x.i.Id == interviewId)
                                        .AnyAsync();

                if (!checkResult)
                {
                    _logger.LogWarning("One or more entities do not exist: QuestionId={QuestionId}, AnswerId={AnswerId}, InterviewId={InterviewId}", questionId, answerId, interviewId);
                    return null;
                }
                _logger.LogInformation("Saving answer: InterviewId={InterviewId}, QuestionId={QuestionId}, AnswerId={AnswerId}",
                    interviewId, questionId, answerId);

                var result = new Result
                {
                    InterviewId = interviewId,
                    QuestionId = questionId,
                    AnswerId = answerId,
                    IsRight = true
                };

                await _db.Results.AddAsync(result);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Answer saved successfully. ResultId={ResultId}", result.Id);

                var currentQuestion = await _db.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(q => q.Id == questionId);

                if (currentQuestion == null)
                {
                    _logger.LogWarning("No current question found for ID {QuestionId}", questionId);
                    return null;
                }

                var nextQuestionId = await _db.Questions
                    .Where(q => q.SurveyId == currentQuestion.SurveyId && q.Id > questionId)
                    .OrderBy(q => q.Id)
                    .Select(q => (int?)q.Id)
                    .FirstOrDefaultAsync();

                if (nextQuestionId == null)
                {
                    _logger.LogInformation("No more questions left in the survey for QuestionId {QuestionId}", questionId);
                    return 0;
                }

                _logger.LogInformation("Next question ID is {NextQuestionId}", nextQuestionId);
                return nextQuestionId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving answer for InterviewId={InterviewId}, QuestionId={QuestionId}, AnswerId={AnswerId}",
                    interviewId, questionId, answerId);
                throw;
            }
        }
    }
}

