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
using System.Web.Http.Cors;
using Prodavnica.Models.dto;

namespace Prodavnica.Controllers
{
   // [EnableCors("*", "*", "*")]
    public class kategorijaController : ApiController
    {
        private prodavnicaEntities db = new prodavnicaEntities();

        //[Authorize(Roles = "admin")]
        // GET: api/kategorija
        public IQueryable<kategorijaDto> Getkategorija()
        {
            var kategorija = from kat in db.kategorija
                             select new kategorijaDto()
                             {
                                 idKategorija = kat.idKategorija,
                                 nazivKategorija = kat.nazivKategorija
                             };

            return kategorija;
           // return db.kategorija;
        }

        // GET: api/kategorija/5
        [ResponseType(typeof(kategorija))]
        public IHttpActionResult Getkategorija(int id)
        {
            kategorija kategorija = db.kategorija.Find(id);
            if (kategorija == null)
            {
                return NotFound();
            }
            var kat=new kategorijaDto()
            {
                idKategorija = kategorija.idKategorija,
                nazivKategorija = kategorija.nazivKategorija
            };
            return Ok(kat);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/kategorija/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putkategorija(int id, kategorija kategorija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kategorija.idKategorija)
            {
                return BadRequest();
            }

            //db.Entry(kategorija).State = EntityState.Modified;
            var old = db.kategorija.Find(id);
            db.Entry(old).CurrentValues.SetValues(kategorija);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!kategorijaExists(id))
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

        [Authorize(Roles = "admin")]
        // POST: api/kategorija
        [ResponseType(typeof(kategorija))]
        public IHttpActionResult Postkategorija(kategorija kategorija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.kategorija.Add(kategorija);
            db.SaveChanges();

            return Ok(new
            {
                idKategorija = kategorija.idKategorija,
                nazivKategorija = kategorija.nazivKategorija
            });
            //return CreatedAtRoute("DefaultApi", new { id = kategorija.idKategorija }, kategorija);
        }

        [Authorize(Roles = "admin")]
        // DELETE: api/kategorija/5
        [ResponseType(typeof(kategorija))]
        public IHttpActionResult Deletekategorija(int id)
        {
            kategorija kategorija = db.kategorija.Find(id);
            if (kategorija == null)
            {
                return NotFound();
            }
            if (kategorija.proizvod.Count>0)
            { // brise samo ako kategorija ne sadrzi proizvode
               return Ok(new { error="Ne možete obrisati kategoriju. Ova kategorija sadrži proizvode!" });
            }

            db.kategorija.Remove(kategorija);
            db.SaveChanges();

            return Ok(kategorija);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool kategorijaExists(int id)
        {
            return db.kategorija.Count(e => e.idKategorija == id) > 0;
        }
    }
}