namespace GeneticAlgorithm_OrbitalDataTransformation;

public class SystemeSolaire
{
    private const string BaseUrl = "https://api.le-systeme-solaire.net/";
    private const string OrbitalDataUrl = "/rest/bodies/";

    public static string AssembleUrl(PlanetaryData type)
    {
        return type switch
        {
            PlanetaryData.OrbitalData => $"{BaseUrl}{OrbitalDataUrl}",
            //PlanetaryData.PlanetDescription => $"{BaseUrl}{PlanetDescriptionUrl}",
            //PlanetaryData.PlanetCoordinates => $"{BaseUrl}{CoordinatesUrl}",
            _ => string.Empty,
        };
    }
}