namespace GeneticAlgorithm_OrbitalDataTransformation
{

    public class AppConfig
    {
        public GeneralGeneticAlgorithmParameters GeneralGAParameters { get; set; }
        public PlanetVisualParameters PlanetVisualParameters { get; set; }
        public FitnessEvaluationParameters FitnessEvaluationParameters { get; set; }
    }

    public class GeneralGeneticAlgorithmParameters
    {
        public int PopulationSize { get; set; }
        public int Elitism { get; set; }
        public float MutationRate { get; set; }
    }

    public class PlanetVisualParameters
    {
        public int PlanetSizeNewMin { get; set; }
        public int PlanetSizeNewMax { get; set; }
        public int OrbitSizeNewMin { get; set; }
        public int OrbitSizeNewMax { get; set; }
        public float RotationDurationNewMin { get; set; }
        public float RotationDurationNewMax { get; set; }
    }

    public class FitnessEvaluationParameters
    {
        public Windowsize WindowSize { get; set; }
        public float OverlapWeight { get; set; }
        public float ProportionalSizeWeight { get; set; }
    }

    public class Windowsize
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

}
