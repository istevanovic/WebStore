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
using Prodavnica.Models.dto;

namespace Prodavnica.Controllers
{
    public class korisnikController : ApiController
    {
        private prodavnicaEntities db = new prodavnicaEntities();

        [Authorize(Roles = "admin")]
        // GET: api/korisnik
        public IQueryable<korisnikDto> Getkorisnik()
        {
            var korisnici = from b in db.korisnik
                            select new korisnikDto()
                        {
                            idKorisnik = b.idKorisnik,
                            username = b.username,
                            password = b.password,
                            role=b.role
                            

                        };

            return korisnici;
            //return db.korisnik;
        }
        [Authorize(Roles = "admin")]
        [Route("api/admin/korisnik")]
        public IQueryable<korisnikDto> Getkorisnikadmin()
        {
            var korisnici = from b in db.korisnik
                            select new korisnikDto()
                            {
                                idKorisnik = b.idKorisnik,
                                username = b.username,
                                password = b.password,
                                role = b.role,
                                datumRegistracije = b.datumRegistracije,
                                datumPoslednjegLogovanja = b.datumPoslednjegLogovanja,
                                ime=b.ime,
                                prezime=b.prezime,
                                email=b.email

                            };

            return korisnici;
        }
        // GET: api/korisnik/5
        [ResponseType(typeof(korisnik))]
        public IHttpActionResult Getkorisnik(int id)
        {
            korisnik k = db.korisnik.Find(id);
            if (k == null)
            {
                return NotFound();
            }
            var korisnik =  new korisnikDto()
                            {
                                idKorisnik = k.idKorisnik,
                                username = k.username,
                                password = k.password,
                                role =k.role,
                ime = k.ime,
                prezime = k.prezime,
                email =k.email

            };

            

            return Ok(korisnik);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/korisnik/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putkorisnik(int id, korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.idKorisnik)
            {
                return BadRequest();
            }

            // db.Entry(korisnik).State = EntityState.Modified;
            var old = db.korisnik.Find(id);
            korisnik.datumPoslednjegLogovanja = old.datumPoslednjegLogovanja;
            korisnik.datumRegistracije = old.datumRegistracije;
            db.Entry(old).CurrentValues.SetValues(korisnik); // -ove dve linije ubacene

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!korisnikExists(id))
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
        // POST: api/korisnik
        [ResponseType(typeof(korisnik))]
        public IHttpActionResult Postkorisnik(korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            korisnik.datumRegistracije = DateTime.Now;
            korisnik.datumPoslednjegLogovanja = DateTime.Now;
            db.korisnik.Add(korisnik);
            db.SaveChanges();
            return Ok(new { id = korisnik.idKorisnik });
            //return CreatedAtRoute("DefaultApi", new { id = korisnik.idKorisnik }, korisnik);
        }

        [Route("api/korisnik/register")]
        // POST: api/korisnik
        [ResponseType(typeof(korisnik))]
        public IHttpActionResult Register(korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            korisnik.datumRegistracije = DateTime.Now;
            korisnik.datumPoslednjegLogovanja = DateTime.Now;
            korisnik.role = "user";
            db.korisnik.Add(korisnik);
            db.SaveChanges();

            return Ok(new
            {
                id = korisnik.idKorisnik
            });
            //return CreatedAtRoute("DefaultApi", new { id = korisnik.idKorisnik }, korisnik);
        }

     /*  
      *  NEMA BRISANJA
      *  
      *   [Authorize(Roles = "admin")]
        // DELETE: api/korisnik/5
        [ResponseType(typeof(korisnik))]
        public IHttpActionResult Deletekorisnik(int id)
        {
            korisnik korisnik = db.korisnik.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            db.korisnik.Remove(korisnik);
            db.SaveChanges();

            return Ok(korisnik);
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

        private bool korisnikExists(int id)
        {
            return db.korisnik.Count(e => e.idKorisnik == id) > 0;
        }
    }
}