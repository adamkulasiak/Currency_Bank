using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyBank.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Commons.Tests
{
    [TestClass()]
    public class PeselValidatorTests
    {
        [TestMethod()]
        public void IsPeselValidTestLettersInPeselFalseExpected()
        {
            var pesel = PeselValidator.IsPeselValid("970311as865");
            var expected = false;

            Assert.AreEqual(expected, pesel);
        }

        [TestMethod()]
        public void IsPeselValidTestTooLongFalseExpected()
        {
            var pesel = PeselValidator.IsPeselValid("970311118651");
            var expected = false;

            Assert.AreEqual(expected, pesel);
        }

        [TestMethod()]
        public void IsPeselValidTestTooShortFalseExpected()
        {
            var pesel = PeselValidator.IsPeselValid("9703111186");
            var expected = false;

            Assert.AreEqual(expected, pesel);
        }

        [TestMethod()]
        public void IsPeselValidTestGoodTrueExpected()
        {
            var pesel = PeselValidator.IsPeselValid("97031501223");
            var expected = false;

            Assert.AreEqual(expected, pesel);
        }
    }
}