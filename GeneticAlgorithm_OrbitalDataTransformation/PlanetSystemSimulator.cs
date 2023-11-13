namespace GeneticAlgorithm_OrbitalDataTransformation;

public class PlanetSystemSimulator
{
    public GeneticAlgorithm GA { get; private set; }
    private Random Random;

    public PlanetSystemSimulator(List<Planet> initialPlanetData)
    {
        const int populationSize = 200;
        const float mutationRate = 0.01f;
        const int elitism = 5;
        Random = new Random();

        //int populationSize, Random random, Func<DNA, float> fitnessFunction, int elitism, float mutationRate = 0.01f
        GA = new GeneticAlgorithm(populationSize, Random, FitnessFunction, elitism, initialPlanetData, mutationRate);
    }

    public void RunSimulation(int numberOfGenerations)
    {
        for (var i = 0; i < numberOfGenerations; i++)
        {
            GA.NewGeneration();

            // For now, let's just print the fitness of the best individual in the current generation.
            Console.WriteLine($"Generation {i + 1}: Best Fitness = {GA.BestFitness}");
        }

        // After the simulation ends, you can print more details or visualize results.
    }

    private float FitnessFunction(DNA dna)
    {
        float totalFitness = 0;

        // Step 1: Determine min and max sizes for all planets.
        var minDiameter = BigBang.Instance.Planets.Min(d => d.Diameter);
        var maxDiameter = BigBang.Instance.Planets.Max(d => d.Diameter);
        var minSemiMajorAxis = BigBang.Instance.Planets.Min(d => d.OrbitalData.semimajorAxis);
        var maxSemiMajorAxis = BigBang.Instance.Planets.Max(d => d.OrbitalData.semimajorAxis);

        for (var i = 0; i < BigBang.Instance.Planets.Count; i++)
        {
            var currentPlanet = BigBang.Instance.Planets[i];

            var scalingFactorForPlanet = dna.PlanetSizeFactors[i];
            var scalingFactorForOrbit = dna.OrbitSizeFactors[i];

            var planetActualDiameter = currentPlanet.Diameter;
            var planetActualSemiMajorAxis = currentPlanet.OrbitalData.semimajorAxis;

            // Apply scaling for planet size
            double scaledDiameter = dna.SizeScaling switch
            {
                DNA.ScalingType.Linear => ScalingMethods.LinearScaling(planetActualDiameter, scalingFactorForPlanet.LinearFactor),
                DNA.ScalingType.Logarithmic => ScalingMethods.LogarithmicScaling(planetActualDiameter, scalingFactorForPlanet.LogMultiplier),
                DNA.ScalingType.Exponential => ScalingMethods.ExponentialScaling(planetActualDiameter, scalingFactorForPlanet.ExponentialBase),
                DNA.ScalingType.MinMax => ScalingMethods.MinMaxScaling(planetActualDiameter, minDiameter, maxDiameter,
                    scalingFactorForPlanet.MinMaxMinValue, scalingFactorForPlanet.MinMaxMaxValue),
                DNA.ScalingType.Combination => ScalingMethods.CombinationScaling(planetActualDiameter, Random,
                    scalingFactorForPlanet.LinearFactor, scalingFactorForPlanet.LogMultiplier,
                    scalingFactorForPlanet.MinMaxMinValue, scalingFactorForPlanet.MinMaxMaxValue),
                _ => planetActualDiameter
            };

            // Apply scaling for orbit size
            double scaledSemiMajor = dna.OrbitScaling switch
            {
                DNA.ScalingType.Linear => ScalingMethods.LinearScaling(planetActualSemiMajorAxis, scalingFactorForOrbit.LinearFactor),
                DNA.ScalingType.Logarithmic => ScalingMethods.LogarithmicScaling(planetActualSemiMajorAxis, scalingFactorForOrbit.LogMultiplier),
                DNA.ScalingType.Exponential => ScalingMethods.ExponentialScaling(planetActualSemiMajorAxis, scalingFactorForOrbit.ExponentialBase),
                DNA.ScalingType.MinMax => ScalingMethods.MinMaxScaling(planetActualSemiMajorAxis, minSemiMajorAxis, maxSemiMajorAxis,
                    scalingFactorForOrbit.MinMaxMinValue, scalingFactorForOrbit.MinMaxMaxValue),
                DNA.ScalingType.Combination => ScalingMethods.CombinationScaling(planetActualSemiMajorAxis, Random,
                    scalingFactorForOrbit.LinearFactor, scalingFactorForOrbit.LogMultiplier,
                    scalingFactorForOrbit.MinMaxMinValue, scalingFactorForOrbit.MinMaxMaxValue),
                _ => planetActualSemiMajorAxis
            };

            // Now calculate fitness for this planet based on these scaled values
            totalFitness += dna.CalculateFitness();
        }

        return totalFitness;
    }
}