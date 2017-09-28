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
using System.Web;
using System.IO;
using Prodavnica.Models.dto;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Prodavnica.Controllers
{
    public class proizvodController : ApiController
    {
        private prodavnicaEntities db = new prodavnicaEntities();

        // GET: api/proizvod
        public IQueryable<proizvodDto> Getproizvod()
        { // daj sve proizvode

            var proizvodi = from p in db.proizvod
                            select new proizvodDto()
                            {
                                idProizvod = p.idProizvod,
                                nazivProizvod = p.nazivProizvod,
                                opisProizvod = p.opisProizvod,
                                slikaProizvod = p.slikaProizvod,
                                cenaProizvod = p.cenaProizvod,
                                popustProizvod = p.popustProizvod,
                                statusProizvod = p.statusProizvod,
                                datumUnosaProizvod = p.datumUnosaProizvod,
                                datumIzmeneProizvod = p.datumIzmeneProizvod,
                                idKategorija = p.idKategorija,
                                nazivKategorija = p.kategorija.nazivKategorija,
                                slike = db.slike.Where(x => x.idProizvod == p.idProizvod)
                        .Select(s => new slikeDto()
                        {
                            idSlika = s.idSlika,
                            idProizvod = s.idProizvod,
                            putanjaSlika = s.putanjaSlika,

                        })
                            };

            return proizvodi;
            //  return db.proizvod;
        }

        // GET: api/proizvod/5
        // [ResponseType(typeof(proizvod))]
        [Route("api/kat/proizvod")]
        public IHttpActionResult GetKategorijaProizvod(int katId)
        { // daj jedan proizvod po id
            //proizvod p = db.proizvod.Find(id);
            var proizvodi = from p in db.proizvod
                            where p.kategorija.idKategorija == katId
                            select new proizvodDto()
                            {
                                idProizvod = p.idProizvod,
                                nazivProizvod = p.nazivProizvod,
                                opisProizvod = p.opisProizvod,
                                slikaProizvod = p.slikaProizvod,
                                cenaProizvod = p.cenaProizvod,
                                popustProizvod = p.popustProizvod,
                                statusProizvod = p.statusProizvod,
                                datumUnosaProizvod = p.datumUnosaProizvod,
                                datumIzmeneProizvod = p.datumIzmeneProizvod,
                                idKategorija = p.idKategorija,
                                nazivKategorija = p.kategorija.nazivKategorija,
                                slike = db.slike.Where(x => x.idProizvod == p.idProizvod)
                        .Select(s => new slikeDto()
                        {
                            idSlika = s.idSlika,
                            idProizvod = s.idProizvod,
                            putanjaSlika = s.putanjaSlika,

                        })
                            };
            if (proizvodi == null)
            {
                return NotFound();
            }
            return Ok(proizvodi);
        }

        // GET: api/proizvod/5
        // [ResponseType(typeof(proizvod))]
        [Route("api/pretraga/proizvod")]
        public IHttpActionResult GetPretragaProizvod(string term)
        { // daj jedan proizvod po id
            //proizvod p = db.proizvod.Find(id);
            var proizvodi = from p in db.proizvod
                            where p.nazivProizvod.Contains(term) || p.opisProizvod.Contains(term)
                            select new proizvodDto()
                            {
                                idProizvod = p.idProizvod,
                                nazivProizvod = p.nazivProizvod,
                                opisProizvod = p.opisProizvod,
                                slikaProizvod = p.slikaProizvod,
                                cenaProizvod = p.cenaProizvod,
                                popustProizvod = p.popustProizvod,
                                statusProizvod = p.statusProizvod,
                                datumUnosaProizvod = p.datumUnosaProizvod,
                                datumIzmeneProizvod = p.datumIzmeneProizvod,
                                idKategorija = p.idKategorija,
                                nazivKategorija = p.kategorija.nazivKategorija,
                                slike = db.slike.Where(x => x.idProizvod == p.idProizvod)
                        .Select(s => new slikeDto()
                        {
                            idSlika = s.idSlika,
                            idProizvod = s.idProizvod,
                            putanjaSlika = s.putanjaSlika,

                        })
                            };
            if (proizvodi == null)
            {
                return NotFound();
            }
            return Ok(proizvodi);
        }


        // GET: api/proizvod/5
        [ResponseType(typeof(proizvod))]
        public IHttpActionResult Getproizvod(int id)
        { // daj jedan proizvod po id
            proizvod p = db.proizvod.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            var proizvod = new proizvodDto()
            {
                idProizvod = p.idProizvod,
                nazivProizvod = p.nazivProizvod,
                opisProizvod = p.opisProizvod,
                slikaProizvod = p.slikaProizvod,
                cenaProizvod = p.cenaProizvod,
                popustProizvod = p.popustProizvod,
                statusProizvod = p.statusProizvod,
                datumUnosaProizvod = p.datumUnosaProizvod,
                datumIzmeneProizvod = p.datumIzmeneProizvod,
                idKategorija = p.idKategorija,
                nazivKategorija = p.kategorija.nazivKategorija,
                slike = db.slike.Where(x => x.idProizvod == p.idProizvod)
                        .Select(s => new slikeDto()
                        {
                            idSlika = s.idSlika,
                            idProizvod = s.idProizvod,
                            putanjaSlika = s.putanjaSlika,

                        })
            };
            return Ok(proizvod);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/proizvod/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproizvod(int id, proizvod proizvod)
        { // menjanje proizvoda, modifikovno u odnosu na ono sto EF scaffolding daje
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proizvod.idProizvod)
            {
                return BadRequest();
            }

            //db.Entry(proizvod).State = EntityState.Modified; - ovo iybaceno
            var old = db.proizvod.Find(id);
            proizvod.datumIzmeneProizvod = DateTime.Now;
            db.Entry(old).CurrentValues.SetValues(proizvod); // -ove dve linije ubacene

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!proizvodExists(id))
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
        // POST: api/proizvod
        [Route("api/upload")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Postslike(int id)
        { // upload slika i upis u bazi po proizvodu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var request = HttpContext.Current.Request;
            var fileName = string.Empty;

            try
            {
                for (var i = 0; i < request.Files.Count; i++)
                {
                    var postedFile = request.Files[i];
                    fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/slike/" + fileName));


                    var slika = new slike
                    {
                        idProizvod = id,
                        putanjaSlika = fileName

                    };
                    db.slike.Add(slika);
                }


            }
            catch (WebException ex)
            {

            }

            /* priprema male slike od prve slike koja se upload za proizvod */
            string originalFilename = request.Files[0].FileName;
            Image imgOriginal = Image.FromFile(HttpContext.Current.Server.MapPath("~/slike/" + request.Files[0].FileName));
            Image imgActual = ScaleBySize(imgOriginal, 400);
            imgActual.Save(HttpContext.Current.Server.MapPath("~/slike/" + "small_" + request.Files[0].FileName));
            imgActual.Dispose();


            var old = db.proizvod.Find(id);
            proizvod p = old;
            p.slikaProizvod = "small_" + request.Files[0].FileName;
            db.Entry(old).CurrentValues.SetValues(p);

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
            //return CreatedAtRoute("DefaultApi", new { id = proizvod.idProizvod }, proizvod);
        }


        [Authorize(Roles = "admin")]
        // POST: api/proizvod
        [ResponseType(typeof(void))]
        public IHttpActionResult Postproizvod(proizvod proizvod)
        { // unos novog proizvoda
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            proizvod.datumUnosaProizvod = DateTime.Now;
            proizvod.datumIzmeneProizvod = DateTime.Now;
            db.proizvod.Add(proizvod);


            db.SaveChanges();
           
            return Ok(new
            {
                id = proizvod.idProizvod
            });
            //CreatedAtRoute("DefaultApi", new { id = proizvod.idProizvod });
        }



        [Authorize(Roles = "admin")]
        // DELETE: api/proizvod/5
        [ResponseType(typeof(proizvod))]
        public IHttpActionResult Deleteproizvod(int id)
        { // brisanje proizvoda
            proizvod p = db.proizvod.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            if (p.narudzbenicaProizvod.Count != 0)
            {
                return Ok(new { error="Ima narudzbenica sa ovim proizvodom. Stavite proizvod kao neaktivan!"});
            }
            
                foreach (var slika in p.slike.ToList()) { 
                db.slike.Remove(slika);
            }
            db.proizvod.Remove(p);
            db.SaveChanges();

            return Ok(p);
        }


        /* helpers */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool proizvodExists(int id)
        {
            return db.proizvod.Count(e => e.idProizvod == id) > 0;
        }

        private static Image ScaleBySize(Image imgPhoto, int size)
        {
            int logoSize = size;

            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // Resize Image to have the height = logoSize/2 or width = logoSize.
            // Height is greater than width, set Height = logoSize and resize width accordingly
            if (sourceWidth > (2 * sourceHeight))
            {
                destWidth = logoSize;
                destHeight = (float)(sourceHeight * logoSize / sourceWidth);
            }
            else
            {
                int h = logoSize / 2;
                destHeight = h;
                destWidth = (float)(sourceWidth * h / sourceHeight);
            }
            // Width is greater than height, set Width = logoSize and resize height accordingly

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
    }
}