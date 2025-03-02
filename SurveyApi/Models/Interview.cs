

namespace SurveyApi.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public DateTime DateTime { get; set; }
        public string Respondent { get; set; }
        public List<Result>? Results { get; set; }
    }
}
