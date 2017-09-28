using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prodavnica.Models.dto
{
    public class proizvodDto
    {
        public int idProizvod { get; set; }
        public string nazivProizvod { get; set; }
        public string opisProizvod { get; set; }
        public string slikaProizvod { get; set; }
        public double cenaProizvod { get; set; }
        public Nullable<int> popustProizvod { get; set; }
        public int statusProizvod { get; set; }
        public System.DateTime datumUnosaProizvod { get; set; }
        public Nullable<System.DateTime> datumIzmeneProizvod { get; set; }
        public int idKategorija { get; set; }
        public string nazivKategorija { get; set; }
        public virtual IQueryable<slikeDto> slike { get; set; }

    }
}