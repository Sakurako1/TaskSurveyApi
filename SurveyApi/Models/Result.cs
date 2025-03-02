

namespace SurveyApi.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public int InterviewId { get; set; }
        public Interview Interview { get; set; }
        public bool? IsRight { get; set; }
    }
}
