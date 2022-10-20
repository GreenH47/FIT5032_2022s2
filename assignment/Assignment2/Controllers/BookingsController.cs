﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Models;
using Assignment2.Utils;
using Microsoft.AspNet.Identity;

namespace Assignment2.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private AppointmentContainer db = new AppointmentContainer();

        // GET: Bookings
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return Redirect("/Account/Login");
            }
            var bookings = db.Bookings.Include(b => b.Patient).Include(b => b.Doctor);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName");
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,PatientId,DoctorId")] Booking booking)
        {
            // check if conflict
            var find = db.Bookings.Where(s => s.DoctorId == booking.DoctorId).ToList();
            var dates = db.Bookings.Where(s => s.Date == booking.Date).ToList();
            if (find.Intersect(dates).Any())
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();

                EmailSender es = new EmailSender();
                String subject = "Your dental appointment is in " + booking.Date;
                string contents = "Your new dental appointment in" + booking.Date;
                HttpPostedFileBase attachment = null;
                es.Send("shua0098@student.monash.edu", subject, contents, attachment);

                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName", booking.PatientId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName", booking.DoctorId);

            
            

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName", booking.PatientId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName", booking.DoctorId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,PatientId,DoctorId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName", booking.PatientId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "FirstName", booking.DoctorId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
