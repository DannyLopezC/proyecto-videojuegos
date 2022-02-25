using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioCollider : MonoBehaviour {

    InventarioManager m;

    void Start()
    {
        m = GetComponent<InventarioManager>();
    }


	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col)
    {
        if (col.GetComponent<InventarioObjetoRecogible>() != null )
        {
            InventarioObjetoRecogible i = col.GetComponent<InventarioObjetoRecogible>();
            m.addInventario(i.id, i.cantidad);
            Destroy(col.gameObject);
        }		
	}
}
