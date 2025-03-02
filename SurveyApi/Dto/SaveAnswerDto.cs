using System.Text.Json.Serialization;

namespace SurveyApi.Dto
{
    public class SaveAnswerDto
    {
        [JsonIgnore]
        public int InterviewId { get; set; }
        [JsonIgnore]
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }

    }
}
