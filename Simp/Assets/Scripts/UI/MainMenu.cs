using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string startingScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startingScene);
    }

    public void OpenOptions()
    {

    }

    public void CloseOptions(){

    }
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quiting");
    }
}
