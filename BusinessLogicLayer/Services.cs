﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using static DataAccessLayer.InterviewViewModels;

namespace BusinessLogicLayer
{
    public class Services
    {
        DataAccess dataAccess = new DataAccess();

        public bool ValidateLoginCredentials(string UserName, string Password)
        {
            return dataAccess.ValidateLoginCredentials(UserName, Password);
        }

        public UserViewModel GetLoginUserDetails(string UserName, string Password)
        {
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1");
            if (UserName == "hr")
            {
                return dataAccess.GetLoginUserDetails(UserName, Password);
            }
            else
            {
                return dataAccess.GetLoginUserDetails(UserName, hashedPwd);
            }
        }

        public UserViewModel GetProfile(string name)
        {
            return dataAccess.GetProfile(name);
        }

        public string UpdateProfile(string name, UserViewModel userViewModel, int UserID)
        {
            dataAccess.UpdateProfile(name, userViewModel, UserID);
            return Constants.profileUpdate;
        }

        public int UpdatePassword(int userId, ChangePasswordViewModel changePasswordViewModel)
        {
            return dataAccess.UpdatePassword(userId, changePasswordViewModel);
        }

        public void UpdatePasswordByEmail(string Email, string newPassword)
        {
            dataAccess.UpdatePasswordByEmail(Email, newPassword);
        }

        public bool ValidateEmail(string Email)
        {
            return dataAccess.ValidateEmail(Email);
        }

        public void InsertEvaluation(int UserID, int CandidateID, int RoundID, int hrID)
        {
            dataAccess.InsertEvaluation(UserID, CandidateID, RoundID, UserID);
        }

        public HRDashboardViewModel GetHRDashBoard()
        {
            return dataAccess.GetHRDashBoard();
        }

        public void GetPieChart(PieChartViewModel result)
        {
            if (result.Hired != 0 || result.InProgress != 0 || result.Rejected != 0)
            {
                // Use Chart class to create a pie chart image based on an array of values
                Chart chart = new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
                .AddLegend("Summary")
                .AddSeries("Default",
                    chartType: "doughnut",
                    xValue: new[] { (result.InProgress != 0) ? "Inprogress - #PERCENT{P0}" : "", (result.Hired != 0) ? "Hired - #PERCENT{P0}" : "", (result.Rejected != 0) ? "Rejected - #PERCENT{P0}" : "" },
                    yValues: new[] { result.InProgress, result.Hired, result.Rejected })
                .Write("bmp");
            }
        }

        public void GetHRPieChart(int year)
        {
            GetPieChart(dataAccess.GetHRPieChartData(year));
        }

        public void GetInterviewerPieChart(int UserID, int year)
        {
            GetPieChart(dataAccess.GetPieChartData(UserID, year));
        }

        public void GetColumnChart(ColumnChartViewModel result, int year)
        {
            if (result.January != 0 || result.February != 0 || result.March != 0 || result.April != 0 || result.May != 0 || result.June != 0 || result.July != 0 || result.August != 0 || result.September != 0 || result.October != 0 || result.November != 0 || result.December != 0)
            {
                // Use Chart class to create a column chart image based on an array of values
                Chart chart = new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddSeries("Default", chartType: "column",
                    xValue: new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                    yValues: new[] { result.January, result.February, result.March, result.April, result.May, result.June, result.July, result.August, result.September, result.October, result.November, result.December })
                .SetXAxis(year.ToString())
                .SetYAxis("No of Candidates")
                .Write("bmp");
            }
        }

        public void GetHRColumnChart(int year)
        {
            GetColumnChart(dataAccess.GetHRColumnChartData(year), year);
        }

        public void GetInterviewerColumnChart(int UserID, int year)
        {
            GetColumnChart(dataAccess.GetColumnChartData(UserID, year), year);
        }

