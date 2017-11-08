﻿using InterviewEvaluationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InterviewEvaluationSystem.Business_Logic;
using System.Web.Helpers;
using System.Net.Mail;
using System.Configuration;

namespace InterviewEvaluationSystem.Controllers
{
    public class InterviewerController : Controller
    {
        #region Fields

        /// <summary>
        /// Declare Db Entity to connect to database
        /// </summary>
        InterviewEvaluationDbEntities dbContext = new InterviewEvaluationDbEntities();

        /// <summary>
        /// Declare Service class that contains methods to implement business logic
        /// </summary>
        Services services = new Services();

        #endregion

        #region Home Page 

        /// <summary>
        /// To get Counts of New Candidates, Today's interviews, Hired candidates, Total candidates from database to display in dash board
        /// </summary>
        [HttpGet]
        public ActionResult HomePage()
        {
            try
            {
                InterviewerDashboardViewModel interviewerDashBoardViewModel = new InterviewerDashboardViewModel();
                var interviewerDashBoard = dbContext.spGetInterviewerDashBoard(Convert.ToInt32(Session["UserID"])).Single();
                interviewerDashBoardViewModel.NewCandidateCount = interviewerDashBoard.NewCandidateCount;
                interviewerDashBoardViewModel.TodaysInterviewCount = interviewerDashBoard.TodaysInterviewCount;
                interviewerDashBoardViewModel.HiredCandidateCount = interviewerDashBoard.HiredCandidateCount;
                interviewerDashBoardViewModel.TotalCandidateCount = interviewerDashBoard.TotalCandidateCount;
                return View(interviewerDashBoardViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "Login"));
            }
        }

        #endregion

        #region Chart

        /// <summary>
        /// To Create pie chart based on the data from database for a particular year
        /// </summary>
        /// <param name="year"></param>
        [HttpGet]
        public void ChartPie(int year)
        {
            var result = dbContext.spGetInterviewerPieChart(Convert.ToInt32(Session["UserID"]), year).Single();
            if (result.Hired != 0 || result.InProgress != 0 || result.Rejected != 0)
            {
                // Use Chart class to create a pie chart image based on an array of values
                Chart chart = new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
                .AddLegend("Summary")
                .AddSeries("Default", chartType: "doughnut", xValue: new[] { (result.InProgress != 0) ? "Inprogress - #PERCENT{P0}" : "", (result.Hired != 0) ? "Recoommended - #PERCENT{P0}" : "", (result.Rejected != 0) ? "Rejected - #PERCENT{P0}" : "" }, yValues: new[] { result.InProgress, result.Hired, result.Rejected })
                .Write("bmp");
            }
        }

