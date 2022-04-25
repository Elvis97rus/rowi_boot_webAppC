using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp_c.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitPlace { get; set; }
        public string Diagnosis { get; set; }
        public string Recommendation { get; set; }
        public int Medicine { get; set; }
        public int Doctor { get; set; }
        public int Patient { get; set; }
    }
}