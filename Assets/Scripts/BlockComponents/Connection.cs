using UnityEngine;

public class Connection : MonoBehaviour
{
    public enum ConnectionState
    {
        Open,
        Connected,
        Connecting,
        Locked
    }

    [SerializeField] private Material _openMaterial;
    [SerializeField] private Material _connectedMaterial;
    [SerializeField] private Material _connectingMaterial;

    private ConnectComponent _component;
    private ConnectionState _state = ConnectionState.Open;
    private Renderer _renderer;
    
    public ConnectionState State { 
        get { return _state; } 
        set { 
            _state = value;
            if (_state == ConnectionState.Open) {
                _renderer.material = _openMaterial;
            } else if (_state == ConnectionState.Connecting) {
                _renderer.material = _connectingMaterial;
            } else if (_state == ConnectionState.Connected) {
                _renderer.material = _connectedMaterial;
            }
        }
    }
    public Connection Pair { get; private set; }
    public ConnectComponent PairedComponent => Pair != null ? Pair._component : null;

    public void Init(ConnectComponent component) { _component = component; }

    private void Start()
    {
        gameObject.TryGetComponent(out _renderer);
        _renderer.material = _openMaterial;
    }

    private void Update()
    {
        if (State == ConnectionState.Connecting && Pair) {
            Join();
        }
    }

    public bool Disconnect()
    {
        if (State == ConnectionState.Connected && Pair.State == ConnectionState.Connected) {
            Connection localPairReference = Pair;
            Pair.State = State = ConnectionState.Open;
            Pair = Pair.Pair = null;
            localPairReference._component.UpdateConnections();
            _component.UpdateConnections();
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Connection other) && State == ConnectionState.Open && other.State == ConnectionState.Open && (_component.Core != null || other._component.Core != null) ) {
            other.State = State = ConnectionState.Connecting;
            Pair = other;
            other.Pair = this;
            _component.Connect(other._component);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Equals(other.GetComponent<Connection>(), Pair)) {
            Disconnect();
        }
    }

    private void Join()
    {
        Debug.Log("joining");
        Vector3 delta = 0.25f * (Pair.transform.position - transform.position);

        if (delta.sqrMagnitude > 0.005f) {
            _component.transform.position += delta / 100;
        } else {
            _component.transform.position += delta;
            //TODO: only join if this is the added component (and not the core), then reenable so it doesn't process every frame
            State = ConnectionState.Connected;
        }
    }
}
