using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketTracker.Models;
using Microsoft.AspNet.Identity;

namespace TicketTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index(TicketColumns column = TicketColumns.Created, 
            TicketSorting sortOrder = TicketSorting.CredAsc, string type = "")
        {       
            var tickets = from t in db.Tickets select t;
            ViewBag.Sorting = sortOrder;
            ViewBag.TicketType = string.IsNullOrEmpty(type) ? type = "Active" : type;

            // Sorts the Tickets according to the sorting option selected
            switch (column)
            {
                case TicketColumns.Id:
                    ViewBag.Sorting = sortOrder == TicketSorting.IdAsc ?
                        TicketSorting.IdDsc : TicketSorting.IdAsc;
                    tickets = ViewBag.Sorting == TicketSorting.IdDsc ?
                        tickets.OrderBy(t => t.TicketID) : tickets.OrderByDescending(t => t.TicketID);
                    break;
                case TicketColumns.Severity:
                    ViewBag.Sorting = sortOrder == TicketSorting.SevAsc ?
                        TicketSorting.SevDsc : TicketSorting.SevAsc;
                    tickets = ViewBag.Sorting == TicketSorting.SevDsc ?
                        tickets.OrderBy(t => t.Severity) : tickets.OrderByDescending(t => t.Severity);
                    break;
                case TicketColumns.Creator:
                    ViewBag.Sorting = sortOrder == TicketSorting.CrorAsc ?
                        TicketSorting.CrorDsc : TicketSorting.CrorAsc;
                    tickets = ViewBag.Sorting == TicketSorting.CrorDsc ?
                        tickets.OrderBy(t => t.Creator) : tickets.OrderByDescending(t => t.Creator);
                    break;
                case TicketColumns.Created:
                    ViewBag.Sorting = sortOrder == TicketSorting.CredAsc ?
                        TicketSorting.CredDsc : TicketSorting.CredAsc;
                    tickets = ViewBag.Sorting == TicketSorting.CredDsc ? 
                        tickets.OrderBy(t => t.Created) : tickets.OrderByDescending(t => t.Created);
                    break;
                case TicketColumns.Modifier:
                    ViewBag.Sorting = sortOrder == TicketSorting.MdorAsc ?
                        TicketSorting.MdorDsc : TicketSorting.MdorAsc;
                    tickets = ViewBag.Sorting == TicketSorting.MdorDsc ?
                        tickets.OrderBy(t => t.Modifier) : tickets.OrderByDescending(t => t.Modifier);
                    break;
                case TicketColumns.Modified:
                    ViewBag.Sorting = sortOrder == TicketSorting.MdedAsc ?
                        TicketSorting.MdedDsc : TicketSorting.MdedAsc;
                    tickets = ViewBag.Sorting == TicketSorting.MdedDsc ?
                        tickets.OrderBy(t => t.Modified) : tickets.OrderByDescending(t => t.Modified);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.Created);
                    break;
            }
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "admin, user")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public ActionResult Create([Bind(Include = "TicketID,Severity,Title,Description,Status,Creator,Created,Modifier,Modified")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Status = TicketTypes.Active;
                ticket.Creator = User.Identity.GetUserName();
                ticket.Created = DateTime.Now.Date;
                ticket.Modifier = "";
                ticket.Modified = null;

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "TicketID,Severity,Title,Description,Status,Creator,Created")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Modifier = User.Identity.GetUserName();
                ticket.Modified = DateTime.Now.Date;

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tickets/Resolve/5
        [Authorize(Roles = "admin")]
        public ActionResult Resolve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            ticket.Status = TicketTypes.Resolved;
            ticket.Modifier = User.Identity.GetUserName();
            ticket.Modified = DateTime.Now.Date;

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return View("Details", ticket);
        }

        // GET: Tickets/Resolve/5
        [Authorize(Roles = "admin")]
        public ActionResult Active(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            ticket.Status = TicketTypes.Active;
            ticket.Modifier = User.Identity.GetUserName();
            ticket.Modified = DateTime.Now.Date;

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return View("Details", ticket);
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

    /// <summary>
    /// Allows switching for ascending and descending ticket order by categories
    /// </summary>
    public enum TicketSorting
    {
        none,
        IdAsc,
        IdDsc,
        SevAsc,
        SevDsc,
        StaAsc,
        StaDsc,
        CrorAsc,
        CrorDsc,
        CredAsc,
        CredDsc,
        MdorAsc,
        MdorDsc,
        MdedAsc,
        MdedDsc
    }
}

