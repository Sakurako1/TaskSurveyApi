

namespace SurveyApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<Answer> Answers { get; set; }

    }
}
