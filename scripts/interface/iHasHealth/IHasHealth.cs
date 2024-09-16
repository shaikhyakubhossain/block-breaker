public interface IHasHealth
{
    float health { get; }

    void takeDamage(int damage);
}