        public List<CurrentStatusViewModel> GetCandidatesinHR()
        {
            return dataAccess.GetCandidatesinHR();
        }
        public List<CurrentStatusViewModel> SearchCandidatesinHR(string searchString)
        {
            return dataAccess.SearchCandidatesinHR(searchString);
        }

        public List<CandidateViewModel> GetInProgressCandidates()
        {
            return dataAccess.GetInProgressCandidates();
        }

        public List<CandidateViewModel> SearchInProgressCandidates(string searchString)
        {
            return dataAccess.SearchInProgressCandidates(searchString);
        }

        public List<CandidateViewModel> GetHiredCandidates()
        {
            return dataAccess.GetHiredCandidates();
        }

        public List<CandidateViewModel> SearchHiredCandidates(string searchString)
        {
            return dataAccess.SearchHiredCandidates(searchString);
        }

        public List<CandidateViewModel> GetCandidateStatuses()
        {
            return dataAccess.GetCandidateStatuses();
        }

        public void InsertRound(RoundViewModel roundViewModel, int UserID)
        {
            dataAccess.InsertRound(roundViewModel, UserID);
        }

        public bool ValidateRound(string RoundName)
        {
            return dataAccess.ValidateRound(RoundName); ;
        }

        public void UpdateRound(int RoundID, string RoundName)
        {
            dataAccess.UpdateRound(RoundID, RoundName);
        }

        public void DeleteRound(int RoundID, int UserID)
        {
            dataAccess.DeleteRound(RoundID, UserID);
        }

        public void InsertRatingScale(RatingScaleViewModel ratingScaleViewModel, int UserID)
        {
            dataAccess.InsertRatingScale(ratingScaleViewModel, UserID);
        }

        public bool ValidateRateScale(string RateScale)
        {
            return dataAccess.ValidateRateScale(RateScale);
        }

        public bool ValidateRateValue(int RateValue)
        {
            return dataAccess.ValidateRateValue(RateValue);
        }

        public void UpdateRatingScale(int RateScaleID, string Ratescale, int Ratevalue, string description)
        {
            dataAccess.UpdateRatingScale(RateScaleID, Ratescale, Ratevalue, description);
        }

        public void DeleteRatingScale(int RateScaleID, int UserID)
        {
            dataAccess.DeleteRatingScale(RateScaleID, UserID);
        }
        public void InsertSkillCategory(SkillCategoryViewModel skillCategoryViewModel, int UserID)
        {
            dataAccess.InsertSkillCategory(skillCategoryViewModel, UserID);
        }

        public bool ValidateSkillCategory(string SkillCategory)
        {
            return dataAccess.ValidateSkillCategory(SkillCategory);
        }

        public void UpdateSkillCategory(int SkillCategoryID, string SkillCategory, string description)
        {
            dataAccess.UpdateSkillCategory(SkillCategoryID, SkillCategory, description);
        }

        public void DeleteSkillCategory(int SkillCategoryID, int UserID)
        {
            dataAccess.DeleteSkillCategory(SkillCategoryID, UserID);
        }

        public List<SkillWithCategoryViewModel> GetSkillsWithCategory()
        {
            return dataAccess.GetSkillsWithCategory();
        }

        public void InsertSkill(SkillViewModel skillViewModel, string category, int UserID)
        {
            dataAccess.InsertSkill(skillViewModel, category, UserID);
        }

        public bool ValidateSkill(string SkillName)
        {
            return dataAccess.ValidateSkill(SkillName);
        }

        public void UpdateSkill(int SkillID, int CategoryID, string Skillname, int UserID)
        {
            dataAccess.UpdateSkill(SkillID, CategoryID, Skillname, UserID);
        }

        public string GetSkillCategoryByID(int CategoryID)
        {
            return dataAccess.GetSkillCategoryByID(CategoryID);
        }

        public void DeleteSkill(int SkillID, int UserID)
        {
            dataAccess.DeleteSkill(SkillID, UserID);
        }

        public List<UserViewModel> GetInterviewers()
        {
            return dataAccess.GetInterviewers();
        }

