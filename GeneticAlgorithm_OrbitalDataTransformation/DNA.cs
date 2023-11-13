namespace GeneticAlgorithm_OrbitalDataTransformation
{
    public class DNA
    {
        public enum ScalingType
        {
            Linear,
            Logarithmic,
            Exponential,
            MinMax,
            Combination
        }

        public List<ScalingFactors> PlanetSizeFactors { get; private set; }
        public List<ScalingFactors> OrbitSizeFactors { get; private set; }
        public float DurationFactor { get; private set; }
        public ScalingType SizeScaling { get; private set; }
        public ScalingType OrbitScaling { get; private set; }

        public float Fitness { get; private set; }

        private Random random;
        private Func<DNA, float> fitnessFunction;

        public DNA(Random random, Func<DNA, float> fitnessFunction, bool shouldInitScalingFactors = true)
        {
            this.random = random;
            this.fitnessFunction = fitnessFunction;

            PlanetSizeFactors = new List<ScalingFactors>();
            OrbitSizeFactors = new List<ScalingFactors>();

            InitializeFactors();
        }

        private void InitializeFactors()
        {
            PlanetSizeFactors = new List<ScalingFactors>(9);
            OrbitSizeFactors = new List<ScalingFactors>(9);


            for (var i = 0; i < 9; i++)
            {
                PlanetSizeFactors[i] = new ScalingFactors();
                OrbitSizeFactors[i] = new ScalingFactors();
            }

            DurationFactor = (float)random.NextDouble();

            SizeScaling = (ScalingType)random.Next(0, Enum.GetValues(typeof(ScalingType)).Length);
            OrbitScaling = (ScalingType)random.Next(0, Enum.GetValues(typeof(ScalingType)).Length);
        }

        public float CalculateFitness()
        {
            Fitness = fitnessFunction(this);
            return Fitness;
        }

        public DNA Crossover(DNA otherParent)
        {
            var child = new DNA(random, fitnessFunction);

            for (var i = 0; i < PlanetSizeFactors.Count; i++)
            {
                child.PlanetSizeFactors[i].LinearFactor = random.NextDouble() < 0.5 
                    ? PlanetSizeFactors[i].LinearFactor 
                    : otherParent.PlanetSizeFactors[i].LinearFactor;
                child.PlanetSizeFactors[i].LogMultiplier = random.NextDouble() < 0.5 
                    ? PlanetSizeFactors[i].LogMultiplier 
                    : otherParent.PlanetSizeFactors[i].LogMultiplier;
                child.PlanetSizeFactors[i].ExponentialBase = random.NextDouble() < 0.5 
                    ? PlanetSizeFactors[i].ExponentialBase 
                    : otherParent.PlanetSizeFactors[i].ExponentialBase;
                child.PlanetSizeFactors[i].MinMaxMinValue = random.NextDouble() < 0.5 
                    ? PlanetSizeFactors[i].MinMaxMinValue 
                    : otherParent.PlanetSizeFactors[i].MinMaxMinValue;
                child.PlanetSizeFactors[i].MinMaxMaxValue = random.NextDouble() < 0.5 
                    ? PlanetSizeFactors[i].MinMaxMaxValue 
                    : otherParent.PlanetSizeFactors[i].MinMaxMaxValue;

                child.OrbitSizeFactors[i].LinearFactor = random.NextDouble() < 0.5 
                    ? OrbitSizeFactors[i].LinearFactor 
                    : otherParent.OrbitSizeFactors[i].LinearFactor;
                child.OrbitSizeFactors[i].LogMultiplier = random.NextDouble() < 0.5 
                    ? OrbitSizeFactors[i].LogMultiplier 
                    : otherParent.OrbitSizeFactors[i].LogMultiplier;
                child.OrbitSizeFactors[i].ExponentialBase = random.NextDouble() < 0.5 
                    ? OrbitSizeFactors[i].ExponentialBase 
                    : otherParent.OrbitSizeFactors[i].ExponentialBase;
                child.OrbitSizeFactors[i].MinMaxMinValue = random.NextDouble() < 0.5 
                    ? OrbitSizeFactors[i].MinMaxMinValue 
                    : otherParent.OrbitSizeFactors[i].MinMaxMinValue;
                child.OrbitSizeFactors[i].MinMaxMaxValue = random.NextDouble() < 0.5 
                    ? OrbitSizeFactors[i].MinMaxMaxValue 
                    : otherParent.OrbitSizeFactors[i].MinMaxMaxValue;
            }

            child.DurationFactor = random.NextDouble() < 0.5 
                ? DurationFactor 
                : otherParent.DurationFactor;
            child.SizeScaling = random.NextDouble() < 0.5 
                ? SizeScaling 
                : otherParent.SizeScaling;
            child.OrbitScaling = random.NextDouble() < 0.5 
                ? OrbitScaling 
                : otherParent.OrbitScaling;

            return child;
        }


        public void Mutate(float mutationRate)
        {
            for (var i = 0; i < PlanetSizeFactors.Count; i++)
            {
                if (!(random.NextDouble() < mutationRate)) continue;
                PlanetSizeFactors[i].LinearFactor += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                PlanetSizeFactors[i].LogMultiplier += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                PlanetSizeFactors[i].ExponentialBase += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                PlanetSizeFactors[i].MinMaxMinValue += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                PlanetSizeFactors[i].MinMaxMaxValue += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));

                OrbitSizeFactors[i].LinearFactor += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                OrbitSizeFactors[i].LogMultiplier += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                OrbitSizeFactors[i].ExponentialBase += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                OrbitSizeFactors[i].MinMaxMinValue += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
                OrbitSizeFactors[i].MinMaxMaxValue += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1));
            }

            if (random.NextDouble() < mutationRate)
            {
                DurationFactor += (float)(random.NextDouble() * 0.1f * ((random.Next(2) * 2) - 1)); // Adjusting duration by a small factor
            }

            if (random.NextDouble() < mutationRate)
            {
                SizeScaling = (ScalingType)random.Next(0, Enum.GetValues(typeof(ScalingType)).Length);
            }

            if (random.NextDouble() < mutationRate)
            {
                OrbitScaling = (ScalingType)random.Next(0, Enum.GetValues(typeof(ScalingType)).Length);
            }
        }

    }

    public class ScalingFactors
    {
        public double LinearFactor { get; set; }
        public double LogMultiplier { get; set; }
        public double ExponentialBase { get; set; }
        public double MinMaxMinValue { get; set; }
        public double MinMaxMaxValue { get; set; }
    }

}
