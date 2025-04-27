using System;

namespace LectureAssessmentManager.Models
{
    public class Course
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LecturerId { get; set; }
    }
}