        public void UpdateInterviewer(int UserID, string UserName, string Email, string Designation, int hrID)
        {
            dataAccess.UpdateInterviewer(UserID, UserName, Email, Designation, hrID);
        }

        public void DeleteInterviewer(int UserID, int hrID)
        {
            dataAccess.DeleteInterviewer(UserID, hrID);
        }

        public bool ValidateInterviewerName(string UserName)
        {
            return dataAccess.ValidateInterviewerName(UserName);
        }

        public bool ValidateEmployeeID(string EmployeeId)
        {
            return dataAccess.ValidateEmployeeID(EmployeeId);
        }

        public List<CandidateViewModel> GetCandidates()
        {
            return dataAccess.GetCandidates();
        }

        public List<CandidateViewModel> SearchCandidate(string searchString)
        {
            return dataAccess.SearchCandidate(searchString);
        }

        public List<SelectListItem> GetCandidateInterviewersDropdown(int CandidateID)
        {
            //Used to fill the drop down with interviewers that have not taken interview for particular candidate.
            List<SelectListItem> selectedlist = new List<SelectListItem>();
            List<CandidateInterviewersViewModel> interviewers = GetCandidateInterviewers(CandidateID);
            foreach (CandidateInterviewersViewModel interviewer in interviewers)
            {
                SelectListItem selectlistitem = new SelectListItem
                {
                    Text = interviewer.UserName,
                    Value = interviewer.UserID.ToString()
                };
                selectedlist.Add(selectlistitem);
            }
            return selectedlist;
        }

        public List<SelectListItem> GetInterviewerDropdown()
        {
            List<SelectListItem> selectedlist = new List<SelectListItem>();
            foreach (var user in GetInterviewers())
            {
                SelectListItem selectlistitem = new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.UserID.ToString(),

                };
                selectedlist.Add(selectlistitem);
            }
            return selectedlist;
        }

        public void UpdateCandidate(int CandidateID, string CandidateName, DateTime DateOfInterview, string email, DateTime dateofbirth, string pan, string designation, decimal experience, string qualifications, int UserID)
        {
            dataAccess.UpdateCandidate(CandidateID, CandidateName, DateOfInterview, email, dateofbirth, pan, designation, experience, qualifications, UserID);
        }

        public void DeleteCandidate(int CandidateID, int UserID)
        {
            dataAccess.DeleteCandidate(CandidateID, UserID);
        }

        public List<NotificationViewModel> GetNotifications()
        {
            return dataAccess.GetNotifications();
        }

        public List<SelectListItem> GetRoundDropdown(int CandidateID)
        {
            List<SelectListItem> selectedlistround = new List<SelectListItem>();
            //stored procedure to list all the rounds that the candidates have not attended yet.
            //This is assigned to ViewBag.round
            List<CandidateRoundViewModel> CandidateRound = dataAccess.GetCandidateRound(CandidateID);
            foreach (CandidateRoundViewModel round1 in CandidateRound)
            {
                SelectListItem selectlistitem = new SelectListItem
                {
                    Text = round1.RoundName,
                    Value = round1.RoundID.ToString()
                };
                selectedlistround.Add(selectlistitem);
            }
            return selectedlistround;
        }



        public List<CandidateInterviewersViewModel> GetCandidateInterviewers(int CandidateID)
        {
            return dataAccess.GetCandidateInterviewers(CandidateID);
        }

        public void UpdateCandidateStatus(int CandidateID, bool status)
        {
            dataAccess.UpdateCandidateStatus(CandidateID, status);
        }

        public List<InterviewersOfCandidateViewModel> GetUpdatableInterviews()
        {
            return dataAccess.GetUpdatableInterviews();
        }

        public void UpdateCandidateInterviewer(int UserID, int CandidateID, int RoundID)
        {
            dataAccess.UpdateCandidateInterviewer(UserID, CandidateID, RoundID);
        }

