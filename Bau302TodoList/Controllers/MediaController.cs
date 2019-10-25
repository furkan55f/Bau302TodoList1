using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bau302TodoList.Models;
using System.IO;

namespace Bau302TodoList.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Media
        public async Task<ActionResult> Index()
        {
            return View(await db.Medias.ToListAsync());
        }

        // GET: Media/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // GET: Media/Create
        public ActionResult Create()
        {
            var media = new Media();
            return View(media);
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Extension,FilePath,FileSize,Year,Month,ContentType,CreateDate,CreateBy,UpdateDate,UpdateBy")] Media media)
        {
            if (ModelState.IsValid)
            {
                media.CreateDate = DateTime.Now;
                media.CreateBy = User.Identity.Name;
                media.UpdateDate = DateTime.Now;
                media.UpdateBy = User.Identity.Name;

                //Upload İşlemi ******************
                if (!String.IsNullOrEmpty(media.FilePath))
                {
                    FileInfo fileinfo = new FileInfo(Server.MapPath("~" + media.FilePath));
                    media.FileSize = ((float)fileinfo.Length) / ((float)1024);
                    media.Extension = fileinfo.Extension;
                    media.ContentType = fileinfo.Extension;
                }
                db.Medias.Add(media);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(media);
        }

        // GET: Media/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Extension,FilePath,FileSize,Year,Month,ContentType,CreateDate,CreateBy,UpdateDate,UpdateBy")] Media media)
        {
            if (ModelState.IsValid)
            {
                media.UpdateDate = DateTime.Now;
                media.UpdateBy = User.Identity.Name;

                //Upload İşlemi ******************
                if (!String.IsNullOrEmpty(media.FilePath))
                {
                    FileInfo fileinfo = new FileInfo(Server.MapPath("~" + media.FilePath));
                    media.FileSize = ((float)fileinfo.Length) / ((float)1024);
                    media.Extension = fileinfo.Extension;
                    media.ContentType = fileinfo.Extension;
                }

                db.Entry(media).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(media);
        }
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = " ";
            string categoryFolder = " ";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        var uploadLocation = Server.MapPath("~/uploads");
                        categoryFolder = "/" + DateTime.Now.Year + "-" + DateTime.Now.Month + "/";
                        fName = file.FileName;
                        var extension = Path.GetExtension(fName).ToLower();
                        var contentType = file.ContentType;
                        float fileSize = ((float)file.ContentLength) / ((float)1024);
                        if (!Directory.Exists(uploadLocation + categoryFolder))
                        {
                            Directory.CreateDirectory(uploadLocation + categoryFolder);
                        }
                        if (!System.IO.File.Exists(uploadLocation + categoryFolder + fName))
                        {
                            file.SaveAs(uploadLocation + categoryFolder + fName);
                        }
                        else
                        {
                            throw new Exception("Dosya zaten mevcut.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = "/uploads" + categoryFolder + fName, success = true });
            }
            else
            {
                return Json(new { Message = "Hata oldu, Dosya kaydedilemedi.", success = false });
            }
        }

        // GET: Media/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Media media = await db.Medias.FindAsync(id);
            db.Medias.Remove(media);
            await db.SaveChangesAsync();
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
