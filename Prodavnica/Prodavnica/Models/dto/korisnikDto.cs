using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prodavnica.Models.dto
{
    public class korisnikDto
    {
        public int idKorisnik { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string password { get; set; }
        public System.DateTime datumRegistracije { get; set; }
        public System.DateTime datumPoslednjegLogovanja { get; set; }
        public string role { get; set; }
      //  public virtual ICollection<narudzbenica> narudzbenica { get; set; }
    }
}