namespace GeneticAlgorithm_OrbitalDataTransformation;

public class GeneticAlgorithm
{
    public List<DNA> Population { get; private set; }
    public int Generation { get; private set; }
    public float BestFitness { get; private set; }
    public DNA BestDNA { get; private set; }

    public int Elitism;
    public float MutationRate;

    private List<DNA> newPopulation;
    private Random random;
    private float fitnessSum;
    private Func<DNA, float> fitnessFunction;
    private List<Planet> initialPlanetData;

    public GeneticAlgorithm(int populationSize, Random random, Func<DNA, float> fitnessFunction, int elitism, List<Planet> initialPlanetData, float mutationRate = 0.01f)
    {
        Generation = 1;
        Elitism = elitism;
        MutationRate = mutationRate;
        Population = new List<DNA>(populationSize);
        newPopulation = new List<DNA>(populationSize);
        this.random = random;
        this.fitnessFunction = fitnessFunction;
        this.initialPlanetData = initialPlanetData;

        for (var i = 0; i < populationSize; i++)
        {
            Population.Add(new DNA(random, fitnessFunction));
        }
    }

    public void NewGeneration()
    {
        if (Population.Count <= 0) return;

        CalculateFitness();
        Population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness)); // Descending sort
        newPopulation.Clear();

        for (var i = 0; i < Population.Count; i++)
        {
            if (i < Elitism)
            {
                newPopulation.Add(Population[i]);
            }
            else
            {
                var parent1 = ChooseParent();
                var parent2 = ChooseParent();
                var child = parent1.Crossover(parent2);
                child.Mutate(MutationRate);
                newPopulation.Add(child);
            }
        }

        (newPopulation, Population) = (Population, newPopulation);
        Generation++;
    }

    private void CalculateFitness()
    {
        fitnessSum = 0;
        var best = Population[0];

        foreach (var dna in Population)
        {
            dna.CalculateFitness();
            if (dna.Fitness > best.Fitness)
            {
                best = dna;
            }
            fitnessSum += dna.Fitness;
        }

        BestFitness = best.Fitness;
        BestDNA = best;
    }

    private DNA ChooseParent()
    {
        var randomNumber = random.NextDouble() * fitnessSum;

        foreach (var dna in Population)
        {
            if (randomNumber < dna.Fitness)
            {
                return dna;
            }
            randomNumber -= dna.Fitness;
        }
        return null;
    }
}