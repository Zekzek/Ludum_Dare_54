using System.Collections.Generic;
using UnityEngine;

public class ComponentDestroyer : MonoBehaviour
{
    [SerializeField] private float damagePerSecond;

    private List<ArmorComponent> _armorComponents = new List<ArmorComponent>();

    private const float DAMAGE_PERIOD = 0.1f;
    private float _damageCooldown;

    private void Update()
    {
        _damageCooldown -= Time.deltaTime;
        while(_damageCooldown <= 0) {
            _damageCooldown += DAMAGE_PERIOD;
            float damage = damagePerSecond * DAMAGE_PERIOD;
            foreach (ArmorComponent armor in _armorComponents) {
                armor.Damage(damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out ArmorComponent armor)) {
            _armorComponents.Add(armor);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out ArmorComponent armor)) {
            _armorComponents.Remove(armor);
        }
    }
}
