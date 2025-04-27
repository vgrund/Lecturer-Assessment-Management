using System;

namespace LectureAssessmentManager.Models
{
    public class Assignment
    {
        public string AssignmentId { get; set; }
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime DueDate { get; set; }
        public int MaxScore { get; set; }
    }
}