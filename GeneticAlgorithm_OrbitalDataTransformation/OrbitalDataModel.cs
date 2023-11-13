namespace GeneticAlgorithm_OrbitalDataTransformation;

[Serializable]

public class OrbitalDataModel
{
    public string alternativeName { get; set; }
    public double aphelion { get; set; }
    public double argPeriapsis { get; set; }
    public object aroundPlanet { get; set; }
    public double avgTemp { get; set; }
    public double axialTilt { get; set; }
    public string bodyType { get; set; }
    public double density { get; set; }
    public string dimension { get; set; }
    public string discoveredBy { get; set; }
    public string discoveryDate { get; set; }
    public double eccentricity { get; set; }
    public string englishName { get; set; }
    public double equaRadius { get; set; }
    public double escape { get; set; }
    public double flattening { get; set; }
    public double gravity { get; set; }
    public string id { get; set; }
    public double inclination { get; set; }
    public bool isPlanet { get; set; }
    public double longAscNode { get; set; }
    public double mainAnomaly { get; set; }
    public Mass mass { get; set; }
    public double meanRadius { get; set; }
    public object moons { get; set; }
    public string name { get; set; }
    public double semiMinorAxis { get; set; }
    public double perihelion { get; set; }
    public double polarRadius { get; set; }
    public double semimajorAxis { get; set; }
    public double sideralOrbit { get; set; }
    public double sideralRotation { get; set; }
    public Vol vol { get; set; }
}