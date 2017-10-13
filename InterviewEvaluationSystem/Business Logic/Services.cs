﻿using InterviewEvaluationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewEvaluationSystem.Business_Logic
{
    public class Services
    {
        InterviewEvaluationDbEntities dbContext = new InterviewEvaluationDbEntities();
        public List<SkillCategoryViewModel> GetSkillCategories()
        {
            List<SkillCategoryViewModel> SkillCategories = dbContext.tblSkillCategories.Where(s => s.IsDeleted == false)
                .Select(s => new SkillCategoryViewModel
                {
                    SkillCategoryID = s.SkillCategoryID,
                    SkillCategory = s.SkillCategory
                }).ToList();
            return SkillCategories;
        }

        public List<RatingScaleViewModel> GetRatingScale()
        {
            List<RatingScaleViewModel> RatingScales = dbContext.tblRatingScales.Where(r => r.IsDeleted == false)
                .Select(r => new RatingScaleViewModel
                {
                    RateScaleID = r.RateScaleID,
                    RateScale = r.RateScale,
                    Value = r.Value,
                    Description = r.Description
                }).ToList();
            return RatingScales;
        }
        public List<SkillViewModel> GetSkills()
        {
            var skills = dbContext.tblSkills.Where(s => s.IsDeleted == false).ToList();
            List<SkillViewModel> Skills = skills.Select(s => new SkillViewModel
            {
                SkillID = s.SkillID,
                SkillName = s.SkillName,
                SkillCategoryID = s.SkillCategoryID
            }).ToList();
            return Skills;
        }

        public List<SkillViewModel> GetSkillsByCategory(int skillCategoryID)
        {
            var skills = dbContext.tblSkills.Where(s => s.SkillCategoryID == skillCategoryID && s.IsDeleted == false).ToList();
            List<SkillViewModel> Skills = skills.Select(s => new SkillViewModel
            {
                SkillID = s.SkillID,
                SkillName = s.SkillName,
                SkillCategoryID = s.SkillCategoryID
            }).ToList();
            return Skills;
        }

        public List<RoundViewModel> GetRounds()
        {
            List<RoundViewModel> Rounds = dbContext.tblRounds.Where(r => r.IsDeleted == false)
                .Select(r => new RoundViewModel
                {
                    RoundID = r.RoundID,
                    RoundName = r.RoundName
                }).ToList();
            return Rounds;
        }

        public List<StatusViewModel> GetStatus(int UserId)
        {
            List<StatusViewModel> Statuses = dbContext.spGetStatus(UserId)
                .Select(e => new StatusViewModel
                {
                    Name = e.Name,
                    RoundName = e.RoundName,
                    CandidateID = e.CandidateID,
                    RoundID = e.RoundID,
                    EvaluationID = e.EvaluationID,
                    Recommended = e.Recommended
                }).ToList();
            return Statuses;
        }

        public List<ScoreEvaluationViewModel> GetPreviousRoundScores(Nullable<int> candidateID, int roundID)
        {
            List<ScoreEvaluationViewModel> Statuses = dbContext.spGetPreviousRoundScores(candidateID, roundID)
                .Select(s => new ScoreEvaluationViewModel
                {
                    CandidateID = s.CandidateID,
                    EvaluationID = s.EvaluationID,
                    RoundID = s.RoundID,
                    RateScaleID = s.RateScaleID
                }).ToList();
            return Statuses;
        }

        public List<CurrentStatusViewModel> GetCurrentStatus()
        {
            List<CurrentStatusViewModel> CurrentStatuses = dbContext.spGetCurrentStatus()
                .Select(c => new CurrentStatusViewModel
                {
                    Name = c.Name,
                    Email = c.Email,
                    RoundID = c.RoundID,
                    EvaluationID = c.EvaluationID,
                    CandidateID = c.CandidateID,
                    Recommended =c.Recommended
                }).ToList();
            return CurrentStatuses;
        }

        public List<CommentViewModel> GetComments(Nullable<int> CandidateID)
        {
            List<CommentViewModel> comments = dbContext.spGetComments(CandidateID)
                .Select(c => new CommentViewModel
                {
                    RoundName = c.RoundName,
                    UserName = c.UserName,
                    Comment = c.Comment,
                    Recommended = c.Recommended
                }).ToList();
            return comments;
        }

        public int UpdatePassword(int userId, ChangePasswordViewModel changePasswordViewModel)
        {
            int res = dbContext.spUpdatePassword(userId, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
            return res;
        }
    }
}