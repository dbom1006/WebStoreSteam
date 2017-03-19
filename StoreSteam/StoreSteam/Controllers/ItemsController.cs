using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreSteam.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoreSteam.Controllers
{
    public class ItemsController : Controller
    {
        private StoreSteamContext db = new StoreSteamContext();
        private ApiItem apiItem = new ApiItem();
        // GET: Items
        public ActionResult Index()
        {
            return View(apiItem.List(570));
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AppId,ClassId,Name,IconUrl,Color,Type,Tradable,Marketable")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AppId,ClassId,Name,IconUrl,Color,Type,Tradable,Marketable")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
    public class ApiItem
    {
        private StoreSteamContext db = new StoreSteamContext();
        private string ApiKey
        {
            get
            {
                return db.ApiKeys.FirstOrDefault().Key;
            }
        }
        private string SteamId
        {
            get
            {
                return db.ApiKeys.FirstOrDefault().SteamId;
            }
        }
        public List<Item> List(int appId)
        {
            List<Item> lst = new List<Item>();
            string url= String.Format(@"http://steamcommunity.com/inventory/{0}/{1}/2/", 76561198145839778, 570);
            SteamWeb steamWeb = new SteamWeb();
            string response = steamWeb.Fetch(url, "GET");
            try
            {
                dynamic x= JsonConvert.DeserializeObject(response);
                var y = x.assets;

                foreach (var i in y)
                {
                    string classid = i.classid;
                    url= String.Format(@"https://api.steampowered.com/ISteamEconomy/GetAssetClassInfo/v1/?key={0}&format=json&appid={1}&class_count=1&classid0={2}", ApiKey, appId, classid);
                    response = steamWeb.Fetch(url, "GET");
                    dynamic obj = JsonConvert.DeserializeObject(response);
                    var obj2 = JsonConvert.DeserializeObject<IDictionary<string,dynamic>>(((object) obj.result).ToString());
                    if (obj2["success"] == true)
                    {
                        Item item = JsonConvert.DeserializeObject<Item>(((object)obj2[classid]).ToString());
                        item.AppId = appId;
                        dynamic it = JsonConvert.DeserializeObject(((object)obj2[classid]).ToString());
                        item.ListTag = new List<Tag>();
                        foreach (var tag in it.tags)
                        {
                            Tag a = JsonConvert.DeserializeObject<Tag>(((object)tag.Value).ToString());
                            item.ListTag.Add(a);
                        }
                        item.ListAction = new List<Models.Action>();
                        if (it.actions != null)
                            foreach (var action in it.actions)
                            {
                                Models.Action a = JsonConvert.DeserializeObject<Models.Action>(((object)action.Value).ToString());
                                item.ListAction.Add(a);
                            }
                        lst.Add(item);
                    }
                    
                }
                return lst;
            }
            catch (Exception)
            {
                return new List<Item>();
            }
        }
    }
}
