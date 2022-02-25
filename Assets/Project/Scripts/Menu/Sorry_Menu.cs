using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sorry_Menu : MonoBehaviour
{
    public void EsceneMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");

        GameManager.instance.StartGame();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}