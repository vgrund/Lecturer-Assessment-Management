using System.Collections.Generic;

namespace LectureAssessmentManager.Models
{
    public class Rubric
    {
        public string RubricId { get; set; }
        public string AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<RubricCriterion> Criteria { get; set; } = new List<RubricCriterion>();
        public int TotalWeight { get; set; }
    }
}