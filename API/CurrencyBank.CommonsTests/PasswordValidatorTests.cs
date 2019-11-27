using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyBank.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Commons.Tests
{
    [TestClass()]
    public class PasswordValidatorTests
    {
        [TestMethod()]
        public void ArePasswordsEqualsTestDifferentPasswordsFalseExpected()
        {
            var pass = PasswordValidator.ArePasswordsEquals("testAAA", "testaaa");
            var expected = false;

            Assert.AreEqual(expected, pass);
        }

        [TestMethod()]
        public void ArePasswordsEqualsTestDifferentPasswordsFalseExpected2()
        {
            var pass = PasswordValidator.ArePasswordsEquals("testAAA", "");
            var expected = false;

            Assert.AreEqual(expected, pass);
        }

        [TestMethod()]
        public void ArePasswordsEqualsTestTheSamePasswordsTrueExpected()
        {
            var pass = PasswordValidator.ArePasswordsEquals("testAAA", "testAAA");
            var expected = true;

            Assert.AreEqual(expected, pass);
        }
    }
}