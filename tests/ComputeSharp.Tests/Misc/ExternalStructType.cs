namespace ComputeSharp.Tests.Misc
{
    /// <summary>
    /// An internal custom struct used to test semantic model information coming from different files (different syntax trees).
    /// </summary>
    internal struct ExternalStructType
    {
        public int A;
        public float B;

        public static ExternalStructType New(int a, float b)
        {
            ExternalStructType value;
            value.A = a;
            value.B = b;

            return value;
        }

        public static float Sum(ExternalStructType value)
        {
            return value.A + value.B;
        }
    }
}
