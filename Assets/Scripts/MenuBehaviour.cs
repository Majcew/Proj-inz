using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) MainMenu();
    }
    public void PlaySingle()
    {
        PlayerPrefs.SetInt("start_action_mode", 1);
        PlayerPrefs.SetString("mode", "singleplayer");
        SceneManager.LoadScene(1);
    }

    public void PlayMulti()
    {
        PlayerPrefs.SetString("mode", "multiplayer");
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

    public void PVPMap()
    {
        SceneManager.LoadScene(3);
    }

    public void ChooseMap(int scene_id) 
    {
        PlayerPrefs.SetInt("scene_id", scene_id);
        SceneManager.LoadScene(8); //SubMultiMenu
    }

    public void CreateGame()
    {
        PlayerPrefs.SetInt("start_action_mode", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("scene_id"));
    }

        public void Join()
    {
        PlayerPrefs.SetInt("start_action_mode", 2);
        PlayerPrefs.SetString("network_address", GameObject.Find("InputField").GetComponent<InputField>().text);
        SceneManager.LoadScene(PlayerPrefs.GetInt("scene_id"));
    }
}