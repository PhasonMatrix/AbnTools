using NUnit.Framework;
using AbnTools;

namespace Tests
{
    public class Tests
    {
        [TestCase("30608629947")]                 // BHP
        [TestCase("30 608 629 947")]              // BHP - standard masked format
        [TestCase("306 08 62 99 47")]             // BHP - non-standard format
        [TestCase("   3 0 6 0 8 6 2 9 9 4 7  ")]  // BHP - extra spaces

        [TestCase("51 824 753 556")]              // AUSTRALIAN TAX OFFICE
        [TestCase("98 008 624 691")]              // ASX Limited
        public void TestAbnValid(string abn)
        {
            Assert.True(AbnValidator.Validate(abn));
        }


        [TestCase("")]
        [TestCase("1")]
        [TestCase("12345678910")]
        [TestCase("abcxyz")]
        [TestCase("30608628947")]   // BHP - one digit incorrect
        public void TestAbnsInvalid(string abn)
        {
            Assert.False(AbnValidator.Validate(abn));
        }
    }
}