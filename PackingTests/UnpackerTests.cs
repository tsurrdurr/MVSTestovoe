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
    class UnpackerTests
    {

        [Test]
        public void DecodingTest()
        {
            var array = new byte[] { 240, 120, 30 };
            var expected = new byte[] { 112, 113, 121 };
            var result = new Unpacker().Unpack(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void DecodingTestLong()
        {
            var array = new byte[] { 240, 120, 254, 255, 255, 255, 255, 120 };
            var expected = new byte[] { 112, 113, 121, 127, 127, 127, 127, 127, 120 };
            var result = new Unpacker().Unpack(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void StretchArrayTest()
        {
            var array = new byte[] { 240, 120, 254, 255, 255, 255, 255, 120 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255, 0, 120 };
            var result = new Unpacker().StretchArray(array, array.Length + 1);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void StretchArrayTestLonger()
        {
            var array = new byte[] { 240, 120, 254, 255, 255, 255, 255,
                                     120, 254, 255, 255, 255, 255, 255 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255, 0,
                                        120, 254, 255, 255, 255, 255, 255, 0 };
            int extraBytes = array.Length / 7;
            var result = new Unpacker().StretchArray(array, array.Length + extraBytes);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void StretchArrayTestLongest()
        {
            var array = new byte[] { 240, 120, 254, 255, 255, 255, 255,
                                     120, 254, 255, 255, 255, 255, 255, 255 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255, 0,
                                        120, 254, 255, 255, 255, 255, 255, 0, 255 };
            int extraBytes = array.Length / 7;
            var result = new Unpacker().StretchArray(array, array.Length + extraBytes);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void DecodingTestDoubleLength()
        {
            var expected = new byte[] { 112, 113, 121, 127, 127, 127, 127, 127,
                                        112, 113, 121, 127, 127, 127, 127, 127 };
            var array = new byte[] { 240, 120, 254, 255, 255, 255, 255,
                                     240, 120, 254, 255, 255, 255, 255 };
            var result = new Unpacker().Unpack(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void DecodingTestLonger()
        {
            var array = new byte[] { 240, 120, 254, 255, 255, 255, 255 };
            var expected = new byte[] { 112, 113, 121, 127, 127, 127, 127, 127 };
            var result = new Unpacker().Unpack(array);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
