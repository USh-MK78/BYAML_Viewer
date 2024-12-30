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
    /// Dictionary Node [ C1 Node ]
    /// </summary>
    public class C1Node
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

        public Dictionary<int, DictionaryData> C1DataDictionary { get; set; }
        public class DictionaryData
        {
            /// <summary>
            /// Index of NodeNameTable
            /// </summary>
            public CustomValueTypeClass.Int24 NameIndex { get; set; }

            public byte BYAMLNodeTypeValue { get; set; }
            public BYAMLNodeIdentifier BYAMLNodeType
            {
                get
                {
                    return (BYAMLNodeIdentifier)Enum.ToObject(typeof(BYAMLNodeIdentifier), BYAMLNodeTypeValue);
                }
            }

            public Value ValueData { get; set; }
            public class Value
            {
                public int NodeOffset { get; set; } //Value => 4 byte Value, Full => Offset (From : Start of BYAML file)
                public BYAMLNode BYAMLNode { get; set; }

                public void ReadValue(BinaryReader br, byte[] BOM, BYAMLIdentifier bYAMLIdentifier)
                {
                    EndianConvert endianConvert = new EndianConvert(BOM);
                    if (bYAMLIdentifier.BYAMLNodeTypeIdentifier == BYAMLNodeTypeIdentifier.Full || bYAMLIdentifier.BYAMLNodeTypeIdentifier == BYAMLNodeTypeIdentifier.Full_Table)
                    {
                        NodeOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                        if (NodeOffset != 0)
                        {
                            long CurrentPos = br.BaseStream.Position;

                            //Move StartPos
                            br.BaseStream.Seek(0, SeekOrigin.Begin);

                            //Move Node
                            br.BaseStream.Seek(NodeOffset, SeekOrigin.Current);

                            BYAMLNode.Read_BYAMLNode(br, BOM);

                            br.BaseStream.Position = CurrentPos;
                        }
                    }
                    else if (bYAMLIdentifier.BYAMLNodeTypeIdentifier == BYAMLNodeTypeIdentifier.Value || bYAMLIdentifier.BYAMLNodeTypeIdentifier == BYAMLNodeTypeIdentifier.IndexValue)
                    {
                        //TODO : Read Value
                        NodeOffset = -1;
                        BYAMLNode.Read_BYAMLNode(br, BOM, bYAMLIdentifier.BYAMLNodeIdentifier);
                    }
                }

                public Value()
                {
                    NodeOffset = 0;
                    BYAMLNode = new BYAMLNode();
                }
            }

            /// <summary>
            /// Read DictionaryData
            /// </summary>
            /// <param name="br"></param>
            /// <param name="BOM"></param>
            public void ReadDictionaryData(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                NameIndex = CustomValueTypeClass.ToInt24(endianConvert.Convert(br.ReadBytes(3)), 0);
                BYAMLNodeTypeValue = br.ReadByte();

                var BYAMLIdentifier = GetBYAMLNodeType((BYAMLNodeIdentifier)Enum.ToObject(typeof(BYAMLNodeIdentifier), BYAMLNodeTypeValue));

                //A0, A1, C0, C1, C2, C3 D0, D1, D2
                ValueData.ReadValue(br, BOM, BYAMLIdentifier);
            }

            public DictionaryData()
            {
                NameIndex = 0;
                BYAMLNodeTypeValue = 0;
                ValueData = new Value();
            }

            public override string ToString()
            {
                return "Dictionary : " + BYAMLNodeType.ToString();
            }
        }

        /// <summary>
        /// Read C1 Node 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="BOM"></param>
        public void ReadC1Node(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);

            NodeType = br.ReadByte();
            NodeCount = CustomValueTypeClass.ToInt24(endianConvert.Convert(br.ReadBytes(3)), 0);

            for (int i = 0; i < NodeCount; i++)
            {
                DictionaryData dictionaryData = new DictionaryData();
                dictionaryData.ReadDictionaryData(br, BOM);

                C1DataDictionary.Add(i, dictionaryData);
            }
        }

        public C1Node()
        {
            NodeType = 0;
            NodeCount = 0;
            C1DataDictionary = new Dictionary<int, DictionaryData>();
        }

        public override string ToString()
        {
            return "[ C1 ] DictionaryNode";
        }
    }
}