        public List<InterviewersOfCandidateViewModel> SearchUpdatableInterviews(string UserName)
        {
            return dataAccess.SearchUpdatableInterviews(UserName);
        }

        public void InsertJoinDetails(JoinViewModel joinViewModel)
        {
            dataAccess.InsertJoinDetails(joinViewModel);
        }

        public List<CurrentStatusViewModel> GetCurrentStatus()
        {
            return dataAccess.GetCurrentStatus();
        }

        public List<CurrentStatusViewModel> SearchCurrentStatus(string searchString)
        {
            return dataAccess.SearchCurrentStatus(searchString);
        }

        public List<CommentViewModel> GetComments(Nullable<int> CandidateID)
        {
            return dataAccess.GetComments(CandidateID);
        }

        public int GetHRNotificationsCount()
        {
            return dataAccess.GetHRNotificationsCount();
        }

        public InterviewerDashboardViewModel GetInterviewerDashBoard(int userID)
        {
            return dataAccess.GetInterviewerDashBoard(userID);
        }

        public List<StatusViewModel> GetTodaysInterview(int UserID)
        {
            return dataAccess.GetTodaysInterview(UserID);
        }

        public List<StatusViewModel> SearchTodaysInterview(int UserID, string searchString)
        {
            return dataAccess.SearchTodaysInterview(UserID, searchString);
        }

        public List<StatusViewModel> GetRecommendedCandidates(int UserID)
        {
            return dataAccess.GetRecommendedCandidates(UserID);
        }

        public List<StatusViewModel> SearchRecommendedCandidates(int UserID, string searchString)
        {
            return dataAccess.SearchRecommendedCandidates(UserID, searchString);
        }

        public List<StatusViewModel> GetCandidatesByInterviewer(int UserID)
        {
            return dataAccess.GetCandidatesByInterviewer(UserID);
        }

        public List<StatusViewModel> SearchCandidatesByInterviewer(int UserID, string searchString)
        {
            return dataAccess.SearchCandidatesByInterviewer(UserID, searchString);
        }

        public List<StatusViewModel> GetStatus(int UserID)
        {
            return dataAccess.GetStatus(UserID);
        }

        public int UpdateEvaluation(int evaluationID, int[] ids, int[] values, string comments, bool recommended, int UserID)
        {
            dataAccess.InsertScores(evaluationID, ids, values, UserID);
            return dataAccess.UpdateEvaluation(evaluationID, comments, recommended, UserID);
        }

        public List<RatingScaleViewModel> GetRatingScale()
        {
            return dataAccess.GetRatingScale();
        }

        public List<RoundViewModel> GetRounds()
        {
            return dataAccess.GetRounds();
        }

        public List<SkillCategoryViewModel> GetSkillCategories()
        {
            return dataAccess.GetSkillCategories();
        }

        public List<SelectListItem> GetSkillCategoryDropdown()
        {
            // Select all the skill categories which are not deleted.
            var itemlist = GetSkillCategories();
            List<SelectListItem> selectedlist = new List<SelectListItem>();
            // Store the skill categories and corresponding ID's in a list.
            foreach (var skillitem in itemlist)
            {
                SelectListItem selectlistitem = new SelectListItem
                {
                    Text = skillitem.SkillCategory,
                    Value = skillitem.SkillCategoryID.ToString()
                };
                selectedlist.Add(selectlistitem);
            }
            return selectedlist;
        }

        public List<SkillViewModel> GetSkills()
        {
            return dataAccess.GetSkills();
        }

        public List<SkillViewModel> GetSkillsByCategory(int skillCategoryID)
        {
            return dataAccess.GetSkillsByCategory(skillCategoryID);
        }

        public List<ScoreEvaluationViewModel> GetPreviousRoundScores(Nullable<int> candidateID, int roundID)
        {
            return dataAccess.GetPreviousRoundScores(candidateID, roundID);
        }

