namespace KarginScales.Models;

public struct DataPoint
{
    public double Temperature { get; set; }
    public double Gamma { get; set; }

    public DataPoint(double temperature, double gamma)
    {
        Temperature = temperature; 
        Gamma = gamma;
    }
}
