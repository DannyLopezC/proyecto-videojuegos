using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventarioCollider : MonoBehaviour
{
    InventarioManager m;

    void Start()
    {
        m = GetComponent<InventarioManager>();
    }


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<InventarioObjetoRecogible>() != null)
        {
            InventarioObjetoRecogible i = col.GetComponent<InventarioObjetoRecogible>();
            GameManager.instance.SetItemTaken(SceneManager.GetActiveScene().name, i.id);
            m.addInventario(i.id, i.cantidad);
            Destroy(col.gameObject);
        }
    }
}