        public InterviewEvaluationViewModel GetInterviewEvaluationViewModel(StatusViewModel statusViewModel)
        {
            InterviewEvaluationViewModel interviewEvaluationViewModel = new InterviewEvaluationViewModel();
            interviewEvaluationViewModel.RatingScale = GetRatingScale();
            interviewEvaluationViewModel.Rounds = GetRounds();
            interviewEvaluationViewModel.SkillCategories = GetSkillCategories();
            interviewEvaluationViewModel.Skills = GetSkills();

            //Get List of skills by skill category and save in a List<List<Skills>>
            foreach (var skillCategory in interviewEvaluationViewModel.SkillCategories)
            {
                interviewEvaluationViewModel.SkillsByCategory.Add(GetSkillsByCategory(skillCategory.SkillCategoryID));
            }

            //Get List of scores by round and save in a List<List<Scores>>
            foreach (var round in interviewEvaluationViewModel.Rounds)
            {
                List<ScoreEvaluationViewModel> scores = GetPreviousRoundScores(statusViewModel.CandidateID, round.RoundID);
                bool exists = false;
                ScoreEvaluationViewModel scoreEvaluationViewModel = new ScoreEvaluationViewModel();
                foreach (var skill in interviewEvaluationViewModel.Skills)
                {
                    exists = scores.Exists(item => item.SkillID == skill.SkillID);

                    // Check if score exists for corresponding skill
                    if (!exists)
                    {
                        scoreEvaluationViewModel.SkillID = skill.SkillID;

                        // If skill is not evaluated in previous round, display score as 0
                        scoreEvaluationViewModel.RateValue = 0;
                        scores.Add(scoreEvaluationViewModel);
                    }
                }
                interviewEvaluationViewModel.ScoresByRound.Add(scores);
            }
            interviewEvaluationViewModel.CandidateName = statusViewModel.Name;
            if (statusViewModel.Recommended != null)
            {
                //Get comments from database
                interviewEvaluationViewModel.Comments = GetComments(statusViewModel.CandidateID);
            }
            return interviewEvaluationViewModel;
        }


        /// <summary>
        /// To sent mail from mail address specified in web.config to HR mail address using smtp
        /// </summary>
        /// <param name="CandidateID"></param>
        /// <param name="UserID"></param>
        /// <param name="comments"></param>
        /// <param name="recommended"></param>
        public void SentEmailAfterFeedBack(int CandidateID, int UserID, string comments, bool recommended)
        {
            // Get Interviewer name and email, candidate name, HR email from database  
            var mailmodel = dataAccess.GetMail(CandidateID, UserID);
            string status;
            if (recommended == true)
            {
                status = "recommended";
            }
            else
            {
                status = "not recommended";
            }
            string emailBody = "<b>Interviewer: </b>" + mailmodel.UserName + "<br/>"
              + "<b>Interviewer Email : </b>" + mailmodel.Email + "<br/>"
              + "<b>Candidate : </b>" + mailmodel.Name + "<br/>"
              + "<b>Status : </b>" + status + "<br/>"
              + "<b>Comments : </b>" + comments;

            // Sent email using smtp
            SentEmail(mailmodel.HREmail, "Notification", emailBody, true);
        }

        /// <summary>
        /// To sent mail from mail address specified in web.config to Interviewer mail address using smtp
        /// </summary>
        /// <param name="CandidateID"></param>
        public void SentEmailNotification(int CandidateID, int UserID)
        {
            // Get Interviewer email, candidate name, round, date of interview from database  
            var mailmodel = dataAccess.GetInterviewerMail(CandidateID, UserID);
            string emailBody = "Hi, <br/>"
                + "You have Interview on " + mailmodel.DateOfInterview.ToShortDateString() + "<br/>"
                + "Details of interview is as follows" + "<br/>"
                + "<b>Candidate : </b>" + mailmodel.Name + "<br/>"
                + "<b>Round : </b>" + mailmodel.RoundName;
            // Sent email using smtp
            SentEmail(mailmodel.InterviewerEmail, "Notification", emailBody, true);
        }


