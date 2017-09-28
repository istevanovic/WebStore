using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Prodavnica.Models;

namespace Prodavnica.Controllers
{
    public class narudzbenicaProizvodController : ApiController
    {
        private prodavnicaEntities db = new prodavnicaEntities();

        [Authorize(Roles = "admin,user")]
        // GET: api/narudzbenicaProizvod
        public IQueryable<narudzbenicaProizvod> GetnarudzbenicaProizvod()
        {
            return db.narudzbenicaProizvod;
        }
        [Authorize(Roles = "admin,user")]
        // GET: api/narudzbenicaProizvod/5
        [ResponseType(typeof(narudzbenicaProizvod))]
        public IHttpActionResult GetnarudzbenicaProizvod(int id)
        {
            narudzbenicaProizvod narudzbenicaProizvod = db.narudzbenicaProizvod.Find(id);
            if (narudzbenicaProizvod == null)
            {
                return NotFound();
            }

            return Ok(narudzbenicaProizvod);
        }
        /*
         * NEMA MENJANJA
         * 
        // PUT: api/narudzbenicaProizvod/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutnarudzbenicaProizvod(int id, narudzbenicaProizvod narudzbenicaProizvod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != narudzbenicaProizvod.idNarProizvod)
            {
                return BadRequest();
            }

            db.Entry(narudzbenicaProizvod).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!narudzbenicaProizvodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        */
        // POST: api/narudzbenicaProizvod
        [ResponseType(typeof(narudzbenicaProizvod))]
        public IHttpActionResult PostnarudzbenicaProizvod(narudzbenicaProizvod narudzbenicaProizvod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.narudzbenicaProizvod.Add(narudzbenicaProizvod);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = narudzbenicaProizvod.idNarProizvod }, narudzbenicaProizvod);
        }
        /*
         * 
         * NEMA BRISANJA
         * 
        [Authorize(Roles = "admin")]
        // DELETE: api/narudzbenicaProizvod/5
        [ResponseType(typeof(narudzbenicaProizvod))]
        public IHttpActionResult DeletenarudzbenicaProizvod(int id)
        {
            narudzbenicaProizvod narudzbenicaProizvod = db.narudzbenicaProizvod.Find(id);
            if (narudzbenicaProizvod == null)
            {
                return NotFound();
            }

            db.narudzbenicaProizvod.Remove(narudzbenicaProizvod);
            db.SaveChanges();

            return Ok(narudzbenicaProizvod);
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool narudzbenicaProizvodExists(int id)
        {
            return db.narudzbenicaProizvod.Count(e => e.idNarProizvod == id) > 0;
        }
    }
}