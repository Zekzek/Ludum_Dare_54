using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
