using BYAML_Library.Component;
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
    /// Path Node (C3 Node)
    /// </summary>
    public class C3Node
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

        public List<int> NodeOffsetList { get; set; }
        public int EndOfNodeOffset { get; set; }

        public List<int> EntrySizeList => GetEntrySizeList();
        public List<int> GetEntrySizeList()
        {
            List<int> c3d = new List<int>();
            c3d.AddRange(NodeOffsetList);
            c3d.Add(EndOfNodeOffset);

            List<int> OutputDataList = new List<int>();
            for (int i = c3d.Count - 1; i > 0; --i)
            {
                //i >= 1
                if (i > 0)
                {
                    int size = c3d[i] - c3d[i - 1];
                    OutputDataList.Add(size);
                }
            }

            return OutputDataList;
        }

        public List<Transform> TransformDataList { get; set; }
        public class Transform
        {
            public ValueStruct.Position3D Position3D { get; set; }
            public ValueStruct.Scale3D Scale3D { get; set; }
            public ValueStruct.Rotation3D Rotation3D { get; set; }
            public float UnknownValue0 { get; set; }

            public void ReadTransformData(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);

                double PX = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                double PY = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                double PZ = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);

                double SX = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                double SY = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                double SZ = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);

                double RX = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                double RY = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                double RZ = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);

                UnknownValue0 = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);

                Position3D = new ValueStruct.Position3D(PX, PY, PZ);
                Scale3D = new ValueStruct.Scale3D(SX, SY, SZ);
                Rotation3D = new ValueStruct.Rotation3D(RX, RY, RZ);
            }

            public Transform(ValueStruct.Position3D position3D, ValueStruct.Scale3D scale3D, ValueStruct.Rotation3D rotation3D, float val)
            {
                Position3D = position3D;
                Scale3D = scale3D;
                Rotation3D = rotation3D;
                UnknownValue0 = val;
            }

            public Transform()
            {
                Position3D = new ValueStruct.Position3D(0, 0, 0);
                Scale3D = new ValueStruct.Scale3D(0, 0, 0);
                Rotation3D = new ValueStruct.Rotation3D(0, 0, 0);
                UnknownValue0 = 0;
            }

            public override string ToString()
            {
                return "TransformData";
            }
        }

        public void ReadC3Node(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);
            NodeType = br.ReadByte();
            NodeCount = CustomValueTypeClass.ToInt24(endianConvert.Convert(br.ReadBytes(3)), 0);

            if (NodeCount > 0)
            {
                for (int i = 0; i < NodeCount; i++) //NodeCount - 1
                {
                    NodeOffsetList.Add(BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0));
                }

                //Read EndOfOffset
                EndOfNodeOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

                long NodeBeginOffset = br.BaseStream.Position;

                //long CurrentPos = br.BaseStream.Position;
                for (int i = 0; i < NodeOffsetList.Count; i++)
                {
                    long CurrentPos = br.BaseStream.Position;

                    br.BaseStream.Position = NodeBeginOffset; //Count

                    br.BaseStream.Seek(EntrySizeList[i], SeekOrigin.Current);

                    CustomValueTypeClass.Int24 c = GetEntrySizeList()[i] / 28; //28 => PositionXYZ + ScaleXYZ + RotationXYZ + UnknownValue
                    for (int j = 0; j < c; j++)
                    {
                        Transform transform = new Transform();
                        transform.ReadTransformData(br, BOM);
                        TransformDataList.Add(transform);
                    }

                    br.BaseStream.Position = CurrentPos;
                }
            }
        }

        public C3Node()
        {
            NodeOffsetList = new List<int>();
            EndOfNodeOffset = 0;
            TransformDataList = new List<Transform>();
        }
    }
}
