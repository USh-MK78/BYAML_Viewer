using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BYAML_Library.Node.NodeIdentifier;

namespace BYAML_Library.Node.Format
{
    /// <summary>
    /// String Table Node [ C2 Node ]
    /// </summary>
    public class C2Node
    {
        public byte NodeType { get; set; }
        public BYAMLNodeIdentifier BYAMLNodeType
        {
            get
            {
                return (BYAMLNodeIdentifier)Enum.ToObject(typeof(BYAMLNodeIdentifier), NodeType);
            }
        }

        public CustomValueTypeClass.Int24 NodeCount { get; set; }

        public List<StringData> StringDataList { get; set; }
        public class StringData
        {
            public int NodeNameOffset { get; set; }
            public char[] NodeNameCharArray { get; set; }

            public string NodeNameString
            {
                get
                {
                    return new string(NodeNameCharArray);
                }
                set
                {
                    NodeNameCharArray = value.ToCharArray();
                }
            }

            /// <summary>
            /// Read C2 StringData
            /// </summary>
            /// <param name="br">BinaryReader</param>
            /// <param name="BOM">Endian</param>
            /// <param name="Pos">C2 Node Start Position</param>
            public void ReadStringData(BinaryReader br, byte[] BOM, long Pos)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                NodeNameOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

                long CurrentPos = br.BaseStream.Position;

                br.BaseStream.Position = Pos;

                br.BaseStream.Seek(NodeNameOffset, SeekOrigin.Current);

                ReadByteLine readByteLine = new ReadByteLine(new List<byte>());
                readByteLine.ReadByte(br, 0x00);
                NodeNameCharArray = readByteLine.ConvertToCharArray();

                br.BaseStream.Position = CurrentPos;
            }

            public StringData(string str)
            {
                NodeNameCharArray = str.ToCharArray();
            }

            public StringData()
            {
                NodeNameOffset = 0;
                NodeNameCharArray = new List<char>().ToArray();
            }

            public override string ToString()
            {
                return new string(NodeNameCharArray);
            }
        }

        /// <summary>
        /// Read C2Node
        /// </summary>
        /// <param name="br"></param>
        /// <param name="BOM"></param>
        public void ReadC2Node(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);

            long Pos = br.BaseStream.Position;

            NodeType = br.ReadByte();
            NodeCount = CustomValueTypeClass.ToInt24(endianConvert.Convert(br.ReadBytes(3)), 0);

            for (int i = 0; i < NodeCount; i++)
            {
                StringData stringData = new StringData();
                stringData.ReadStringData(br, BOM, Pos);

                StringDataList.Add(stringData);
            }
        }

        public C2Node()
        {
            NodeType = 0;
            NodeCount = 0;
            StringDataList = new List<StringData>();
        }

        public override string ToString()
        {
            return "[ C2 ] StringTable";
        }
    }
}
