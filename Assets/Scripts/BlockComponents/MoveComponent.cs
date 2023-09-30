using UnityEngine;

public class PlayerMoveComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Move;
    
    [SerializeField] private float speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            blockBehaviour.Move(Vector3.forward * speed);
        } else if (Input.GetKey(KeyCode.A)) {
            blockBehaviour.Move(Vector3.left * speed);
        } else if (Input.GetKey(KeyCode.S)) {
            blockBehaviour.Move(Vector3.back * speed);
        } else if (Input.GetKey(KeyCode.D)) {
            blockBehaviour.Move(Vector3.right * speed);
        }
    }
}
