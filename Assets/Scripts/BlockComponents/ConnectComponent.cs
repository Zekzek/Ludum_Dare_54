using UnityEngine;

public class ConnectComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Connect;
    
    [SerializeField] private Connection[] _connections;
    public Connection[] Connections => _connections;

    public CoreComponent Core { get; set; }

    public override void Init()
    {
        foreach (Connection connection in _connections) {
            connection.Init(this);
        }
        UpdateConnections();
    }

    public void UpdateConnections()
    {
        if (Core == null) {
            Core = (CoreComponent)GetSibling(ComponentType.Core);
        }

        if (Core != null) {
            Core.UpdateConnections();
        }
    }

    public void Connect(ConnectComponent other) {
        if (Core == null && other.Core != null) {
            other.Core.Add(this);
        } else if (Core != null && other.Core == null) {
            Core.Add(other);
        }
    }

    public void DisconnectAll()
    {
        foreach (Connection connection in _connections) {
            connection.Disconnect();
        }
    }

    public void Move(Vector3 direction)
    {
        blockBehaviour.Move(direction);
    }
}
