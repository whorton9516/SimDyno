﻿namespace DataReceiver.Helpers;
public static class PacketParser
{
    private const int DASH_PACKET_LENGTH = 331; // FM8

    public static bool IsDashFormat(byte[] packet)
    {
        return packet.Length == DASH_PACKET_LENGTH;
    }

    internal static float GetSingle(byte[] bytes, int index)
    {
        ByteCheck(bytes, index, 4);
        return BitConverter.ToSingle(bytes, index);
    }

    internal static uint GetUInt16(byte[] bytes, int index)
    {
        ByteCheck(bytes, index, 2);
        return BitConverter.ToUInt16(bytes, index);
    }

    internal static uint GetUInt32(byte[] bytes, int index)
    {
        ByteCheck(bytes, index, 4);
        return BitConverter.ToUInt32(bytes, index);
    }

    internal static int GetInt32(byte[] bytes, int index)
    {
        ByteCheck(bytes, index, 4);
        return BitConverter.ToInt32(bytes, index);
    }

    internal static uint GetUInt8(byte[] bytes, int index)
    {
        ByteCheck(bytes, index, 1);
        return bytes[index];
    }

    internal static int GetInt8(byte[] bytes, int index)
    {
        ByteCheck(bytes, index, 1);
        return Convert.ToInt16((sbyte)bytes[index]);
    }

    private static void ByteCheck(byte[] bytes, int index, int byteCount)
    {
        if (index + byteCount <= bytes.Length)
        {
            return;
        }

        throw new ArgumentException("Not enough bytes in this array");
    }
}
