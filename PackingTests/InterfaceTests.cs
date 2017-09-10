using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packing;

namespace PackingTests
{
    [TestFixture]
    class InterfaceTests
    {
        [Test]
        public void SampleTest()
        {
            string input = "teststring";
            var packer = new Packer();
            var bytes = packer.Encode(input);
            var unpacker = new Unpacker();
            var result = unpacker.Decode(bytes);
            Assert.AreEqual(input, result);
        }

        [Test]
        public void CheekyTest()
        {
            string input = "testtest";
            var packer = new Packer();
            var bytes = packer.Encode(input);
            var newbytes = bytes.Concat(bytes).ToArray();
            var unpacker = new Unpacker();
            var result = unpacker.Decode(newbytes);
            Assert.AreEqual(input + input, result);
        }

        [Test]
        public void CheekyTestAlternative()
        {
            string input = "testtesttesttest";
            var packer = new Packer();
            var bytes = packer.Encode(input);
            var newbytes = bytes.Take(bytes.Length / 2).ToArray();
            var unpacker = new Unpacker();
            var result = unpacker.Decode(newbytes);
            Assert.AreEqual(input.Substring(8), result);
        }

        [Test]
        public void SampleTestLong()
        {
            string input = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttest";
            var packer = new Packer();
            var test = Encoding.UTF8.GetBytes(input);
            var bytes = packer.Encode(input);
            var unpacker = new Unpacker();
            var result = unpacker.Decode(bytes);
            Assert.AreEqual(input, result);
        }
    }
}
