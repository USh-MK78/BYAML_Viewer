using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Node.Format
{
    /// <summary>
    /// Float Node [ D2 Node ]
    /// </summary>
    public class D2Node
    {
        public float FloatValue { get; set; }

        public void ReadFloatValue(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);
            FloatValue = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
        }

        public D2Node(float value)
        {
            FloatValue = value;
        }

        public D2Node()
        {
            FloatValue = 0.0f;
        }

        public override string ToString()
        {
            return "[ D2 ] Float";
        }
    }
}
