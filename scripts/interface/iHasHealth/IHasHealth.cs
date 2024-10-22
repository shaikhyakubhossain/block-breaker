public interface IHasHealth
{
    float health { get; }

    float maxHealth { get; }
    float healthRegenerationRate { get; }

    void takeDamage(int damage);
}
