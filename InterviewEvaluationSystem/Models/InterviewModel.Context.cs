﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterviewEvaluationSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InterviewEvaluationDbEntities : DbContext
    {
        public InterviewEvaluationDbEntities()
            : base("name=InterviewEvaluationDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCandidate> tblCandidates { get; set; }
        public virtual DbSet<tblEvaluation> tblEvaluations { get; set; }
        public virtual DbSet<tblPreviousCompany> tblPreviousCompanies { get; set; }
        public virtual DbSet<tblRatingScale> tblRatingScales { get; set; }
        public virtual DbSet<tblRound> tblRounds { get; set; }
        public virtual DbSet<tblScore> tblScores { get; set; }
        public virtual DbSet<tblSkill> tblSkills { get; set; }
        public virtual DbSet<tblSkillCategory> tblSkillCategories { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserType> tblUserTypes { get; set; }
    
        public virtual ObjectResult<Nullable<int>> spAuthenticate(string username, string passWord)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passWordParameter = passWord != null ?
                new ObjectParameter("PassWord", passWord) :
                new ObjectParameter("PassWord", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spAuthenticate", usernameParameter, passWordParameter);
        }
    
        public virtual ObjectResult<spCandidateWebGrid_Result> spCandidateWebGrid()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spCandidateWebGrid_Result>("spCandidateWebGrid");
        }
    
        public virtual ObjectResult<spGetCandidateInterviewers_Result> spGetCandidateInterviewers(Nullable<int> candidateID)
        {
            var candidateIDParameter = candidateID.HasValue ?
                new ObjectParameter("CandidateID", candidateID) :
                new ObjectParameter("CandidateID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetCandidateInterviewers_Result>("spGetCandidateInterviewers", candidateIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> spGetCandidatesInProgress()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spGetCandidatesInProgress");
        }
    
        public virtual ObjectResult<spGetCloumnChart_Result> spGetCloumnChart(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetCloumnChart_Result>("spGetCloumnChart", yearParameter);
        }
    
        public virtual ObjectResult<spGetComments_Result> spGetComments(Nullable<int> candidateID)
        {
            var candidateIDParameter = candidateID.HasValue ?
                new ObjectParameter("CandidateID", candidateID) :
                new ObjectParameter("CandidateID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetComments_Result>("spGetComments", candidateIDParameter);
        }
    
        public virtual ObjectResult<spGetCurrentStatus_Result> spGetCurrentStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetCurrentStatus_Result>("spGetCurrentStatus");
        }
    
        public virtual ObjectResult<spGetEmailByUserID_Result> spGetEmailByUserID(Nullable<int> candidateID, Nullable<int> userID)
        {
            var candidateIDParameter = candidateID.HasValue ?
                new ObjectParameter("CandidateID", candidateID) :
                new ObjectParameter("CandidateID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetEmailByUserID_Result>("spGetEmailByUserID", candidateIDParameter, userIDParameter);
        }
    
        public virtual ObjectResult<spGetHRDashBoard_Result> spGetHRDashBoard()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetHRDashBoard_Result>("spGetHRDashBoard");
        }
    
        public virtual ObjectResult<spGetInterviewerCloumnChart_Result> spGetInterviewerCloumnChart(Nullable<int> userID, Nullable<int> year)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetInterviewerCloumnChart_Result>("spGetInterviewerCloumnChart", userIDParameter, yearParameter);
        }
    
        public virtual ObjectResult<spGetInterviewerDashBoard_Result> spGetInterviewerDashBoard(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetInterviewerDashBoard_Result>("spGetInterviewerDashBoard", userIDParameter);
        }
    
        public virtual ObjectResult<spGetInterviewerPieChart_Result> spGetInterviewerPieChart(Nullable<int> userID, Nullable<int> year)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetInterviewerPieChart_Result>("spGetInterviewerPieChart", userIDParameter, yearParameter);
        }
    
        public virtual ObjectResult<spGetPieChart_Result> spGetPieChart(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetPieChart_Result>("spGetPieChart", yearParameter);
        }
    
        public virtual ObjectResult<spGetPreviousRoundScores_Result> spGetPreviousRoundScores(Nullable<int> candidateID, Nullable<int> roundID)
        {
            var candidateIDParameter = candidateID.HasValue ?
                new ObjectParameter("CandidateID", candidateID) :
                new ObjectParameter("CandidateID", typeof(int));
    
            var roundIDParameter = roundID.HasValue ?
                new ObjectParameter("RoundID", roundID) :
                new ObjectParameter("RoundID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetPreviousRoundScores_Result>("spGetPreviousRoundScores", candidateIDParameter, roundIDParameter);
        }
    
        public virtual ObjectResult<spGetSkillsBySkillCategory_Result> spGetSkillsBySkillCategory(Nullable<int> skillCategoryID)
        {
            var skillCategoryIDParameter = skillCategoryID.HasValue ?
                new ObjectParameter("SkillCategoryID", skillCategoryID) :
                new ObjectParameter("SkillCategoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetSkillsBySkillCategory_Result>("spGetSkillsBySkillCategory", skillCategoryIDParameter);
        }
    
        public virtual ObjectResult<spGetStatus_Result> spGetStatus(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetStatus_Result>("spGetStatus", userIDParameter);
        }
    
        public virtual ObjectResult<spHRNotificationGrid_Result> spHRNotificationGrid()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spHRNotificationGrid_Result>("spHRNotificationGrid");
        }
    
        public virtual int spInsertJoinDetails(Nullable<int> userID, Nullable<int> candidateID, Nullable<decimal> offeredSalary, Nullable<System.DateTime> dateOfJoining)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var candidateIDParameter = candidateID.HasValue ?
                new ObjectParameter("CandidateID", candidateID) :
                new ObjectParameter("CandidateID", typeof(int));
    
            var offeredSalaryParameter = offeredSalary.HasValue ?
                new ObjectParameter("OfferedSalary", offeredSalary) :
                new ObjectParameter("OfferedSalary", typeof(decimal));
    
            var dateOfJoiningParameter = dateOfJoining.HasValue ?
                new ObjectParameter("DateOfJoining", dateOfJoining) :
                new ObjectParameter("DateOfJoining", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertJoinDetails", userIDParameter, candidateIDParameter, offeredSalaryParameter, dateOfJoiningParameter);
        }
    
        public virtual ObjectResult<spLogin_Result> spLogin(string username, string passWord)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passWordParameter = passWord != null ?
                new ObjectParameter("PassWord", passWord) :
                new ObjectParameter("PassWord", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLogin_Result>("spLogin", usernameParameter, passWordParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> spRegister(string username, string employeeid, string designation, string address, string pincode, string password, string email)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var employeeidParameter = employeeid != null ?
                new ObjectParameter("Employeeid", employeeid) :
                new ObjectParameter("Employeeid", typeof(string));
    
            var designationParameter = designation != null ?
                new ObjectParameter("designation", designation) :
                new ObjectParameter("designation", typeof(string));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            var pincodeParameter = pincode != null ?
                new ObjectParameter("pincode", pincode) :
                new ObjectParameter("pincode", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spRegister", usernameParameter, employeeidParameter, designationParameter, addressParameter, pincodeParameter, passwordParameter, emailParameter);
        }
    
        public virtual int spResetPassword(string email, string newPassword)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var newPasswordParameter = newPassword != null ?
                new ObjectParameter("NewPassword", newPassword) :
                new ObjectParameter("NewPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spResetPassword", emailParameter, newPasswordParameter);
        }
    
        public virtual int spUpdateCandidateInterviewer(Nullable<int> userid, Nullable<int> candidateid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var candidateidParameter = candidateid.HasValue ?
                new ObjectParameter("candidateid", candidateid) :
                new ObjectParameter("candidateid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateCandidateInterviewer", useridParameter, candidateidParameter);
        }
    
        public virtual int spUpdatePassword(Nullable<int> userId, string oldPassword, string newPassword)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var oldPasswordParameter = oldPassword != null ?
                new ObjectParameter("OldPassword", oldPassword) :
                new ObjectParameter("OldPassword", typeof(string));
    
            var newPasswordParameter = newPassword != null ?
                new ObjectParameter("NewPassword", newPassword) :
                new ObjectParameter("NewPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdatePassword", userIdParameter, oldPasswordParameter, newPasswordParameter);
        }
    }
}
