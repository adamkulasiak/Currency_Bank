using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Commons.Tests
{
    [TestClass()]
    public class EmailValidatorTests
    {
        [TestMethod()]
        public void IsEmailValidTestFalseExpected()
        {
            var email = EmailValidator.IsEmailValid("aaaa@.pl");
            var expected = false;

            Assert.AreEqual(expected, email);
        }

        [TestMethod()]
        public void IsEmailValidTestFalseExpected2()
        {
            var email = EmailValidator.IsEmailValid("aaaa@aaaaa");
            var expected = false;

            Assert.AreEqual(expected, email);
        }

        [TestMethod()]
        public void IsEmailValidTestFalseExpected3()
        {
            var email = EmailValidator.IsEmailValid("2132435345");
            var expected = false;

            Assert.AreEqual(expected, email);
        }

        [TestMethod()]
        public void IsEmailValidTestTrueExpected()
        {
            var email = EmailValidator.IsEmailValid("adam@gmail.com");
            var expected = true;

            Assert.AreEqual(expected, email);
        }

        [TestMethod()]
        public void IsEmailValidTestTrueExpected2()
        {
            var email = EmailValidator.IsEmailValid("adam@wp.pl.com");
            var expected = true;

            Assert.AreEqual(expected, email);
        }

    }
}