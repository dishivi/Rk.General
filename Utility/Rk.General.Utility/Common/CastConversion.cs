
namespace Rk.General.Utility.Common
{
    public static class CastConversion
    {
        public static object ConvertTo(Type type, string value)
        {
            switch (type.Name)
            {
                case nameof(String):
                    return value;
                case nameof(Guid):
                    return Guid.Parse(value);
                case nameof(Int64):
                    return Convert.ToInt64(value);
                case nameof(Int32):
                    return Convert.ToInt32(value);
                case nameof(Boolean):
                    return Convert.ToBoolean(value);
                case nameof(DateTime):
                    return Convert.ToDateTime(value);
                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        public static Guid? ToGuid(this string value)
        {
            if (!Guid.TryParse(value, out Guid result)) return null;

            return result;
        }
    }
}
