using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EsceneNewGame(){
        SceneManager.LoadScene("Floor_2");
    }
    public void EsceneCutePuppies(){
        SceneManager.LoadScene("Sorry");
    }

    
    
}
