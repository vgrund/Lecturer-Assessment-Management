using System;
using System.Collections.Generic;

namespace LectureAssessmentManager.Models
{
    public class StudentMark
    {
        public string MarkId { get; set; }
        public string StudentId { get; set; }
        public string AssignmentId { get; set; }
        public string RubricId { get; set; }
        public DateTime MarkedDate { get; set; }
        public string Comments { get; set; }
        public decimal FinalScore { get; set; }
        public string FinalGrade { get; set; }
        public List<CriterionMark> CriterionMarks { get; set; } = new List<CriterionMark>();
    }

    public class CriterionMark
    {
        public string CriterionId { get; set; }
        public decimal Score { get; set; }
        public string Comments { get; set; }
    }
}