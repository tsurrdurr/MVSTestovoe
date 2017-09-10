using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Packing;

namespace PackingTests
{
    [TestFixture]
    class PackerTests
    {
        [Test]
        public void GetTailTest_Input01110001Shift1_Returns10000000()
        {
            byte value = 113;
            var result = new Packer().GetTail(value, 1);
            Assert.AreEqual(128, result);
        }

        [Test]
        public void GetTailTest_Input01110001Shift2_Returns01000000()
        {
            byte value = 113;
            var result = new Packer().GetTail(value, 2);
            Assert.AreEqual(64, result);
        }

        [Test]
        public void GetTailTest_Input01110011Shift2_Returns11000000()
        {
            byte value = 115;
            var result = new Packer().GetTail(value, 2);
            Assert.AreEqual(192, result);
        }

        [Test]
        public void EncodingTest_InputArrayOfLength3_ReturnsExpected()
        {
            var array = new byte[] 
                                      { 112, 113, 121 };
            var expected = new byte[] { 240, 120, 30 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EncodingTest_InputArrayOfLength8_ReturnsExpected()
        {
            var array = new byte[] 
                                      { 112, 113, 121, 127, 127, 127, 127, 127 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EncodingTest_InputArrayOfLength9_ReturnsExpected()
        {
            var array = new byte[] 
                                      { 112, 113, 121, 127, 127, 127, 127, 127, 120 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255, 120 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EncodingTest_InputArrayOfLength16_ReturnsExpected()
        {
            var array = new byte[] { 112, 113, 121, 127, 127, 127, 127, 127,
                                     112, 113, 121, 127, 127, 127, 127, 127 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255,
                                        240, 120, 254, 255, 255, 255, 255 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
