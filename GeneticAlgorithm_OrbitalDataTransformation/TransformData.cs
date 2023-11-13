using System.Drawing;

namespace GeneticAlgorithm_OrbitalDataTransformation;

public class TransformData : OrbitalData
{
    //this is copied from OrbitalData, and it needs to be refactored
    public delegate void ErrorHandler(string errorMessage);
    public static event ErrorHandler? ErrorOccurred;

    public Planet Planet { get; set; }

    public static void PreProcessPlanetCalculations()
    {
        foreach (var planet in BigBang.Instance.Planets)
        {
            planet.OrbitalData.semiMinorAxis = CalculateSemiMinorAxis(planet.OrbitalData.semimajorAxis, planet.OrbitalData.eccentricity);
        }
        ScalePlanetDiameters(BigBang.Instance.Planets);
    }
    public static void ToUsableSizes(double screenWidth, double screenHeight)
    {
        try
        {
            foreach (var planet in BigBang.Instance.Planets.OrderByDescending(p => p.OrbitalData.semimajorAxis))
            {
                // This is the time scale factor
                planet.ScaleFactor = 40000;
                if (planet.OrbitalData == null) continue;

                // Modify the semi major and semi minor axes of the ellipse
                planet.OrbitalData.semimajorAxis = planet.OrbitalData.semimajorAxis;
                planet.OrbitalData.semiMinorAxis = CalculateSemiMinorAxis(planet.OrbitalData.semimajorAxis, planet.OrbitalData.eccentricity);
            }

            ScaleOrbitsWithScreenDimensions(BigBang.Instance.Planets, screenWidth, screenHeight);
            ScalePlanetDiameters(BigBang.Instance.Planets);
        }
        catch (Exception ex)
        {
            // Raise the event when an error occurs.
            ErrorOccurred?.Invoke(ex.Message);
        }
    }


    public TimeSpan ComputeScaledOrbitDuration()
    {
        var planet = Planet;
        var originalOrbitTime = planet.OrbitalData.sideralOrbit; // Original orbit time in days
        double scaleFactor = planet.ScaleFactor + 3000000; // Assuming scaleFactor is between 0 and 5000
        //TODO: REMOVE ADDITION TO SCALE FACTOR. TESTING THIS ONLY.
        var scaledTime = originalOrbitTime / scaleFactor;

        var scaledTimeInSeconds = scaledTime * 24 * 60 * 60;

        return TimeSpan.FromSeconds(scaledTimeInSeconds);
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="degrees">Angle in degrees.</param>
    /// <returns>Angle in radians.</returns>
    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    /// <summary>
    /// Calculates the distance from the central body at a given true anomaly.
    /// Uses the formula: r = (a * (1 - e^2)) / (1 + e * cos(ν)) [Polar Form Equation for an Ellipse]
    /// </summary>
    /// <param name="a">Semi-major axis.</param>
    /// <param name="e">Eccentricity of the orbit.</param>
    /// <param name="nu">True anomaly (angle between the direction of periapsis and the current position).</param>
    /// <returns>Distance from the central body.</returns>
    private static double CalculateDistance(double a, double e, double nu)
    {
        return a * (1 - (e * e)) / (1 + (e * Math.Cos(nu)));
    }

    /// <summary>
    /// Calculates the position (x, y) in a 2D plane based on polar coordinates.
    /// </summary>
    /// <param name="r">Distance from central body.</param>
    /// <param name="nu">True anomaly.</param>
    /// <param name="omega">Argument of periapsis (angle between the reference direction and the periapsis).</param>
    /// <returns>Position (x, y) in a 2D plane.</returns>
    private static (double, double) CalculatePosition(double r, double nu, double omega)
    {
        var x = r * Math.Cos(nu + omega);
        var y = r * Math.Sin(nu + omega);
        return (x, y);
    }

    /// <summary>
    /// Gets the starting coordinates for a planet based on its orbital elements.
    /// </summary>
    /// <returns>The starting coordinates as a Point.</returns>
    public Point GetStartingCoordinates()
    {
        // Extracting the orbital elements from the planet's data
        var a = Planet.OrbitalData.semimajorAxis; // Semi-major axis
        var e = Planet.OrbitalData.eccentricity;  // Eccentricity of the orbit
        var nu = DegreesToRadians(Planet.OrbitalData.mainAnomaly); // True anomaly in radians
        var omega = DegreesToRadians(Planet.OrbitalData.argPeriapsis); // Argument of periapsis in radians

        // Calculate the distance from the central body at the current true anomaly
        var r = CalculateDistance(a, e, nu);

        // Convert the polar coordinates to Cartesian coordinates
        var (x, y) = CalculatePosition(r, nu, omega);

        return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
    }
}