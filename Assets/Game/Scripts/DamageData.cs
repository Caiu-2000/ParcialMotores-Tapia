public struct DamageData
{
    public int Damage { get; }
    public Entity FromWho { get; }

    public DamageData(int damage, Entity fromWho)
    {
        Damage = damage;
        FromWho = fromWho;
    }



}