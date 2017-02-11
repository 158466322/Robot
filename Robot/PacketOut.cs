﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

//流写入 2015.8.5
namespace GameCommon
{
    public class PacketOut
    {
        private MemoryStream stream;
        private BinaryWriter write;
        public PacketOut()
        {
            stream = new MemoryStream();
            write = new BinaryWriter(stream);
           
        }
        ~PacketOut()
        {
            stream.Close();
            write = null;
        }
        public void WriteInt32(int v)
        {
            write.Write(v);
        }

        public void WriteUInt32(uint v)
        {
            write.Write(v);
        }

        public void WriteInt16(short v)
        {
            write.Write(v);
        }

        public void WriteUInt16(ushort v)
        {
            write.Write(v);
        }

        public void WriteLong(long v)
        {
            write.Write(v);
        }

        public void WriteULong(ulong v)
        {
            write.Write(v);
        }
        public void WriteBool(bool v)
        {
            write.Write(v);
        }
        public void WriteCmd(byte sysId, byte id)
        {
            write.Write(sysId);
            write.Write(id);
        }
        public void WriteString(String v)
        {
      
            byte[] data = Coding.GetUtf8Coding().GetBytes(v);
            write.Write((byte)data.Length);
            write.Write((byte)0);
            write.Write(data);
            write.Write((byte)0);
        }

        public int GetPostion()
        {
            return (int)write.BaseStream.Length;
        }

        //coverLen 补位
        public void WriteBuff(byte[] v, int coverLen = 0)
        {
            write.Write(v);
            if (coverLen > 0)
            {
                byte cover = 0;
                for (int i = v.Length; i < coverLen; i++)
                {
                    write.Write(cover);
                }
            }
        }
        public void WriteByte(byte v)
        {
            write.Write(v);
        }
        
        public byte[] Flush()
        {
            write.Flush();
            byte[] ret = stream.GetBuffer();
            byte[] v1 = new byte[2];
            v1[0] = ret[0]; v1[1] = ret[1];
            ushort nLen = BitConverter.ToUInt16(v1, 0);
            
            byte[] retdata = new byte[nLen];
            Buffer.BlockCopy(ret, 0, retdata, 0, nLen);
            return retdata;
        }

        public byte[] GetBuffer()
        {
            byte[] ret = stream.GetBuffer();
            byte[] retdata = new byte[write.BaseStream.Length];
            Buffer.BlockCopy(ret, 0, retdata, 0, (int)write.BaseStream.Length);
            return retdata;
        }

    }
}
