﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionsLibrary;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double k = Cinterval_ww_finfin_1.interval_ww_finfin_1(3);
            Assert.IsTrue(Math.Abs(k - 0.3) < Double.Epsilon, "false");
        }
    }
}


    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod2()
        {
            double m = Cinterval_ww_finfin_2.interval_ww_finfin_2(1);
            Assert.IsTrue(Math.Abs(m - 1) < Double.Epsilon, "false");
        }
    }

[TestClass]
public class UnitTest3
{
    [TestMethod]
    public void TestMethod3()
    {
        double l = Cinterval_ww_finfin_3.interval_ww_finfin_3(1, 1, 2);
        Assert.IsTrue(Math.Abs(l - 0.25) < Double.Epsilon, "false");
    }
}



[TestClass]
public class UnitTest4
{
    [TestMethod]
    public void TestMethod4()
    {
        double p = Cinterval_ww_finfin_4.interval_ww_finfin_4(0);
        Assert.IsTrue(Math.Abs(p - 1) < Double.Epsilon, "false");
    }
}