
public struct MathfInternal
{
    public static volatile float FloatMinNormal = 1.175494E-38f;
    public static volatile float FloatMinDenormal = float.Epsilon;
    public static bool IsFlushToZeroEnabled = (double) MathfInternal.FloatMinDenormal == 0.0;
}