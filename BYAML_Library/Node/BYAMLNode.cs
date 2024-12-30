using BYAML_Library.Component;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BYAML_Library.Node.NodeIdentifier;

namespace BYAML_Library.Node
{
    public class BYAMLNode
    {
        public BYAMLNodeIdentifier BYAMLNodeType;

        public Format.A0Node A0NodeData { get; set; }
        public Format.A1Node A1NodeData { get; set; }
        public Format.C0Node C0NodeData { get; set; }
        public Format.C1Node C1NodeData { get; set; }
        public Format.C2Node C2NodeData { get; set; }
        public Format.C3Node C3NodeData { get; set; }
        public Format.D0Node D0NodeData { get; set; }
        public Format.D1Node D1NodeData { get; set; }
        public Format.D2Node D2NodeData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="BOM"></param>
        /// <param name="BYAMLNodeType"></param>
        public void Read_BYAMLNode(BinaryReader br, byte[] BOM, BYAMLNodeIdentifier BYAMLNodeType)
        {
            this.BYAMLNodeType = BYAMLNodeType;
            if (BYAMLNodeType == BYAMLNodeIdentifier.StringValueTable)
            {
                A0NodeData.ReadStringTableIndexValue(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.PathValueTable)
            {
                A1NodeData.ReadPathTableIndexValue(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.Boolean)
            {
                D0NodeData.ReadBoolianValue(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.Integer)
            {
                D1NodeData.ReadIntValue(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.Float)
            {
                D2NodeData.ReadFloatValue(br, BOM);
            }
        }

        public void Read_BYAMLNode(BinaryReader br, byte[] BOM)
        {
            long CurPos = br.BaseStream.Position;
            byte BYAMLNodeTypeValue = br.ReadByte();
            this.BYAMLNodeType = (BYAMLNodeIdentifier)Enum.ToObject(typeof(BYAMLNodeIdentifier), BYAMLNodeTypeValue);
            br.BaseStream.Position = CurPos;

            //if (BYAMLNodeType == BYAMLNodeIdentifier.Boolean)
            //{
            //    D0NodeData.ReadBoolianValue(br, BOM);
            //}
            //else if (BYAMLNodeType == BYAMLNodeIdentifier.Integer)
            //{
            //    D1NodeData.ReadIntValue(br, BOM);
            //}
            //else if (BYAMLNodeType == BYAMLNodeIdentifier.Float)
            //{
            //    D2NodeData.ReadFloatValue(br, BOM);
            //}
            if (BYAMLNodeType == BYAMLNodeIdentifier.ArrayNode)
            {
                C0NodeData.ReadC0Node(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.DictionaryNode)
            {
                C1NodeData.ReadC1Node(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.StringTable)
            {
                C2NodeData.ReadC2Node(br, BOM);
            }
            else if (BYAMLNodeType == BYAMLNodeIdentifier.PathTable)
            {
                long pos = br.BaseStream.Position;

                C3NodeData.ReadC3Node(br, BOM);
            }
        }

        public BYAMLNode(BYAMLNodeIdentifier bYAMLNodeIdentifier)
        {
            BYAMLNodeType = bYAMLNodeIdentifier;

            //TODO : 分岐させる
            A0NodeData = new Format.A0Node();
            A1NodeData = new Format.A1Node();
            C0NodeData = new Format.C0Node();
            C1NodeData = new Format.C1Node();
            C2NodeData = new Format.C2Node();
            C3NodeData = new Format.C3Node();
            D0NodeData = new Format.D0Node();
            D1NodeData = new Format.D1Node();
            D2NodeData = new Format.D2Node();
        }

        public BYAMLNode()
        {
            BYAMLNodeType = 0;

            //TODO : 分岐させる
            A0NodeData = new Format.A0Node();
            A1NodeData = new Format.A1Node();
            C0NodeData = new Format.C0Node();
            C1NodeData = new Format.C1Node();
            C2NodeData = new Format.C2Node();
            C3NodeData = new Format.C3Node();
            D0NodeData = new Format.D0Node();
            D1NodeData = new Format.D1Node();
            D2NodeData = new Format.D2Node();
        }

        public override string ToString()
        {
            return "BYAML Node : [Type] -> " + BYAMLNodeType.ToString();
        }
    }
}
