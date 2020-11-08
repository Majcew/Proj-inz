using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) MainMenu();
    }
    public void PlaySingle()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayMulti()
    {
        SceneManager.LoadScene(6);
    }

    public void Options()
    {
        SceneManager.LoadScene(5);
    }

    public void Authors()
    {
        SceneManager.LoadScene(4);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}