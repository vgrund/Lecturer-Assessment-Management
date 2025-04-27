using System;

namespace LectureAssessmentManager.Models
{
    public class StudentAssessment
    {
        public int AssessmentId { get; set; }
        public string StudentId { get; set; }
        public int AssignmentId { get; set; }
        public int CategoryId { get; set; }
        public decimal Score { get; set; }
        public string Grade { get; set; }
        public DateTime AssessmentDate { get; set; }

        public static string CalculateGrade(decimal score)
        {
            if (score >= 70) return "Excellent";
            if (score >= 60) return "Distinction";
            if (score >= 50) return "Merit";
            if (score >= 40) return "Pass";
            return "Fail";
        }
    }
}