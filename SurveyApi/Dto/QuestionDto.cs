using SurveyApi.Models;

namespace SurveyApi.Dto
{
    public class QuestionDto
    {
        public string Title { get; set; }
        public List<AnswerDto> AnswersDto { get; set; }
    }
}
