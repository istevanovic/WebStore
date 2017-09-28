using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prodavnica.Models.dto
{
	public class narudzbenicaProizvodDto
	{
        public int idNarProizvod { get; set; }
        public int kolicina { get; set; }
        public int idProizvod { get; set; }
        public int idNarudzbenica { get; set; }
        public string nazivProizvod { get; set; }
        public double cenaProizvod { get; set; }
        public Nullable<int> popustProizvod { get; set; }

    }
}