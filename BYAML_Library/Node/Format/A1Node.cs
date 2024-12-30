using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Node.Format
{
    /// <summary>
    /// PathTableIndex Node [ A1 Node ]
    /// </summary>
    public class A1Node
    {
        public int PathTableIndex { get; set; }

        public void ReadPathTableIndexValue(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);
            PathTableIndex = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
        }

        public A1Node(int value)
        {
            PathTableIndex = value;
        }

        public A1Node()
        {
            PathTableIndex = 0;
        }

        public override string ToString()
        {
            return "[ A1 ] PathTable Index";
        }
    }
}
