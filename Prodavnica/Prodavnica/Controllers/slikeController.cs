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
    public class slikeController : ApiController
    {
        private prodavnicaEntities db = new prodavnicaEntities();

        // GET: api/slike
        public IQueryable<slikeDto> Getslike()
        {
            var slike = from s in db.slike
                            select new slikeDto()
                            {
                                idSlika = s.idSlika,
                                idProizvod = s.idProizvod,
                                putanjaSlika = s.putanjaSlika,
                                
                            };

            return slike;
            //return db.slike;
        }

        // GET: api/slike/5
        [ResponseType(typeof(slike))]
        public IHttpActionResult Getslike(int id)
        {
            slike slike = db.slike.Find(id);
            if (slike == null)
            {
                return NotFound();
            }
            var slika= new slikeDto()
            {
                idSlika = slike.idSlika,
                idProizvod = slike.idProizvod,
                putanjaSlika = slike.putanjaSlika,

            };
            return Ok(slika);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/slike/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putslike(int id, slike slike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != slike.idSlika)
            {
                return BadRequest();
            }

            // db.Entry(slike).State = EntityState.Modified;
            var old = db.slike.Find(id);
            db.Entry(old).CurrentValues.SetValues(slike); // -ove dve linije ubacene


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!slikeExists(id))
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
        // POST: api/slike
        [ResponseType(typeof(slike))]
        public IHttpActionResult Postslike(slike slike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.slike.Add(slike);
            db.SaveChanges();

            return Ok(new { idSlika = slike.idSlika,
                idProizvod = slike.idProizvod,
                putanjaSlika = slike.putanjaSlika

            });
            //return CreatedAtRoute("DefaultApi", new { id = slike.idSlika }, slike);
        }

        [Authorize(Roles = "admin")]
        // DELETE: api/slike/5
        [ResponseType(typeof(slike))]
        public IHttpActionResult Deleteslike(int id)
        {
            slike slike = db.slike.Find(id);
            if (slike == null)
            {
                return NotFound();
            }

            db.slike.Remove(slike);
            db.SaveChanges();

            return Ok(slike);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool slikeExists(int id)
        {
            return db.slike.Count(e => e.idSlika == id) > 0;
        }
    }
}