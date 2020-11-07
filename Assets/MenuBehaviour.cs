using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    #region Variables
    private string _IP;
    private Text IP_text;
    #endregion

    private void Start()
    {
        IP_text = GameObject.Find("IP").GetComponent<Text>();
    }
    public void Firstlvl()
    {
        SceneManager.LoadScene(1);
    }

    public void Secondlvl()
    {
        SceneManager.LoadScene(2);
    }

    public void Thirdlvl()
    {
        SceneManager.LoadScene(3);
    }
    public void OnTextChange(string text) {
        _IP = text;
        IP_text.text = _IP;
    }
    
}
