using Mael.LinqHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Commons;

namespace Tests.LinqHelper
{
    [TestClass]
    public class LeftJoinExtesionTests
    {
        [TestMethod]
        public void LeftJoinTests()
        {
            List<PrimaryData> primaryDatas = PrimaryData.CreateTestList();
            List<OtherData> otherDatas = OtherData.CreateTestList();

            var result1 = primaryDatas.AsQueryable().LeftJoin(otherDatas,
                                                              l => l.NumberPd,
                                                              i => i.NumberOd,
                                                              (l, i) => new { Pdn = l.NumberPd, Pds = l.StringPd, Odn = i?.NumberOd, Ods = i?.StringOd })
                                                    .ToList();


            TestAsserts.NumberLeftJoin(result1);

            var result2 = primaryDatas.AsQueryable().LeftJoin(otherDatas,
                                                  l => new { K1 = (int?)l.NumberPd, K2 = l.StringPd },
                                                  i => new { K1 = i.NumberOd, K2 = i.StringOd },
                                                  (l, i) => new { Pdn = l.NumberPd, Pds = l.StringPd, Odn = i?.NumberOd, Ods = i?.StringOd })
                                        .ToList();

            TestAsserts.NumberStringLeftJoin(result2);
        }
    }
}
