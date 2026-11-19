public struct HealthData
{
    public int CurrentHealt { get; }
    public int MaxHealt { get; }

    public HealthData(int currentHealt, int maxHealt)
    {
        CurrentHealt = currentHealt;
        MaxHealt = maxHealt;
    }
}
