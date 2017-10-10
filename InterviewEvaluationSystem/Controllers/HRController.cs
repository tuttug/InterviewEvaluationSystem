﻿using InterviewEvaluationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewEvaluationSystem.Controllers
{
    public class HRController : Controller
    {
        // GET: HR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RatingScale()
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            List<tblRatingScale> rate = db.tblRatingScales.ToList();
            ViewBag.Roles = rate;
            //db.Configuration.ProxyCreationEnabled = false;
            //var data = db.tblRatingScales;
            //return View(data.ToList());
            return View();
        }

        [HttpPost]
        public ActionResult RatingScale(tblRatingScale rate)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            rate.CreatedBy = "hr";
           // rate.ModifiedBy="hr";
            rate.CreatedDate = DateTime.Now;
          //  rate.ModifiedDate = DateTime.Now;
           // rate.IsDeleted = 0;
            db.tblRatingScales.Add(rate);
            db.SaveChanges();
            List<tblRatingScale> rates = db.tblRatingScales.ToList();
            ViewBag.Roles = rates;
            return View();
        }

        [HttpPost]
        public JsonResult RateEdit(int RateScaleId,string Ratescale, int Ratevalue,string description)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            tblRatingScale rate = db.tblRatingScales.Find(RateScaleId);
            rate.RateScale = Ratescale;
            rate.RateValue = Ratevalue;
            rate.Description = description;
            db.SaveChanges();
            return Json(new { rate }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RateDelete(int id)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            var rate = (from u in db.tblRatingScales
                       where id == u.RateScaleID
                       select u).FirstOrDefault();
            var list = (from u in db.tblRatingScales
                        where id != u.RateScaleID
                        select u).ToList();
            Session["Rates"] = list;
            db.tblRatingScales.Remove(rate);
            db.SaveChanges();
            bool result = true;
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SkillCategory()
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            List<tblSkillCategory> category = db.tblSkillCategories.ToList();
            ViewBag.Roles = category;
            return View();
            //var data = db.tblSkillCategories;
            //return View(data.ToList());

        }
        [HttpPost]
        public ActionResult SkillCategory(tblSkillCategory category)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            category.CreatedBy = "hr";
            category.CreatedDate = DateTime.Now;
            db.tblSkillCategories.Add(category);
            db.SaveChanges();
            List<tblSkillCategory> cat = db.tblSkillCategories.ToList();
            ViewBag.Roles = cat;
            return View();
  
        }

        [HttpPost]
        public JsonResult CategoryEdit(int SkillCategoryID, string SkillCategory, string description)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            tblSkillCategory category = db.tblSkillCategories.Find(SkillCategoryID);
            category.SkillCategory = SkillCategory;
            category.Description = description;
            db.SaveChanges();
            return Json(new { category }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CategoryDelete(int id)
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            var category = (from u in db.tblSkillCategories
                        where id == u.SkillCategoryID
                        select u).FirstOrDefault();
            var list = (from u in db.tblSkillCategories
                        where id != u.SkillCategoryID
                        select u).ToList();
            Session["Categories"] = list;
            db.tblSkillCategories.Remove(category);
            db.SaveChanges();
            bool result = true;
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Skill()
        {
            InterviewEvaluationDbEntities db = new InterviewEvaluationDbEntities();
            NewModel obj = new NewModel
            {
                List1 = db.tblSkillCategories.ToList(),
                List2 = db.tblSkills.ToList()
            };
            ViewBag.Roles = obj;
            return View(obj);
        }
    }
}