using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] _tileOptions;
    [SerializeField] GameObject connector;

    private void Start()
    {
        for (int x = -5; x <= 5; x++) {
            for (int z = -5; z <= 5; z++) {
                if (x == 0 && z == 0) {
                    // Starting tile
                    AddNorthConnector(x, z);
                    AddEastConnector(x, z);
                }
                else {
                    AddTile(x, z, _tileOptions[Random.Range(0, _tileOptions.Length)]);
                }
            }
        }
    }

    private void AddTile(int x, int z, GameObject prefab)
    {
        GameObject tile = Instantiate(prefab);
        tile.transform.SetParent(transform);
        tile.transform.position = new Vector3(x * 20, 0, z * 20);
        AddNorthConnector(x, z);
        AddEastConnector(x, z);
    }

    private void AddNorthConnector(int x, int z)
    {
        GameObject go = Instantiate(connector);
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(x * 20, 0, z * 20 + 10);
        go.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
    }

    private void AddEastConnector(int x, int z)
    {
        GameObject go = Instantiate(connector);
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(x * 20 + 10, 0, z * 20);
        go.transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
    }
}
