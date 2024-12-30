using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Node.Format
{
    /// <summary>
    /// Integer Node [ D1 Node ]
    /// </summary>
    public class D1Node
    {
        public int IntValue { get; set; }

        public void ReadIntValue(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);
            IntValue = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
        }

        public D1Node(int value)
        {
            IntValue = value;
        }

        public D1Node()
        {
            IntValue = 0;
        }

        public override string ToString()
        {
            return "[ D1 ] Integer";
        }
    }
}
