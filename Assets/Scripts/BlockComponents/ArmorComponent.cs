using UnityEngine;

public class ArmorComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Armor;

    [SerializeField] private float _armor;
    public float Armor => _armor;
    private bool dead;

    public void Damage(float amount)
    {
        blockBehaviour.DamageFlash();
        _armor -= amount;
        if (!dead && _armor < 0) {
            dead = true;
            blockBehaviour.SelfDestruct();
        }
    }
}
