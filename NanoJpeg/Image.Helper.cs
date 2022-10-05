using System.Runtime.CompilerServices;
using UnityEngine;

namespace NanoJpeg
{
    public partial class Image
    {
        private static void SkipMarker(ref ImageData data)
        {
            int length = DecodeLength(ref data);
            data.Skip(length);
        }

        private static int DecodeLength(ref ImageData data)
        {
            if (data.Remaining < 2) { throw new DecodeException(ErrorCode.SyntaxError); }
            int length = Decode16(ref data);

            if (length > data.Remaining) { throw new DecodeException(ErrorCode.SyntaxError); }
            data.Skip(2);

            return length - 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector4 Multiply(Vector4 a, Vector4 b)
        {
            return new Vector4(
                a.x * b.x,
                a.y * b.y,
                a.z * b.z,
                a.w * b.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort Decode16(ref ImageData data)
        {
            return Decode16(ref data, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort Decode16(ref ImageData data, int offset)
        {
            var bytes = data.Slice(offset, 2);
            ushort a = bytes[0];
            a <<= 8;
            a |= bytes[1];
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte Clip(int x)
        {
            if ((x & (~0xFF)) != 0) { return (byte)((-x) >> 31); }
            else { return (byte)x; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte CF(int x)
        {
            x = (x + 64) >> 7;
            return Clip(x);
        }
    }
}
