using UnityEngine;

public class Win : MonoBehaviour
{
    public GameController gameController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Debug.Log("WIN!");
            gameController.Win();
        }
    }
}
