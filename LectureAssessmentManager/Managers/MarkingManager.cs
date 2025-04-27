using System;
using System.Collections.Generic;
using System.Linq;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Managers
{
    public class MarkingManager
    {
        private readonly RubricManager _rubricManager;
        private List<StudentMark> _marks;

        public MarkingManager(RubricManager rubricManager)
        {
            _rubricManager = rubricManager;
            _marks = new List<StudentMark>();
        }

        public StudentMark CreateMark(string studentId, string assignmentId, string rubricId)
        {
            var mark = new StudentMark
            {
                MarkId = Guid.NewGuid().ToString(),
                StudentId = studentId,
                AssignmentId = assignmentId,
                RubricId = rubricId,
                MarkedDate = DateTime.Now
            };

            // Initialize criterion marks
            var rubric = _rubricManager.GetRubricById(rubricId);
            if (rubric != null)
            {
                foreach (var criterion in rubric.Criteria)
                {
                    mark.CriterionMarks.Add(new CriterionMark
                    {
                        CriterionId = criterion.CriterionId,
                        Score = 0,
                        Comments = string.Empty
                    });
                }
            }

            _marks.Add(mark);
            return mark;
        }

        public void UpdateMark(StudentMark mark)
        {
            var existingMark = _marks.FirstOrDefault(m => m.MarkId == mark.MarkId);
            if (existingMark != null)
            {
                var index = _marks.IndexOf(existingMark);
                _marks[index] = mark;
            }
        }

        public StudentMark GetMarkByStudent(string studentId, string assignmentId)
        {
            return _marks.FirstOrDefault(m => m.StudentId == studentId && m.AssignmentId == assignmentId);
        }

        public List<StudentMark> GetMarksByAssignment(string assignmentId)
        {
            return _marks.Where(m => m.AssignmentId == assignmentId).ToList();
        }

        public void CalculateFinalScore(StudentMark mark)
        {
            var rubric = _rubricManager.GetRubricById(mark.RubricId);
            if (rubric == null) return;

            decimal totalScore = 0;
            foreach (var criterionMark in mark.CriterionMarks)
            {
                var criterion = rubric.Criteria.FirstOrDefault(c => c.CriterionId == criterionMark.CriterionId);
                if (criterion != null)
                {
                    // Calculate weighted score for this criterion
                    decimal weightedScore = (criterionMark.Score * criterion.Weight) / 100;
                    totalScore += weightedScore;
                }
            }

            mark.FinalScore = totalScore;
            mark.FinalGrade = _rubricManager.GetGradeLevel((int)totalScore);
            mark.MarkedDate = DateTime.Now;
        }

        public bool ValidateMarks(StudentMark mark)
        {
            var rubric = _rubricManager.GetRubricById(mark.RubricId);
            if (rubric == null) return false;

            foreach (var criterionMark in mark.CriterionMarks)
            {
                if (criterionMark.Score < 0 || criterionMark.Score > 100)
                {
                    return false;
                }
            }

            return true;
        }

        public void DeleteMark(string markId)
        {
            var mark = _marks.FirstOrDefault(m => m.MarkId == markId);
            if (mark != null)
            {
                _marks.Remove(mark);
            }
        }
    }
}