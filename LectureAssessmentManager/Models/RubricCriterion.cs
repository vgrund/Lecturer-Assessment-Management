namespace LectureAssessmentManager.Models
{
    public class RubricCriterion
    {
        public string CriterionId { get; set; }
        public string RubricId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Score { get; set; }
    }
}