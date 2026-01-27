using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;

    public void OnMainMenu()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void OnControlsMenu()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
