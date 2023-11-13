// Here, you'd preprocess and fetch the initial data for the 9 planets.

using System.Windows.Forms;
using GeneticAlgorithm_OrbitalDataTransformation;

var initialPlanetData = FetchInitialPlanetData();

var simulator = new PlanetSystemSimulator(initialPlanetData);
simulator.RunSimulation(1000); // Running for 1000 generations, for example.


static List<Planet> FetchInitialPlanetData()
{
    
    var orbitalData = new OrbitalData();
    orbitalData.GetData(PlanetaryData.OrbitalData, Screen.PrimaryScreen.Bounds.Width,
        Screen.PrimaryScreen.Bounds.Height);


    return BigBang.Instance.Planets;
}