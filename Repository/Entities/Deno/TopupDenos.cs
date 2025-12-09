using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.Deno
{
    public class TopupDenos
    {
        public string Product { get; set; }
        public string DenoAmount { get; set; }
    }
    public class TopupDenosResponse
    {
        public string Product { get; set; }
        public string[] DenoAmounts { get; set; }
    }
    public class NeaOfficeCodeModel
    {
        public int Officecode { get; set; }
        public string OfficeName { get; set; }
    }
    public class TopupMinMax
    {
        public string Product { get; set; }
        public string MinAmount { get; set; }
        public string MaxAmount { get; set; }
    }
}
