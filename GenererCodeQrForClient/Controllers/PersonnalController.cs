using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GenererCodeQrForClient.DAL;
using GenererCodeQrForClient.Models;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace GenererCodeQrForClient.Controllers
{
    public class PersonnalController : Controller
    {
        private PersonnalContext db = new PersonnalContext();

        // GET: Personnal
        public ActionResult Index()
        {
            return View(db.Personnel.ToList());
        }


        public ActionResult GetList()
        {
            using (PersonnalContext db = new PersonnalContext())
            {
                var empList = db.Personnel.ToList();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Courses/Details/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelModel personnal = db.Personnel.Find(id);
            if (personnal == null)
            {
                return HttpNotFound();
            }
            return View(personnal);
        }


        [HttpPost]
        public ActionResult Detail(string firstname,string lastname,string email,string number, int id)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                string qrcode = "";

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                qrcode += "Name : " + firstname + "\n";
                qrcode += "Last Name : "+ lastname + "\n";
                qrcode += "Email : "+ email + "\n";
                qrcode += "Number : "+ number + "\n";
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap qrCodeImage = qrCode.GetGraphic(80))
                {
                    qrCodeImage.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            PersonnelModel personnal = db.Personnel.Find(id);
            if (personnal == null)
            {
                return HttpNotFound();
            }
            return View(personnal);

        }

        // GET: Personnal/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Personnal/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Number")] PersonnelModel personnal)
        {
            if (ModelState.IsValid)
            {
                db.Personnel.Add(personnal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personnal);
        }


        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelModel personnal = db.Personnel.Find(id);
            if (personnal == null)
            {
                return HttpNotFound();
            }
            return View(personnal);
        }

        // POST: Courses/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Number")] PersonnelModel personnal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personnal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personnal);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelModel personnal = db.Personnel.Find(id);
            if (personnal == null)
            {
                return HttpNotFound();
            }
            return View(personnal);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonnelModel personnal = db.Personnel.Find(id);
            db.Personnel.Remove(personnal);
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