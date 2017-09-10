using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("PackingTests")]

namespace Packing
{
    public class Unpacker
    {
        public string Decode(byte[] payload)
        {
            var result = Unpack(payload);
            return Encoding.UTF8.GetString(result);
        }

        internal byte[] Unpack(byte[] payload)
        {
            int extraBytes = payload.Length / 7;
            int expectedLength = payload.Length + extraBytes;
            payload = StretchArray(payload, expectedLength);
            int currentShift = 1;
            byte lastTail = 0;
            for(int i = 0; i < expectedLength; i++)
            {
                if((i+1) % 8 != 0 || i == 0)
                {
                    byte shift = (byte)(255 << (8 - currentShift));
                    var tail = (byte)((payload[i] & shift) >> (8 - currentShift));
                    payload[i] <<= currentShift;
                    payload[i] >>= 1;
                    payload[i] |= lastTail;
                    lastTail = tail;
                    currentShift++;
                }
                else
                {
                    payload[i] = lastTail;
                    lastTail = 0;
                    currentShift = 1;
                }
            }
            return payload;
        }

        internal byte[] StretchArray(byte[] payload, int expectedLength)
        {
            Array.Resize(ref payload, expectedLength);
            int iterations = (payload.Length + 1) / 8;
            int i = 8;
            for (int iterNum = 1; iterNum <= iterations; iterNum++)
            {
                for (int j = expectedLength - 1; j > (i*iterNum) - 1; j--)
                {
                    payload[j] = payload[j - 1];
                    payload[j - 1] = 0;
                }
            }
            return payload;
        }
    }
}
