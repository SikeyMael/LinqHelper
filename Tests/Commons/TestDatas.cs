using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Commons
{

    internal class PrimaryData
    {
        public PrimaryData(int numberPd, string stringPd)
        {
            NumberPd = numberPd;
            StringPd = stringPd;
        }

        public int NumberPd { get; set; }
        public string StringPd { get; set; }

        public static List<PrimaryData> CreateTestList()
        {
            return new List<PrimaryData>() {
                new PrimaryData(1, "A"),
                new PrimaryData(2, "B"),
                new PrimaryData(3, "C"),
                new PrimaryData(4, "A"),
                new PrimaryData(5, "B"),
                new PrimaryData(6, "C")
            };
        }
    }

    internal class OtherData
    {
        public OtherData(int? numberOd, string stringOd)
        {
            NumberOd = numberOd;
            StringOd = stringOd;
        }

        public int? NumberOd { get; set; }
        public string StringOd { get; set; }

        public static List<OtherData> CreateTestList()
        { 
            return new List<OtherData>() {
                new OtherData(null, "C"),
                new OtherData(null, "C"),
                new OtherData(1, "C"),
                new OtherData(1, "B"),
                new OtherData(2, "B"),
                new OtherData(2, "B")
            };
        }
    }
}
