using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Weapon;

    [SerializeField] private float _damagePeriod;
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _range;
    [SerializeField] private int _maximumNumberTargets;

    private float _damageCooldown;

    private void Update()
    {
        _damageCooldown -= Time.deltaTime;
        while (_damageCooldown <= 0) {
            _damageCooldown += _damagePeriod;
            DoDamage();
        }
    }

    private void DoDamage()
    {
        _damageCooldown += _damagePeriod;
        float damage = _damageAmount * _damagePeriod;
        int remainingTargets = _maximumNumberTargets;

        if (gameObject.tag == "Player") {
            foreach (ArmorComponent armor in FindTargets("Enemy")) {
                armor.Damage(damage);
                if (--remainingTargets > 0) { break; }
            }
        } else if (gameObject.tag == "Enemy") {
            foreach (ArmorComponent armor in FindTargets("Player")) {
                armor.Damage(damage);
                if (--remainingTargets > 0) { break; }
            }
        }
    }

    private List<ArmorComponent> FindTargets(string tag)
    {
        List<ArmorComponent> targets = new List<ArmorComponent>();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _range, Vector3.right);
        foreach(RaycastHit hit in hits) {
            if (hit.collider.tag == tag && hit.collider.gameObject.TryGetComponent(out ArmorComponent armor)) {
                targets.Add(armor);
            }
        }

        return targets;
    }
}
