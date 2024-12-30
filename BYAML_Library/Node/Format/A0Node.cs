using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Node.Format
{
    /// <summary>
    /// StringTableIndex Node [ A0 Node ]
    /// </summary>
    public class A0Node
    {
        public int StringTableIndex { get; set; }

        public void ReadStringTableIndexValue(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);
            StringTableIndex = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
        }

        public A0Node(int value)
        {
            StringTableIndex = value;
        }

        public A0Node()
        {
            StringTableIndex = 0;
        }

        public override string ToString()
        {
            return "[ A0 ] StringTable Index";
        }
    }
}
