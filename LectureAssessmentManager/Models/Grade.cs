namespace LectureAssessmentManager.Models
{
    public class Grade
    {
        public string AssignmentId { get; set; }
        public string StudentId { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
    }
}