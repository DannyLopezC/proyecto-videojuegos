using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [InlineEditor] public List<ItemsInScene> itemsInScene;

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
    }
}