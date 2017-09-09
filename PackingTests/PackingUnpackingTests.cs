﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packing;

namespace PackingTests
{
    [TestFixture]
    public class PackingUnpackingTests
    {
        [Test]
        public void TailTest()
        {
            var packer = new Packer();
            byte value = 113;
            var result = packer.GetTail(value, 1);
            Assert.AreEqual(128, result);
        }

        [Test]
        public void TailTest2()
        {
            var packer = new Packer();
            byte value = 113;
            var result = packer.GetTail(value, 2);
            Assert.AreEqual(64, result);
        }

        [Test]
        public void TailTest3()
        {
            var packer = new Packer();
            byte value = 115;
            var result = packer.GetTail(value, 2);
            Assert.AreEqual(192, result);
        }

        [Test]
        public void EncodingTest()
        {
            var array =    new byte[] { 112, 113, 121 };
            var expected = new byte[] { 240, 120, 30 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EncodingTestLong()
        {
            var array =    new byte[] { 112, 113, 121, 127, 127, 127, 127, 127};
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255  };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EncodingTestLonger()
        {
            var array = new byte[] { 112, 113, 121, 127, 127, 127, 127, 127, 120 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255, 120 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void EncodingTestDoubleSize()
        {
            var array = new byte[] { 112, 113, 121, 127, 127, 127, 127, 127,
                                     112, 113, 121, 127, 127, 127, 127, 127 };
            var expected = new byte[] { 240, 120, 254, 255, 255, 255, 255,
                                        240, 120, 254, 255, 255, 255, 255 };
            var result = new Packer().PackBytes(array);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void SampleTest()
        {
            string input = "teststring";
            var packer = new Packer();
            var test = Encoding.UTF8.GetBytes(input);
            var bytes = packer.Encode(input);
            var unpacker = new Unpacker();
            var result = unpacker.Decode(bytes);
            Assert.AreEqual(input, result);
        }
    }
}