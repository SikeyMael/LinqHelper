using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Commons
{
    internal static class TestAsserts
    {
        internal static void NumberLeftJoin(IEnumerable<dynamic> result)
        {
            Assert.AreEqual(result.Count(), 8);
            Assert.AreEqual(result.Where(r => r.Pdn == 1).Count(), 2);
            Assert.AreEqual(result.Where(r => r.Pdn == 2).Count(), 2);
            Assert.AreEqual(result.Where(r => Equals(r.Odn, null) && Equals(r.Ods, null)).Count(), 4);
        }

        internal static void NumberStringLeftJoin(IEnumerable<dynamic> result)
        {
            Assert.AreEqual(result.Count(), 7);
            Assert.AreEqual(result.Where(r => r.Pdn == 2 && r.Pds == "B").Count(), 2);
            Assert.AreEqual(result.Where(r => Equals(r.Odn, null) && Equals(r.Ods, null)).Count(), 5);
        }
    }
}
