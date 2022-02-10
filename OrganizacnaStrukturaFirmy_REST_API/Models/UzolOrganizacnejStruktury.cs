using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizacnaStrukturaFirmy_REST_API.Models
{
    public class UzolOrganizacnejStruktury
    {
        public int id { get; set; }

        public int kod_urovne { get; set; }

        public string nazov { get; set; }

        public int veduci { get; set; }

        public int? vyssi_uzol { get; set; }
    }
}
