using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEventCalendar.Controllers
{
    public class ClassroomDetailsController : Controller
    {
         ClassroomAllocationSystemEntities entities = new ClassroomAllocationSystemEntities();
        // GET: ClassroomDetails
        Question question = new Question();
        public ActionResult AddClassroomDetails()
        {
            ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");
            Resource resource = new Resource();
            ClassRoom classRoom = new ClassRoom();
            ViewBag.AllResourceId = new SelectList(entities.Resources.ToList(), "ResourceId", "ResourceName");
            ViewBag.AllClassroomID = new SelectList(entities.ClassRooms.ToList(), "ClassRoomId", "ClassroomName");
      
            return View();
           
        }

        [HttpPost]

        public ActionResult AddClassroomDetails(FormCollection form)
        {
            ClassRoomDetail classRoom = new ClassRoomDetail();
            ClassRoom room = new ClassRoom();
           // room.ClassRoomName = form["ClassRoomName"];
            //classRoom.SeatingCapacity = Convert.ToInt32(form["SeatingCapacity"]);
            classRoom.ResourceId = Convert.ToInt32(form["AllResourceId"]);
            classRoom.ResourceQuantity = Convert.ToInt32(form["ResourceQuantity"]);
            classRoom.ClassroomId = Convert.ToInt32(form["AllClassroomID"]);
           //entities.ClassRooms.Add(room);
            entities.ClassRoomDetails.Add(classRoom);
            entities.SaveChanges();
            return RedirectToAction("Welcome","Resource");
        }

        public PartialViewResult DeleteClassroomDetails()
        {
            ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");
            return PartialView("_ConfirmDeleteClassroomDetails", entities.ClassRoomDetails);
        }

        public ActionResult ConfirmDeleteClassroomDetails(int id)
        {

            var user = entities.ClassRoomDetails.Where(e=>e.DetailsId==id).FirstOrDefault();
            entities.ClassRoomDetails.Remove(user);
            entities.SaveChanges();
            return RedirectToAction("Welcome","Resource");
        }


        //public ActionResult UpdateClassroomDetails(int id)
        //{
        //    ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");
        //    ClassRoomDetail detail = entities.ClassRoomDetails.Find(id);
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult UpdateClassroomDetails(ClassRoomDetail detail)
        //{
        //    ClassRoomDetail roomDetail = entities.ClassRoomDetails.Where(e => e.DetailsId == detail.DetailsId).FirstOrDefault();
        //    roomDetail.SeatingCapacity = detail.SeatingCapacity;
        //    roomDetail.ResourceQuantity = detail.ResourceQuantity;
        //    entities.SaveChanges();
        //    return RedirectToAction("Welcome", "Resource");
        //}
    }
}