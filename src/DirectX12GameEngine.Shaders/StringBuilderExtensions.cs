using System.Text;

namespace DirectX12GameEngine.Shaders
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            if (sb.Length == 0) return sb;

            int i;

            for (i = sb.Length - 1; i >= 0; i--)
            {
                if (!char.IsWhiteSpace(sb[i]))
                {
                    break;
                }
            }

            if (i < sb.Length - 1)
            {
                sb.Length = i + 1;
            }

            return sb;
        }
    }
}
