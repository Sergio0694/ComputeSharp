using System;
using System.Numerics;

namespace DirectX12GameEngine.Core
{
    public static class QuaternionExtensions
    {
        public static Vector3 ToEuler(in Quaternion rotation)
        {
            Vector3 rotationEuler;

            float xx = rotation.X * rotation.X;
            float yy = rotation.Y * rotation.Y;
            float zz = rotation.Z * rotation.Z;
            float xy = rotation.X * rotation.Y;
            float zw = rotation.Z * rotation.W;
            float zx = rotation.Z * rotation.X;
            float yw = rotation.Y * rotation.W;
            float yz = rotation.Y * rotation.Z;
            float xw = rotation.X * rotation.W;

            rotationEuler.Y = (float)Math.Asin(2.0f * (yw - zx));
            double test = Math.Cos(rotationEuler.Y);

            if (test > 1e-6f)
            {
                rotationEuler.Z = (float)Math.Atan2(2.0f * (xy + zw), 1.0f - (2.0f * (yy + zz)));
                rotationEuler.X = (float)Math.Atan2(2.0f * (yz + xw), 1.0f - (2.0f * (yy + xx)));
            }
            else
            {
                rotationEuler.Z = (float)Math.Atan2(2.0f * (zw - xy), 2.0f * (zx + yw));
                rotationEuler.X = 0.0f;
            }

            return rotationEuler;
        }

        public static Quaternion ToQuaternion(in Vector3 value)
        {
            Quaternion rotation;

            var halfAngles = value * 0.5f;

            var fSinX = (float)Math.Sin(halfAngles.X);
            var fCosX = (float)Math.Cos(halfAngles.X);
            var fSinY = (float)Math.Sin(halfAngles.Y);
            var fCosY = (float)Math.Cos(halfAngles.Y);
            var fSinZ = (float)Math.Sin(halfAngles.Z);
            var fCosZ = (float)Math.Cos(halfAngles.Z);

            var fCosXY = fCosX * fCosY;
            var fSinXY = fSinX * fSinY;

            rotation.X = fSinX * fCosY * fCosZ - fSinZ * fSinY * fCosX;
            rotation.Y = fSinY * fCosX * fCosZ + fSinZ * fSinX * fCosY;
            rotation.Z = fSinZ * fCosXY - fSinXY * fCosZ;
            rotation.W = fCosZ * fCosXY + fSinXY * fSinZ;

            return rotation;
        }
    }
}
