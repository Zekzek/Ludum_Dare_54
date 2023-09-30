using System.Collections.Generic;
using System.Linq;

public class CoreComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Core;

    private List<ConnectComponent> _connectedComponents = new List<ConnectComponent>();
    
    public void Add(ConnectComponent component)
    {
        _connectedComponents.Add(component);
        component.blockBehaviour.RemoveRigidBody();
        component.transform.SetParent(transform, true);
        component.Core = this;
    }

    private void Remove(ConnectComponent component)
    {
        _connectedComponents.Remove(component);
        component.DisconnectAll();
        component.transform.SetParent(null, true);
        component.Core = null;
        component.blockBehaviour.AddRigidBody();
    }

    public void UpdateConnections()
    {
        AddSelf();

        List<ConnectComponent> connectedComponents = new List<ConnectComponent>();
        BuildConnectionPath((ConnectComponent)GetSibling(ComponentType.Connect), connectedComponents);

        List<ConnectComponent> danglingConnections = _connectedComponents.Except(connectedComponents).ToList();
        for (int i = danglingConnections.Count - 1; i >= 0; i--) {
            Remove(danglingConnections[i]);
        }
    }

    private void BuildConnectionPath(ConnectComponent component, List<ConnectComponent> checkedComponents)
    {
        checkedComponents.Add(component);

        foreach (Connection connection in component.Connections) {
            if (connection.Pair != null && !checkedComponents.Contains(connection.PairedComponent)) {
                BuildConnectionPath(connection.PairedComponent, checkedComponents);
            }
        }
    }

    private void AddSelf()
    {
        ConnectComponent connectedComponent = (ConnectComponent)GetSibling(ComponentType.Connect);
        if (!_connectedComponents.Contains(connectedComponent)) {
            _connectedComponents.Add(connectedComponent);
        }
    }
}
