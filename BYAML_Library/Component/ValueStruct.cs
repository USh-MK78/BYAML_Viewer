using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Component
{
    public class ValueStruct
    {
        /// <summary>
        /// Position3D
        /// </summary>
        public struct Position3D
        {
            public double _X;
            public double _Y;
            public double _Z;

            public Position3D(double X, double Y, double Z)
            {
                _X = X;
                _Y = Y;
                _Z = Z;
            }
        }

        public struct Scale3D
        {
            public double _X;
            public double _Y;
            public double _Z;

            public Scale3D(double X, double Y, double Z)
            {
                _X = X;
                _Y = Y;
                _Z = Z;
            }
        }

        public struct Rotation3D
        {
            public double _X;
            public double _Y;
            public double _Z;

            public Rotation3D(double X, double Y, double Z)
            {
                _X = X;
                _Y = Y;
                _Z = Z;
            }
        }

        public struct Vector3D
        {
            public double _X;
            public double _Y;
            public double _Z;

            public Vector3D(double X, double Y, double Z)
            {
                _X = X;
                _Y = Y;
                _Z = Z;
            }
        }

    }
}
