namespace SurveyApi.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Info { get; set; }
        public List<Question> Questions { get; set; }
        public List<Interview> Interviews { get; set; }
    }
}
