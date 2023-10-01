using UnityEngine;

public abstract class BaseComponent : MonoBehaviour
{
    public enum ComponentType
    {
        Core,
        Armor,
        Move,
        Connect,
        Weapon
    }

    public BlockBehaviour blockBehaviour { get; protected set; }

    public abstract ComponentType Type { get; }

    public void Init(BlockBehaviour blockBehaviour)
    {
        this.blockBehaviour = blockBehaviour;
        Init();
    }

    public virtual void Init() { }

    public BaseComponent GetSibling(ComponentType type)
    {
        return blockBehaviour.GetComponent(type);
    }
}
