namespace GeneticAlgorithm_OrbitalDataTransformation;

public static class ScalingMethods
{
    // Linear Scaling
    public static double LinearScaling(double originalValue, double factor)
    {
        return originalValue * factor;
    }

    // Logarithmic Scaling (using natural logarithm)
    public static double LogarithmicScaling(double originalValue, double multiplier = 1.0)
    {
        return multiplier * Math.Log(originalValue + 1);  // +1 to handle cases where originalValue is 0
    }

    // Exponential Scaling
    public static double ExponentialScaling(double originalValue, double baseValue = 2)
    {
        return Math.Pow(baseValue, originalValue);
    }

    // MinMax Scaling
    public static double MinMaxScaling(double originalValue, double minValue, double maxValue, double newMin, double newMax)
    {
        return ((originalValue - minValue) / (maxValue - minValue) * (newMax - newMin)) + newMin;
    }

    public static double CombinationScaling(double originalValue, Random rng, double minSize, double maxSize, double factor1, double factor2, double newMin = 0, double newMax = 1)
    {
        // Choose two unique random indices
        var indices = new[] { 0, 1, 2, 3 };
        Shuffle(indices, rng);

        var firstScaledValue = originalValue;

        // Apply first scaling
        firstScaledValue = indices[0] switch
        {
            0 => LinearScaling(originalValue, factor1),
            1 => LogarithmicScaling(originalValue, factor1),
            2 => ExponentialScaling(originalValue, factor1),
            3 => MinMaxScaling(originalValue, minSize, maxSize, newMin,
                newMax) // Here, factor1 & factor2 can be used as minValue & maxValue
            ,
            _ => firstScaledValue
        };

        // Apply second scaling
        var secondScaledValue = indices[1] switch
        {
            0 => LinearScaling(firstScaledValue, factor2),
            1 => LogarithmicScaling(firstScaledValue, factor2),
            2 => ExponentialScaling(firstScaledValue, factor2),
            3 => MinMaxScaling(firstScaledValue, factor2, factor1, newMin,
                newMax) // Switching factor1 & factor2 for variation
            ,
            _ => firstScaledValue
        };

        return secondScaledValue;
    }

    private static void Shuffle(IList<int> array, Random rng)
    {
        var n = array.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }

}