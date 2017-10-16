﻿using InterviewEvaluationSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace InterviewEvaluationSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblUser user)
        {
                InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
                var count = db.LoginProcedure(user.UserName, user.Password);
                var item = count.FirstOrDefault();
                int usercount = Convert.ToInt32(item);
                var usertype = (from s in db.tblUsers where s.UserName == user.UserName select s).FirstOrDefault();
                int id = Convert.ToInt32(usertype.UserTypeID);
                if (usercount == 1)
                {
                    //FormsAuthentication.SetAuthCookie(user.UserName, false);
                    Session["Name"] = user.UserName;
                    if (id == 1)
                    {
                        return RedirectToAction("HRHomePage", "HR");

                    }
                    else if (id == 2)
                    {
                        return RedirectToAction("HomePage", "Interviewer");

                    }

                }
                else
                {
                    Response.Write("Invalid credentials");
                }
                //InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
                //var item = (from s in db.tblUsers where s.UserName == username && s.Password == password select s).FirstOrDefault();
                //if (item != null)
                //{
                //    Session["Name"] = item.UserName.ToString();
                //    return RedirectToAction("HRHomePage","HR");
                //}
                //else
                //{
                //    Response.Write("Sorry,invalid credentials");
                //}
            
            return View();
        }

        [HttpGet]
        public ActionResult ProfileUpdate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProfileUpdate(tblUser user)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            var name = Convert.ToString(Session["Name"]);
            var item = (from s in db.tblUsers where s.UserName == name select s).FirstOrDefault();
            item.Address = user.Address;
            item.Pincode = user.Pincode;
            db.SaveChanges();
            int id = Convert.ToInt32(item.UserTypeID);
            if (id == 1)
            {
                return RedirectToAction("HRHomePage", "HR");
            }
            else
            {
                return RedirectToAction("HomePage", "Interviewer");
            }
        }


        [HttpGet]
        public ActionResult Logout()
        {
            return View();
        }
        public ActionResult Logout(tblUser user)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            var name = Convert.ToString(Session["Name"]);
            var item = (from s in db.tblUsers where s.UserName == name select s).FirstOrDefault();
            int id = Convert.ToInt32(item.UserTypeID);
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            if (id == 1)
            {
                return RedirectToAction("HRHomePage", "HR");
            }
            else
            {
                return RedirectToAction("HomePage", "Interviewer");
            }
        }


        public ActionResult ViewProfile()
        {
            
                InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
                var name = Convert.ToString(Session["Name"]);
                var item = (from s in db.tblUsers where s.UserName == name select s).FirstOrDefault();
                // int id = Convert.ToInt32(item.UserTypeID);
                ViewBag.Details = item;
            
                return View();
        }
        
    }
}