        /// <summary>
        /// To Create column chart based on the data from database for a particular year
        /// </summary>
        /// <param name="year"></param>
        [HttpGet]
        public void ChartColumn(int year)
        {
            var result = dbContext.spGetCloumnChart(year).Single();
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

        #endregion

        #region Today's Interviews

        /// <summary>
        /// To display details of candidates having interview today
        /// </summary>
        public ActionResult ViewTodaysInterviews()
        {
            try
            {
                List<StatusViewModel> TodaysInterviews = services.GetTodaysInterview(Convert.ToInt32(Session["UserID"]));
                return View(TodaysInterviews);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "HomePage"));
            }
        }

        /// <summary>
        /// To do case insensitive search based on Candidate Name filter
        /// </summary>
        /// <param name="searchString"></param>
        [HttpPost]
        public ActionResult ViewTodaysInterviews(string searchString)
        {
            List<StatusViewModel> TodaysInterviews = services.GetTodaysInterview(Convert.ToInt32(Session["UserID"]));
            // Check if search string is not empty or null
            if (!String.IsNullOrEmpty(searchString))
            {
                // Get details of candidates whose name starts with search string given
                TodaysInterviews = TodaysInterviews.Where(s => s.Name.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            return View(TodaysInterviews);
        }

        #endregion

        #region Recommended Candidates

        /// <summary>
        /// To display details of candidates recommended by the interviewer
        /// </summary>
        public ActionResult ViewRecommendedCandidates()
        {
            try
            {
                List<StatusViewModel> RecommendedCandidates = services.GetRecommendedCandidates(Convert.ToInt32(Session["UserID"]));
                return View(RecommendedCandidates);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "HomePage"));
            }
        }

        /// <summary>
        /// To do case insensitive search based on Candidate Name filter
        /// </summary>
        /// <param name="searchString"></param>
        [HttpPost]
        public ActionResult ViewRecommendedCandidates(string searchString)
        {
            List<StatusViewModel> RecommendedCandidates = services.GetRecommendedCandidates(Convert.ToInt32(Session["UserID"]));
            // Check if search string is not empty or null
            if (!String.IsNullOrEmpty(searchString))
            {
                // Get details of candidates whose name starts with search string given
                RecommendedCandidates = RecommendedCandidates.Where(s => s.Name.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            return View(RecommendedCandidates);
        }

        #endregion

        #region Total Candidates

        /// <summary>
        /// To display details of all Candidates interviewed by the interviewer
        /// </summary>
        public ActionResult ViewCandidates()
        {
            try
            {
                List<StatusViewModel> candidates = services.GetCandidates(Convert.ToInt32(Session["UserID"]));
                return View(candidates);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "HomePage"));
            }
        }

        /// <summary>
        /// To do case insensitive search based on filters - Candidate Name and email
        /// </summary>
        /// <param name="searchString"></param>
        [HttpPost]
        public ActionResult ViewCandidates(string searchString)
        {
            List<StatusViewModel> candidates = services.GetCandidates(Convert.ToInt32(Session["UserID"]));
            // Check if search string is not empty or null
            if (!String.IsNullOrEmpty(searchString))
            {
                // Get details of candidates whose name or email starts with search string given
                candidates = candidates.Where(s => s.Name.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            return View(candidates);
        }
        #endregion

        #region Evaluation Status

        /// <summary>
        /// To get Details of Candidates assigned to the interviewer, whose evaluation is not completed
        /// </summary>
        public ActionResult EvaluationStatus()
        {
            try
            {
                List<StatusViewModel> Statuses = services.GetStatus(Convert.ToInt32(Session["UserID"]));
                return View(Statuses);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "HomePage"));
            }
        }

        #endregion

        #region Interview Evaluation

        /// <summary>
        /// To get rating scales, rounds, skill categories and skills by skill category from database
        /// To get scores by round and comments of previous rounds from database
        /// </summary>
        public ActionResult InterviewEvaluation(StatusViewModel statusViewModel, string Name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InterviewEvaluationViewModel interviewEvaluationViewModel = new InterviewEvaluationViewModel();
                    interviewEvaluationViewModel.RatingScale = services.GetRatingScale();
                    interviewEvaluationViewModel.Rounds = services.GetRounds();
                    interviewEvaluationViewModel.SkillCategories = services.GetSkillCategories();
                    interviewEvaluationViewModel.Skills = services.GetSkills();
                    // Get List of skills by skill category and save in a List<List<Skills>>
                    foreach (var skillCategory in interviewEvaluationViewModel.SkillCategories)
                    {
                        interviewEvaluationViewModel.SkillsByCategory.Add(services.GetSkillsByCategory(skillCategory.SkillCategoryID));
                    }
                    // Get List of scores by round and save in a List<List<Scores>>
                    foreach (var round in interviewEvaluationViewModel.Rounds)
                    {
                        interviewEvaluationViewModel.ScoresByRound.Add(services.GetPreviousRoundScores(statusViewModel.CandidateID, round.RoundID));
                    }
                    interviewEvaluationViewModel.CandidateName = statusViewModel.Name;
                    // Store CandidateID, RoundID, EvaluationID in TempData
                    TempData["CandidateID"] = statusViewModel.CandidateID;
                    TempData["roundID"] = statusViewModel.RoundID;
                    TempData["evaluationID"] = statusViewModel.EvaluationID;
                    return View(interviewEvaluationViewModel);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "EvaluationStatus"));
            }
        }

        /// <summary>
        /// To insert rate scale values for each skills to Score table in database
        /// To update comments and recommended(yes/no) in Evaluation table in database
        /// Returns interviewer name and email, candidate name, HR email, Subject, status and comments to sent mail
        /// Redirect to SentEmailNotification
        /// </summary>
        /// <param name="recommended"></param>
        /// <param name="evaluationID"></param>
        /// <param name="ids">IDs of skills that are evaluated</param>
        /// <param name="values">Rate scale values of each skills</param>
        /// <param name="comments"></param>
        [HttpPost]
        public ActionResult InterviewEvaluation(bool recommended, int evaluationID, int[] ids, int[] values, string comments)
        {
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    dbContext.tblScores.Add(new tblScore
                    {
                        EvaluationID = evaluationID,
                        SkillID = ids[i],
                        RateScaleID = values[i],
                        CreatedBy = Convert.ToInt32(Session["UserID"]),
                        CreatedDate = DateTime.Now
                    });
                    dbContext.SaveChanges();
                }
                int EvaluationID = Convert.ToInt16(evaluationID);
                tblEvaluation evaluation = dbContext.tblEvaluations.Where(e => e.EvaluationID == EvaluationID).Single();
                evaluation.Comment = comments;
                evaluation.Recommended = recommended;
                evaluation.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                evaluation.ModifiedDate = DateTime.Now;
                dbContext.SaveChanges();
                // Get Interviewer name and email, candidate name, HR email from database  
                MailViewModel mailViewModel = new MailViewModel();
                var mailmodel = dbContext.spGetEmailByUserID(evaluation.CandidateID, Convert.ToInt32(Session["UserID"])).FirstOrDefault();
                mailViewModel.Interviewer = mailmodel.UserName;
                mailViewModel.Candidate = mailmodel.Name;
                mailViewModel.InterviewerEmail = mailmodel.Email;
                mailViewModel.HREmail = mailmodel.HREmail;
                string status;
                if (recommended == true)
                {
                    status = "recommended";
                }
                else
                {
                    status = "not recommended";
                }
                // Redirect to SentEmailNotification method and supply interviewer name and email, candidate name, HR email, Subject, status and comments
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("SentEmailNotification", "Interviewer", new
                {
                    InterviewerEmail = mailViewModel.InterviewerEmail,
                    HREmail = mailViewModel.HREmail,
                    Interviewer = mailViewModel.Interviewer,
                    Candidate = mailViewModel.Candidate,
                    Subject = "Notification",
                    Status = status,
                    Comments = comments,
                });
                return Json(new { Url = redirectUrl });
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "InterviewEvaluation"));
            }
        }

        #endregion

        #region Email Notification

        /// <summary>
        /// To sent mail from mail address specified in web.config to HR mail address using smtp
        /// </summary>
        /// <param name="mailViewModel"></param>
        public ActionResult SentEmailNotification(MailViewModel mailViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get sender mail address and password from web.config
                    var sender = ConfigurationManager.AppSettings["EmailSender"];
                    var emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
                    // Specify subject, body, sender and receiver mail address 
                    MailMessage mailMessage = new MailMessage(sender, mailViewModel.HREmail);
                    mailMessage.Subject = mailViewModel.Subject;
                    mailMessage.Body = "<b>Interviewer: </b>" + mailViewModel.Interviewer + "<br/>"
                      + "<b>Interviewer Email : </b>" + mailViewModel.InterviewerEmail + "<br/>"
                      + "<b>Candidate : </b>" + mailViewModel.Candidate + "<br/>"
                      + "<b>Status : </b>" + mailViewModel.Status + "<br/>"
                      + "<b>Comments : </b>" + mailViewModel.Comments;
                    mailMessage.IsBodyHtml = true;
                    // Specify the SMTP server name and post number
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    // Specify sender mail address and password
                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = sender,
                        Password = emailPassword
                    };
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                    TempData["Success"] = "Review submitted Successfully!";
                    return RedirectToAction("HomePage", "Interviewer");
                }
                else
                {
                    return RedirectToAction("HomePage", "Interviewer");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Interviewer", "InterviewEvaluation"));
            }
        }

        #endregion

    }
}