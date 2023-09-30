using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] _tileOptions;
    [SerializeField] private GameObject _connector;
    [SerializeField] private Canvas _mainMenu;
    [SerializeField] private GameObject _victoryText;
    [SerializeField] private GameObject _defeatText;
    [SerializeField] private BlockBehaviour _playerPrefab;
    [SerializeField] private Follow _followCamera;
    [SerializeField] private Win _win;

    public void Start()
    {
        Time.timeScale = 0f;
    }

    public void Win()
    {
        ShowMenu(true);
    }

    public void Lose()
    {
        ShowMenu(false);
    }

    public void StartNewGame()
    {
        TearDown();
        GenerateAllTiles();
        GeneratePlayer();
        HideMenu();
    }

    private void TearDown()
    {
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void GenerateAllTiles()
    {
        for (int x = -1; x <= 5; x++) {
            for (int z = -1; z <= 5; z++) {
                if (x == 0 && z == 0) {
                    // Starting tile
                    AddNorthConnector(x, z);
                    AddEastConnector(x, z);
                } else if (x == 4 && z == 4) {
                    // Goal tile
                    AddNorthConnector(x, z);
                    AddEastConnector(x, z);
                } else {
                    AddTile(x, z, _tileOptions[Random.Range(0, _tileOptions.Length)]);
                }
            }
        }
    }

    private void GeneratePlayer()
    {
        BlockBehaviour player = Instantiate(_playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity, transform);
        _followCamera.SetTarget(player.gameObject);
        player.gameController = this;
        _win.gameController = this;
    }

    private void HideMenu()
    {
        _mainMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowMenu(bool isWin)
    {
        _mainMenu.gameObject.SetActive(true);
        _victoryText.SetActive(isWin);
        _defeatText.SetActive(!isWin);
        Time.timeScale = 0f;
    }


    private void AddTile(int x, int z, GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, new Vector3(x * 20, 0, z * 20), Quaternion.identity, transform);
        AddNorthConnector(x, z);
        AddEastConnector(x, z);
    }

    private void AddNorthConnector(int x, int z)
    {
        GameObject go = Instantiate(_connector);
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(x * 20, 0, z * 20 + 10);
        go.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
    }

    private void AddEastConnector(int x, int z)
    {
        GameObject go = Instantiate(_connector);
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(x * 20 + 10, 0, z * 20);
        go.transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
    }
}
