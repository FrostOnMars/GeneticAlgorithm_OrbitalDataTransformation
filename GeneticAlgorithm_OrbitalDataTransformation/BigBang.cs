using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GeneticAlgorithm_OrbitalDataTransformation;

public sealed class BigBang : ObservableObject
{
    //Create singleton code to ensure only one instance of the BigBang is created.
    //Everyone must share this.

    private static readonly object LockObject = new();
    private static BigBang? _instance = null;

    public List<Planet> Planets { get; } = new();
    public AppConfig AppConfig { get; }

    private BigBang()
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "launchSettings.json");
        var jsonContent = File.ReadAllText(jsonPath);
        AppConfig = JsonConvert.DeserializeObject<AppConfig>(jsonContent) ?? throw new NullReferenceException();
    }

    public static BigBang Instance
    {
        get
        {
            if (_instance != null) return _instance;
            lock (LockObject)
            {
                _instance ??= new BigBang();
            }
            return _instance;
        }
    }

}