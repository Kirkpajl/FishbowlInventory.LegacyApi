using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Api.Endian
{
    internal class EndiannessAwareBinaryWriter : BinaryWriter
    {
        #region Private Fields

        private readonly Endianness _endianness = Endianness.BigEndian;

        #endregion Private Fields

        #region Constructors

        public EndiannessAwareBinaryWriter(Stream input) : base(input) { }

        public EndiannessAwareBinaryWriter(Stream input, Encoding encoding) : base(input, encoding) { }

        public EndiannessAwareBinaryWriter(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }

        public EndiannessAwareBinaryWriter(Stream input, Endianness endianness) : base(input)
        {
            _endianness = endianness;
        }

        public EndiannessAwareBinaryWriter(Stream input, Encoding encoding, Endianness endianness) : base(input, encoding)
        {
            _endianness = endianness;
        }

        public EndiannessAwareBinaryWriter(Stream input, Encoding encoding, bool leaveOpen, Endianness endianness) : base(input, encoding, leaveOpen)
        {
            _endianness = endianness;
        }

        #endregion Constructors

        #region Synchronous Methods

        public override void Write(short value) => Write(value, _endianness);

        public void Write(short value, Endianness endianness)
        {
            var buffer = new byte[sizeof(short)];
            var span = new Span<byte>(buffer);

            if (endianness == Endianness.LittleEndian)
                BinaryPrimitives.WriteInt16LittleEndian(span, value);
            else
                BinaryPrimitives.WriteInt16BigEndian(span, value);

            Write(buffer);
        }



        public override void Write(ushort value) => Write(value, _endianness);

        public void Write(ushort value, Endianness endianness)
        {
            var buffer = new byte[sizeof(ushort)];
            var span = new Span<byte>(buffer);

            if (endianness == Endianness.LittleEndian)
                BinaryPrimitives.WriteUInt16LittleEndian(span, value);
            else
                BinaryPrimitives.WriteUInt16BigEndian(span, value);

            Write(buffer);
        }



        public override void Write(int value) => Write(value, _endianness);

        public void Write(int value, Endianness endianness)
        {
            var buffer = new byte[sizeof(int)];
            var span = new Span<byte>(buffer);

            if (endianness == Endianness.LittleEndian)
                BinaryPrimitives.WriteInt32LittleEndian(span, value);
            else
                BinaryPrimitives.WriteInt32BigEndian(span, value);

            Write(buffer);
        }



        public override void Write(uint value) => Write(value, _endianness);

        public void Write(uint value, Endianness endianness)
        {
            var buffer = new byte[sizeof(uint)];
            var span = new Span<byte>(buffer);

            if (endianness == Endianness.LittleEndian)
                BinaryPrimitives.WriteUInt32LittleEndian(span, value);
            else
                BinaryPrimitives.WriteUInt32BigEndian(span, value);

            Write(buffer);
        }



        public override void Write(long value) => Write(value, _endianness);

        public void Write(long value, Endianness endianness)
        {
            var buffer = new byte[sizeof(long)];
            var span = new Span<byte>(buffer);

            if (endianness == Endianness.LittleEndian)
                BinaryPrimitives.WriteInt64LittleEndian(span, value);
            else
                BinaryPrimitives.WriteInt64BigEndian(span, value);

            Write(buffer);
        }



        public override void Write(ulong value) => Write(value, _endianness);

        public void Write(ulong value, Endianness endianness)
        {
            var buffer = new byte[sizeof(ulong)];
            var span = new Span<byte>(buffer);

            if (endianness == Endianness.LittleEndian)
                BinaryPrimitives.WriteUInt64LittleEndian(span, value);
            else
                BinaryPrimitives.WriteUInt64BigEndian(span, value);

            Write(buffer);
        }

        #endregion Synchronous Methods
    }
}
