using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    public GameObject Inventario;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Q))
        {
            closeInventario();
        }
    }

    public void closeInventario()
    {
        Inventario.SetActive(!Inventario.activeInHierarchy);
    }
}
