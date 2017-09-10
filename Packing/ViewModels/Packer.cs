using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly:InternalsVisibleTo("PackingTests")]

namespace Packing
{
    public class Packer
    {
        public byte[] Encode(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            if (bytes.Any(x => x > 127)) throw new Exception("Can't be packed into 7-bit");
            else return PackBytes(bytes);
        }

        internal byte[] PackBytes(byte[] bytes)
        {
            int shiftsCounter = 1;
            double expected = ((double)bytes.Length * 7) / 8;
            int expectedLength = (int)Math.Ceiling(expected);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i + 1 < bytes.Length)
                {
                    int tail = GetTail(bytes[i + 1], shiftsCounter);
                    bytes[i] = (byte)(bytes[i] | tail);
                    bytes[i + 1] >>= shiftsCounter;
                }
                shiftsCounter++;
                if (shiftsCounter == 8)
                {
                    shiftsCounter = 1;
                    ShiftWholeBytes(bytes, i);
                }
            }
            bytes = bytes.Take(expectedLength).ToArray();
            return bytes;
        }

        internal int GetTail(byte nextbyte, int shiftsCounter)
        {
            var shift = 7 - shiftsCounter;
            var mask = (byte)Math.Pow(2, shiftsCounter) - 1;
            return (byte)(nextbyte & mask) << shift + 1;
        }

        private void ShiftWholeBytes(byte[] bytes, int i)
        {
            for (int j = i + 1; j < bytes.Length - 1; j++)
            {
                bytes[j] = bytes[j + 1];
                bytes[j + 1] = 0;
            }
        }

    }
}
