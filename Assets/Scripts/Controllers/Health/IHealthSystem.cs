

public interface IHealthSystem
{
    public void DamageTaker(float health, float Damage)
    {
        health = health - Damage;
    }

    public void HealthImprovements(float health);

    
    
}
