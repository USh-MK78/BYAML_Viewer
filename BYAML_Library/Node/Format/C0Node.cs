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
    /// Array Node [ C0 Node ]
    /// </summary>
    public class C0Node
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

        public byte[] NodeTypeArray { get; set; }

        public List<Value> Values { get; set; }
        public class Value
        {
            public int NodeOffset { get; set; } //Value => -1, Full => Offset (From : Start of BYAML file)
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
                    NodeOffset = -1;
                    BYAMLNode.Read_BYAMLNode(br, BOM, bYAMLIdentifier.BYAMLNodeIdentifier);
                }
            }

            public Value()
            {
                NodeOffset = 0;
                BYAMLNode = new BYAMLNode();
            }

            public override string ToString()
            {
                return "ArrayValue";
            }
        }

        public void ReadC0Node(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);

            NodeType = br.ReadByte();
            NodeCount = CustomValueTypeClass.ToInt24(endianConvert.Convert(br.ReadBytes(3)), 0);

            NodeTypeArray = new byte[NodeCount];
            NodeTypeArray = br.ReadBytes(NodeCount);

            //calculate padding
            int padding = NodeCount % 4;
            if (padding >= 1 && padding <= 3)
            {
                //read padding
                br.ReadBytes(4 - padding);
            }

            //example : C1, C1, C1, ...
            foreach (var NodeType in NodeTypeArray)
            {
                var BYAMLNodeType = GetBYAMLNodeType((BYAMLNodeIdentifier)NodeType);
                Value v = new Value();
                v.ReadValue(br, BOM, BYAMLNodeType);
                Values.Add(v);
            }
        }

        public C0Node()
        {
            NodeType = 0;
            NodeCount = 0;

            NodeTypeArray = null;
            Values = new List<Value>();
        }

        public override string ToString()
        {
            return "[ C0 ] ArrayNode";
        }
    }
}
