using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Node.Format
{
    /// <summary>
    /// Boolean Node [ D0 Node ]
    /// </summary>
    public class D0Node
    {
        public bool BooleanValue { get; set; }

        public void ReadBoolianValue(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);
            BooleanValue = Convert.ToBoolean(BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0));
        }

        public D0Node(bool value)
        {
            BooleanValue = value;
        }

        public D0Node()
        {
            BooleanValue = false;
        }

        public override string ToString()
        {
            return "[ D0 ] Boolean";
        }
    }
}
