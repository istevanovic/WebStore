using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prodavnica.Models.dto
{
    public class narudzbenicaDto
    {
        public int idNarudzbenica { get; set; }
        public System.DateTime datumFormiranjaNar { get; set; }
        public System.DateTime datumIzmeneNar { get; set; }
        public string placanjeIme { get; set; }
        public string placanjePrezime { get; set; }
        public string placanjeAdresa { get; set; }
        public string placanjeGrad { get; set; }
        public string placanjeDrzava { get; set; }
        public string placanjeZIP { get; set; }
        public double postarina { get; set; }
        public int idKorisnik { get; set; }
        public string username { get; set; }
        public virtual IQueryable<narudzbenicaProizvodDto> narudzbenicaProizvod { get; set; }
    }
}