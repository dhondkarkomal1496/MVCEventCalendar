using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEventCalendar.Controllers
{
    public class UserController : Controller
    {
        ClassroomAllocationSystemEntities dc = new ClassroomAllocationSystemEntities();

        Question question = new Question();
        // GET: User
        public ActionResult UserLogin()
        {
            if (Session["EmployeeNumber"] != null) { 
            ViewBag.AllQuestions = new SelectList(dc.Questions.ToList(), "Question1", "Question1");
            ViewBag.data1 = new SelectList(dc.ClassRooms.ToList(), "ClassRoomId", "ClassroomName");
                return View();
            }
            else { 
            return Content("<script language='javascript' type='text/javascript'>alert('Login');window.location = '/Home/Index';</script>");
            }



        }

        public JsonResult GetEvents()
        {


            var events = (from e in dc.Events
                          join c in dc.ClassRooms
                           on e.ClassRoomId equals c.ClassRoomId
                          select new
                          {
                              e.EventID,
                              e.Subject,
                              e.Description,
                              e.Start,
                              e.End,
                            
                              e.IsfullDay,
                              e.ClassRoomId,
                              c.ClassRoomName,
                              e.Employeeid

                          }).ToList();




            //var events = dc.Events.ToList();

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public ActionResult SaveEvent(Event e)
        {
            var status = false;
            using (ClassroomAllocationSystemEntities dc = new ClassroomAllocationSystemEntities())
            {
               
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsfullDay = e.IsfullDay;
                       
                        v.ClassRoomId = e.ClassRoomId;
                        v.Employeeid = e.Employeeid;
                        dc.SaveChanges();
                        status = true;

                    }
                }
                else
                {
                    var count = dc.ValidateBookingClassroom(e.Start, e.End, e.ClassRoomId).ToList();
                    int? save = count.FirstOrDefault();
                    Int32 insert = save.HasValue ? save.Value : 0;
                    if (insert == 0) { 
                    dc.Events.Add(e);
                        dc.SaveChanges();

                        status = true;
                        
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {

            var status = false;
            using (ClassroomAllocationSystemEntities dc = new ClassroomAllocationSystemEntities())
            {

                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status }};
        }

        public ActionResult AllRooms()
        {
            if (Session["EmployeeNumber"] != null)
            {
                ViewBag.AllQuestions = new SelectList(dc.Questions.ToList(), "Question1", "Question1");

                return View(dc.ClassRooms.ToList());
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Login');window.location = '/Home/Index';</script>");

            }
        }
    }
}