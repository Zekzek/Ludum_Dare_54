using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    [SerializeField] protected Material _normalMaterial;
    [SerializeField] protected Material _damagedMaterial;
    [SerializeField] BaseComponent[] _components;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private const float FLASH_PERIOD = 0.05f;
    private float flashCooldown;

    private void Start()
    {
        gameObject.TryGetComponent(out _renderer);
        AddRigidBody();
        foreach (BaseComponent component in _components) {
            component.Init(this);
        }
    }

    private void Update()
    {
        if (flashCooldown <=0 && _renderer != null) {
            _renderer.material = _normalMaterial;
        } else {
            flashCooldown -= Time.deltaTime;
        }
    }

    public BaseComponent GetComponent(BaseComponent.ComponentType type)
    {
        foreach(BaseComponent component in _components) {
            if (component.Type == type) { return component; }
        }
        return null;
    }

    public void AddRigidBody()
    {
        gameObject.TryGetComponent(out _rigidbody);
        if (_rigidbody == null) {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.drag = 5;
            _rigidbody.angularDrag = 50;
        }
    }

    public void RemoveRigidBody()
    {
        if (_rigidbody != null) {
            Destroy(_rigidbody);
            _rigidbody = null;
        }
    }

    public void Move(Vector3 direction)
    {
        if (_rigidbody == null) {
            transform.position += direction / Time.deltaTime;
        } else {
            _rigidbody.velocity = direction;
        }
    }

    public void DamageFlash()
    {
        if (_renderer != null) {
            _renderer.material = _damagedMaterial;
            flashCooldown = FLASH_PERIOD;
        }
    }

    public void SelfDestruct()
    {
        ((ConnectComponent)GetComponent(BaseComponent.ComponentType.Connect)).DisconnectAll();
        Destroy(gameObject);
    }
}
