namespace GeneticAlgorithm_OrbitalDataTransformation;

public class Planet
{
    public string Name { get; set; }
    public string Description { get; set; }
    public OrbitalDataModel? OrbitalData { get; set; }
    public double SimulationVelocity { get; set; }
    #region need to move this region to the pathing region
    public double Scale { get; set; }
    public double Distance { get; set; }
    public double Diameter { get; set; }
    public double PlanetaryVelocity { get; set; }
    public double EllipseCircumference { get; set; }
    public uint ScaleFactor { get; set; }

    #endregion

    private Planet() { }

    public static Planet Terraform(string name)
    {
        return new Planet
        {
            Name = name
        };
    }

    public static Planet Terraform(OrbitalDataModel orbitalData)
    {
        return new Planet
        {
            OrbitalData = orbitalData,
            Name = orbitalData.englishName,

        };
    }
}