        public void SentEmail(string To,string Subject, string Body, bool IsBodyHtml)
        {
            // Specify subject, body, sender and receiver mail address 
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(To);
            mailMessage.Subject = Subject;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = IsBodyHtml;

            // Sent mail using smtpClient
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        /// <summary>
        /// T get appValue from web.config for corresponding appKey
        /// </summary>
        /// <param name="AppKey"></param>
        public string GetAppSettingsValue(string AppKey)
        {
            string appValue = string.Empty;
            if (ConfigurationManager.AppSettings[AppKey] != null)
            {
                appValue = Convert.ToString(ConfigurationManager.AppSettings[AppKey]);
            }
            return appValue;
        }


        /// <summary>
        /// To generate otp
        /// </summary>
        public string GetOtp()
        {
            string otp;
            Random r = new Random();
            //Set of values to be used in OTP.
            const string pool = Constants.otpPool;
            var builder = new StringBuilder();

            //Specify the length of OTP.
            int length = 7;
            for (var i = 0; i < length; i++)
            {
                //Generate each character/number in OTP.
                var c = pool[r.Next(0, pool.Length)];

                //Append each character /number to OTP.
                builder.Append(c);
            }
            otp = builder.ToString();
            return otp;
        }

        public string[] InsertInterviewer(UserViewModel user, int UserID)
        {
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "sha1");
            var passwordLength = GetAppSettingsValue("UserPasswordLength");
            var userNameLength = GetAppSettingsValue("UserNameLength");
            var employeeIdLength = GetAppSettingsValue("EmployeeIdLength");
            user.UserTypeID = 2;
            bool passwordValid = true, userNameValid = true, employeeIdValid = true;
            string[] results = new string[4];
            if (user.Password.Length < Convert.ToInt32(passwordLength))
            {
                passwordValid = false;
                results[0] = string.Format(Constants.passwordValidation, passwordLength);
            }
            if (user.UserName.Length < Convert.ToInt32(userNameLength))
            {
                userNameValid = false;
                results[1] = string.Format(Constants.userNameValidation, userNameLength);
            }
            if (user.EmployeeId.Length > Convert.ToInt32(employeeIdLength))
            {
                employeeIdValid = false;
                results[2] = string.Format(Constants.employeeIDValidation, employeeIdLength);
            }
            if (passwordValid == true && userNameValid == true && employeeIdValid == true)
            {
                dataAccess.InsertInterviewer(user, hashedPwd, UserID);
                results[2] = Constants.interviewerAdded;
            }
            return results;
        }

        public CandidateViewModel GetAddCandidateViewModel()
        {
            CandidateViewModel addCandidateViewModel = new CandidateViewModel();
            addCandidateViewModel.CandidatesList = dataAccess.GetCandidates();
            addCandidateViewModel.users = dataAccess.GetInterviewers();
            return addCandidateViewModel;
        }

        public string InsertCandidate(CandidateViewModel candidateView, string user, string[] txtBoxes, int UserID)
        {
            //Stored procedure to get the minimum round id.
            int Round1ID = dataAccess.GetMinimumRoundID();
            string message = "";
            if (Round1ID == 0)
            {
                message = Constants.roundError;
            }
            else
            {
                if (user != null)
                {
                    //Insertion into candidate table
                    int candidateID = dataAccess.InsertCandidate(candidateView, UserID);
                    if (candidateView.TotalExperience > 0)
                    {
                        //Insertion into previous company table
                        dataAccess.InsertPreviousCompanies(candidateID, txtBoxes, UserID);
                    }
                    //Insertion into evaluation table
                    dataAccess.InsertEvaluation(Convert.ToInt32(user), candidateID, Round1ID, UserID);

                    // Call SentEmailNotification method to sent mail to notify Interviewer
                    SentEmailNotification(candidateID, Convert.ToInt32(user));
                }
            }
            return message;
        }
    }
}
