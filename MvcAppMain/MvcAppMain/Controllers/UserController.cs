using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MvcAppMain.Models;
using System.Web.Security;
using MvcAppMain.Filters;

namespace MvcAppMain.Controllers
{
    public class UserController : Controller
    {
        QLPMContext db = new QLPMContext();

        // GET: User
        [AdminFilter]
        public ActionResult Index()
        {
            
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        [AdminFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        [AdminFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_User,Username,Password,Roles")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        [AdminFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_User,Username,Password,Roles")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        [AdminFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [AdminFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user) {
            if (ModelState.IsValid) {
                using (QLPMContext db = new QLPMContext()) {
                    var obj = db.Users.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password) && u.Roles.Equals(user.Roles)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["ID_User"] = obj.ID_User.ToString();
                        Session["Username"] = obj.Username.ToString();
                        Session["Roles"] = obj.Roles.ToString();

                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        return RedirectToAction("Index","TrangChu");
                    }
                    
                }

            }
            
            ModelState.AddModelError("CredentialError", "Invalid User or Password");
            return View(user);
            
        }

        public ActionResult LoggedIn() {
            //ViewBag.Username = User.Identity.Name;
            if (Session["ID_User"] != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
