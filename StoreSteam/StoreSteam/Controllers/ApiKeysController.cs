using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreSteam.Models;

namespace StoreSteam.Controllers
{
    public class ApiKeysController : Controller
    {
        private StoreSteamContext db = new StoreSteamContext();

        // GET: ApiKeys
        public ActionResult Index()
        {
            return View(db.ApiKeys.ToList());
        }

        // GET: ApiKeys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiKey apiKey = db.ApiKeys.Find(id);
            if (apiKey == null)
            {
                return HttpNotFound();
            }
            return View(apiKey);
        }

        // GET: ApiKeys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiKeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Key,SteamId")] ApiKey apiKey)
        {
            if (ModelState.IsValid)
            {
                db.ApiKeys.Add(apiKey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apiKey);
        }

        // GET: ApiKeys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiKey apiKey = db.ApiKeys.Find(id);
            if (apiKey == null)
            {
                return HttpNotFound();
            }
            return View(apiKey);
        }

        // POST: ApiKeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Key,SteamId")] ApiKey apiKey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apiKey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apiKey);
        }

        // GET: ApiKeys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiKey apiKey = db.ApiKeys.Find(id);
            if (apiKey == null)
            {
                return HttpNotFound();
            }
            return View(apiKey);
        }

        // POST: ApiKeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApiKey apiKey = db.ApiKeys.Find(id);
            db.ApiKeys.Remove(apiKey);
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
