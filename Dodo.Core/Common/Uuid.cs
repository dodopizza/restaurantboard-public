using System;

namespace Dodo.Core.Common
{
    [Serializable]
    public class Uuid
    {
        public static readonly Uuid Empty = new Uuid(Guid.Empty);

        public static Uuid NewUUId() => new Uuid(Guid.NewGuid());

        private const String _emptyUUId = "00000000000000000000000000000000";
        private const Int32 _uuid_bytes_length = 16;
        private const Int32 _uuid_length = 32;
        private const Int32 HexLength = 2;
        private const Int32 HexBase = 16;
        private readonly String _uuid;

        public Uuid()
        {
            _uuid = _emptyUUId;
        }

        public Uuid(String uuid)
        {
            if (String.IsNullOrEmpty(uuid))
            {
                throw new ArgumentNullException(nameof(uuid));
            }

            if (uuid.Length != _uuid_length)
            {
                throw new ArgumentException("The length of the String for UUID must be exactly 32 chars.", nameof(uuid));
            }

            if (!IsGuid(uuid))
            {
                throw new ArgumentException("UUId must have the same characters like guid");
            }

            _uuid = uuid;
        }

        public Uuid(Byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length != _uuid_bytes_length)
            {
                throw new ArgumentException("The length of the Byte array for UUID must be exactly 16 bytes.", nameof(bytes));
            }

            String val =ByteArrayToString(bytes);
            if (!IsGuid(val))
            {
                throw new ArgumentException("UUId must have the same characters like guid");
            }

            _uuid = val;
        }

        private Uuid(Guid guid)
        {
            _uuid = GetOrderedUUId(guid).ToUpper();
        }

        public override Int32 GetHashCode() => _uuid.GetHashCode();

        public override String ToString() => _uuid;

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Uuid) obj);
        }

        protected Boolean Equals(Uuid other)
        {
            return String.Equals(_uuid, other._uuid);
        }

        public static Boolean operator ==(Uuid left, Uuid right)
        {
            return Equals(left, right);
        }

        public static Boolean operator !=(Uuid left, Uuid right)
        {
            return !Equals(left, right);
        }

        public Byte[] ToByteArray()
        {
            if (_uuid.Length % HexLength != 0)
            {
                throw new ArgumentException("hexString must have an even length");
            }

            Byte[] bytes = new Byte[_uuid.Length / HexLength];

            for (Int32 i = 0; i < bytes.Length; i++)
            {
                String currentHex = _uuid.Substring(i * HexLength, HexLength);
                bytes[i] = Convert.ToByte(currentHex, HexBase);
            }
            return bytes;
        }


        private String ByteArrayToString(Byte[] bytes)
        {
            String hex = BitConverter.ToString(bytes);
            return hex.Replace("-", "");
        }

        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

        private static String GetOrderedUUId(Guid guid)
        {
            String g = guid.ToString();

            return String.Concat(g.Substring(24), g.Substring(19, 4), g.Substring(14, 4), g.Substring(9, 4), g.Substring(0, 8));
        }

        public static Uuid Parse(String input)
        {
            return new Uuid(input);
        }

        [Obsolete("Use Parse(string input) or TryParse(String input, out UUId result)")]
        public static Uuid TryParse(String input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return null;
            }
            if (input.Length != _uuid_length)
            {
                return null;
            }

            return new Uuid(input);
        }

        public static Boolean TryParse(String input, out Uuid result)
        {
            result = null;

            if (String.IsNullOrEmpty(input))
            {
                return false;
            }
            if (input.Length != _uuid_length)
            {
                return false;
            }

            result = new Uuid(input);

            return true;
        }
    }}