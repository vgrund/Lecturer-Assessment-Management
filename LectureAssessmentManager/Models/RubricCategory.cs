using System;

namespace LectureAssessmentManager.Models
{
    public class RubricCategory
    {
        public int CategoryId { get; set; }
        public int AssignmentId { get; set; }
        public string Name { get; set; }
        public decimal WeightPercentage { get; set; }
    }
}