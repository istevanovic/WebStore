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
using System.Net.Mail;
using Prodavnica.Models.dto;

namespace Prodavnica.Controllers
{
    public class narudzbenicaController : ApiController
    {
        private prodavnicaEntities db = new prodavnicaEntities();

        [Authorize(Roles = "admin,user")]
        // GET: api/narudzbenica
        public IQueryable<narudzbenicaDto> Getnarudzbenica()
        {

            var narudzbenice = from n in db.narudzbenica
                               select new narudzbenicaDto()
                               {
                                   idNarudzbenica = n.idNarudzbenica,
                                   datumFormiranjaNar = n.datumFormiranjaNar,
                                   datumIzmeneNar = n.datumIzmeneNar,
                                   placanjeIme = n.placanjeIme,
                                   placanjePrezime = n.placanjePrezime,
                                   placanjeAdresa = n.placanjeAdresa,
                                   placanjeGrad = n.placanjeGrad,
                                   placanjeDrzava = n.placanjeDrzava,
                                   placanjeZIP = n.placanjeZIP,
                                   postarina = n.postarina,
                                   idKorisnik = n.idKorisnik,
                                   username = n.korisnik.username,
                                   narudzbenicaProizvod = db.narudzbenicaProizvod.Where(x => x.idNarudzbenica == n.idNarudzbenica)
                                   .Select(s => new narudzbenicaProizvodDto()
                                   {
                                       idProizvod = s.idProizvod,
                                       idNarProizvod = s.idNarProizvod,
                                       kolicina = s.kolicina,
                                       nazivProizvod = s.proizvod.nazivProizvod,
                                       cenaProizvod = s.proizvod.cenaProizvod,
                                       popustProizvod = s.proizvod.popustProizvod
                                   })
                               };

            return narudzbenice;
            //  return db.narudzbenica;
        }

        [Authorize(Roles = "admin,user")]
        // GET: api/narudzbenica/5
        [ResponseType(typeof(narudzbenica))]
        public IHttpActionResult Getnarudzbenica(int id)
        {
            narudzbenica n = db.narudzbenica.Find(id);
            if (n == null)
            {
                return NotFound();
            }
            var narudzbenica=  new narudzbenicaDto()
            {
                idNarudzbenica = n.idNarudzbenica,
                datumFormiranjaNar = n.datumFormiranjaNar,
                datumIzmeneNar = n.datumIzmeneNar,
                placanjeIme = n.placanjeIme,
                placanjePrezime = n.placanjePrezime,
                placanjeAdresa = n.placanjeAdresa,
                placanjeGrad = n.placanjeGrad,
                placanjeDrzava = n.placanjeDrzava,
                placanjeZIP = n.placanjeZIP,
                postarina = n.postarina,
                idKorisnik = n.idKorisnik,
                username = n.korisnik.username,
                narudzbenicaProizvod = db.narudzbenicaProizvod.Where(x => x.idNarProizvod == n.idNarudzbenica)
                                    .Select(s => new narudzbenicaProizvodDto()
                                    {
                                        idProizvod = s.idProizvod,
                                        idNarProizvod = s.idNarProizvod,
                                        kolicina = s.kolicina,
                                        nazivProizvod=s.proizvod.nazivProizvod,
                                        cenaProizvod=s.proizvod.cenaProizvod,
                                        popustProizvod=s.proizvod.popustProizvod
                                    })
            };

            return Ok(narudzbenica);
        }

        [Authorize(Roles = "admin,user")]
        // PUT: api/narudzbenica/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnarudzbenica(int id, narudzbenica narudzbenica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != narudzbenica.idNarudzbenica)
            {
                return BadRequest();
            }
            narudzbenica.datumIzmeneNar = DateTime.Now;
            db.Entry(narudzbenica).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!narudzbenicaExists(id))
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

        [Authorize(Roles = "admin,user")]
        // POST: api/narudzbenica
        [ResponseType(typeof(narudzbenica))]
        public IHttpActionResult Postnarudzbenica(narudzbenica narudzbenica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            narudzbenica.datumFormiranjaNar = DateTime.Now;
            narudzbenica.datumIzmeneNar = DateTime.Now;
            db.narudzbenica.Add(narudzbenica);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            korisnik k = db.korisnik.Find(narudzbenica.idKorisnik);
            //
            var fromAddress = new MailAddress("istevanovicbgd@gmail.com", "WebShop");
            var toAddress = new MailAddress(k.email, k.ime);
            const string fromPassword = "T8VubeyE";
            const string subject = "Web Shop Narudzbenica";
            const string body = "Uspesna kupovina. Platite!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            // 

            //SmtpClient client = new SmtpClient("10.0.4.30");
            //MailAddress from = new MailAddress("eticketing@srbrail.rs", "AD Železnice Srbije", System.Text.Encoding.UTF8);
            //MailAddress to = new MailAddress(k.email);
            //MailMessage message = new MailMessage(from, to);
            //message.Subject = "test " + k.email;
            //message.SubjectEncoding = System.Text.Encoding.UTF8;

            //message.Body = "Uspesna kupovina. Platite!";
            //message.BodyEncoding = System.Text.Encoding.UTF8;


            //client.Send(message);

            return Ok(new
            {
                id = narudzbenica.idNarudzbenica
            });
            //return CreatedAtRoute("DefaultApi", new { id = narudzbenica.idNarudzbenica }, narudzbenica);
        }

        /*
         * 
         * NEMA BRISANJA
         * 
        [Authorize(Roles = "admin")]
        // DELETE: api/narudzbenica/5
        [ResponseType(typeof(narudzbenica))]
        public IHttpActionResult Deletenarudzbenica(int id)
        {
            narudzbenica narudzbenica = db.narudzbenica.Find(id);
            if (narudzbenica == null)
            {
                return NotFound();
            }

            db.narudzbenica.Remove(narudzbenica);
            db.SaveChanges();

            return Ok(narudzbenica);
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

        private bool narudzbenicaExists(int id)
        {
            return db.narudzbenica.Count(e => e.idNarudzbenica == id) > 0;
        }
    }
}