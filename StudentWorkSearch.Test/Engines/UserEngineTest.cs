using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentWorksSearch.Engines;

namespace StudentWorkSearch.Test.Engines
{
    [TestClass]
    public class UserEngineTest
    {
        [TestMethod()]
        public void Hahs_nullParametr_ArgumentNullException() { UserEngine.Hash(null); Assert.Fail(); }

        [TestMethod()]
        public void Hash_notNullParametr_return64Char()
        { //1 
            string testStr = "test";
            //2 
            string returnStr = UserEngine.Hash(testStr);
            //3 
            Assert.AreEqual(64, returnStr.Length);
        }
    }
}