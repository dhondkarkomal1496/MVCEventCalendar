using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVCEventCalendar.Controllers
{
    public class AdminController : Controller
    {
        ClassroomAllocationSystemEntities entities = new ClassroomAllocationSystemEntities();

        Question question = new Question();
        // GET: Admin
        public ActionResult AdminLogin()
        {
            ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");
            return View();
        }

        public ActionResult ShowReportToAdmin()
        {
            ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");

            return View();
        }
    }
}