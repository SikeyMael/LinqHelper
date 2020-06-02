using Mael.DynamicLinqHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Tests.Commons;

namespace Tests.DynamicLinqHelper
{
    [TestClass]
    public class LeftJoinExtesionTests
    {
        [TestMethod]
        public void LeftJoinTests()
        {
            List<PrimaryData> primaryDatas = PrimaryData.CreateTestList();
            List<OtherData> otherDatas = OtherData.CreateTestList();

            var result1 = primaryDatas.AsQueryable().LeftJoin(otherDatas.AsQueryable(),
                                                              "NumberPd",
                                                              "NumberOd",
                                                              new List<string>() { "NumberPd AS Pdn", "StringPd AS Pds" },
                                                              new List<string>() { "NumberOd AS Odn", "StringOd AS Ods" })
                                                    .ToDynamicList();


            TestAsserts.NumberLeftJoin(result1);

            var result2 = primaryDatas.AsQueryable().LeftJoin(otherDatas.AsQueryable(),
                                                              " new { Int?(NumberPd) AS K1, StringPd AS K2}",
                                                              " new { NumberOd AS K1, StringOd AS K2}",
                                                              new List<string>() { "NumberPd AS Pdn", "StringPd AS Pds" },
                                                              new List<string>() { "NumberOd AS Odn", "StringOd AS Ods" })
                                        .ToDynamicList();

            TestAsserts.NumberStringLeftJoin(result2);
        }
    }
}
