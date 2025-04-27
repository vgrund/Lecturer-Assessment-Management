using System;
using System.Collections.Generic;
using System.Linq;
using LectureAssessmentManager.Models;

namespace LectureAssessmentManager.Managers
{
    public class RubricManager
    {
        private List<Rubric> _rubrics;
        private List<RubricCriterion> _criteria;

        public RubricManager()
        {
            _rubrics = new List<Rubric>();
            _criteria = new List<RubricCriterion>();
        }

        public Rubric CreateRubric(string assignmentId, string title, string description)
        {
            var rubric = new Rubric
            {
                RubricId = Guid.NewGuid().ToString(),
                AssignmentId = assignmentId,
                Title = title,
                Description = description,
                Criteria = new List<RubricCriterion>()
            };
            _rubrics.Add(rubric);
            return rubric;
        }

        public RubricCriterion AddCriterion(string rubricId, string title, string description, int weight)
        {
            var totalWeight = GetTotalWeightForRubric(rubricId);
            if (totalWeight + weight > 100)
            {
                throw new InvalidOperationException($"Total weight cannot exceed 100%. Current total: {totalWeight}%");
            }

            var criterion = new RubricCriterion
            {
                CriterionId = Guid.NewGuid().ToString(),
                RubricId = rubricId,
                Title = title,
                Description = description,
                Weight = weight
            };

            _criteria.Add(criterion);
            var rubric = GetRubricById(rubricId);
            if (rubric != null)
            {
                rubric.Criteria.Add(criterion);
                UpdateRubricTotalWeight(rubric);
            }
            return criterion;
        }

        public void UpdateCriterion(RubricCriterion criterion)
        {
            var existingCriterion = _criteria.FirstOrDefault(c => c.CriterionId == criterion.CriterionId);
            if (existingCriterion != null)
            {
                var totalWeight = GetTotalWeightForRubric(criterion.RubricId) - existingCriterion.Weight;
                if (totalWeight + criterion.Weight > 100)
                {
                    throw new InvalidOperationException($"Total weight cannot exceed 100%. Current total: {totalWeight}%");
                }

                var index = _criteria.IndexOf(existingCriterion);
                _criteria[index] = criterion;

                var rubric = GetRubricById(criterion.RubricId);
                if (rubric != null)
                {
                    var criterionIndex = rubric.Criteria.FindIndex(c => c.CriterionId == criterion.CriterionId);
                    if (criterionIndex != -1)
                    {
                        rubric.Criteria[criterionIndex] = criterion;
                        UpdateRubricTotalWeight(rubric);
                    }
                }
            }
        }

        public void DeleteCriterion(string criterionId)
        {
            var criterion = _criteria.FirstOrDefault(c => c.CriterionId == criterionId);
            if (criterion != null)
            {
                _criteria.Remove(criterion);
                var rubric = GetRubricById(criterion.RubricId);
                if (rubric != null)
                {
                    rubric.Criteria.RemoveAll(c => c.CriterionId == criterionId);
                    UpdateRubricTotalWeight(rubric);
                }
            }
        }

        public void UpdateRubric(Rubric rubric)
        {
            var existingRubric = _rubrics.FirstOrDefault(r => r.RubricId == rubric.RubricId);
            if (existingRubric != null)
            {
                var index = _rubrics.IndexOf(existingRubric);
                _rubrics[index] = rubric;
            }
        }

        public void DeleteRubric(string rubricId)
        {
            var rubric = GetRubricById(rubricId);
            if (rubric != null)
            {
                _rubrics.Remove(rubric);
                _criteria.RemoveAll(c => c.RubricId == rubricId);
            }
        }

        public Rubric GetRubricById(string rubricId)
        {
            return _rubrics.FirstOrDefault(r => r.RubricId == rubricId);
        }

        public List<Rubric> GetRubricsByAssignment(string assignmentId)
        {
            return _rubrics.Where(r => r.AssignmentId == assignmentId).ToList();
        }

        public List<RubricCriterion> GetCriteriaByRubric(string rubricId)
        {
            return _criteria.Where(c => c.RubricId == rubricId).ToList();
        }

        public int GetTotalWeightForRubric(string rubricId)
        {
            return _criteria.Where(c => c.RubricId == rubricId).Sum(c => c.Weight);
        }

        private void UpdateRubricTotalWeight(Rubric rubric)
        {
            rubric.TotalWeight = rubric.Criteria.Sum(c => c.Weight);
        }

        public string GetGradeLevel(int score)
        {
            if (score >= 70) return "Excellent";
            if (score >= 60) return "Distinction";
            if (score >= 50) return "Merit";
            if (score >= 40) return "Pass";
            return "Fail";
        }

        public Tuple<int, int> GetGradeLevelRange(string level)
        {
            switch (level)
            {
                case "Excellent":
                    return Tuple.Create(70, 100);
                case "Distinction":
                    return Tuple.Create(60, 69);
                case "Merit":
                    return Tuple.Create(50, 59);
                case "Pass":
                    return Tuple.Create(40, 49);
                case "Fail":
                    return Tuple.Create(0, 39);
                default:
                    throw new ArgumentException("Invalid grade level", nameof(level));
            }
        }
    }
}