using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<InventarioManager.ObjetoInventarioId> inventario;

    [InlineEditor] public List<ItemsInScene> itemsInScene;
    public GameObject container;

    private bool inWinScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        foreach (var itemList in itemsInScene)
        {
            foreach (var item in itemList.objectsInScene)
            {
                item.isTaken = false;
            }
        }

        inventario.Clear();
    }

    private void Update()
    {
        foreach (var itemsList in itemsInScene)
        {
            foreach (var markableObject in itemsList.objectsInScene)
            {
                if (!markableObject.isTaken) return;
            }
        }

        if (!inWinScene) SceneManager.LoadScene("WinScreen");
        inWinScene = true;
    }

    public void ChargeItems(string sceneName)
    {
        inWinScene = false;
        ItemsInScene itemList = itemsInScene.Find(x => x.sceneName == sceneName);

        foreach (var item in itemList.objectsInScene)
        {
            if (item.isTaken)
            {
                foreach (var child in container.GetComponentsInChildren<InventarioObjetoRecogible>())
                {
                    child.gameObject.SetActive(child.id != item.idObject);
                }
            }
        }
    }

    public void SetItemTaken(string sceneName, int id)
    {
        ItemsInScene itemList = itemsInScene.Find(x => x.sceneName == sceneName);

        MarkableObject item = itemList.objectsInScene.Find(x => x.idObject == id);

        item.isTaken = true;
    }
}