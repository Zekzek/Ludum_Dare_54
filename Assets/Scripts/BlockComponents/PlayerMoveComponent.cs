using UnityEngine;

public class MoveComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.Move;
    
    [SerializeField] private float speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            blockBehaviour.Move(transform.forward * speed);
        }
        else if (Input.GetKey(KeyCode.S)) {
            blockBehaviour.Move(-transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(new Vector3(0, -150 * Time.deltaTime, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(new Vector3(0, 150 * Time.deltaTime, 0), Space.Self);
        }
    }
}
