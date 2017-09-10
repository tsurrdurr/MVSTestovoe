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
            payload = StretchArray(payload);
            throw new NotImplementedException();
        }

        internal byte[] StretchArray(byte[] payload)
        {
            int extra = payload.Length / 7;
            int expectedLength = payload.Length + extra;
            Array.Resize(ref payload, expectedLength);
            for(int i = 7; i < expectedLength; i+= 7)
            {
                for(int j = expectedLength - 1; j > i; j--)
                {
                    payload[j] = payload[j - 1];
                    payload[j - 1] = 0;
                }
            }
            return payload;
        }
    }
}
