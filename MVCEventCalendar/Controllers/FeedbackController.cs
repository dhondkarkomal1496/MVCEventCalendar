using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEventCalendar.Controllers
{
    public class FeedbackController : Controller
    {
        ClassroomAllocationSystemEntities entities = new ClassroomAllocationSystemEntities();
        // GET: Feedback
        Question question = new Question();
        public ActionResult Index()
        {
            ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");
            var result = entities.GetFeedback().ToList();
            ViewBag.Allfeedback = result;
            return View();
        }
    }
}