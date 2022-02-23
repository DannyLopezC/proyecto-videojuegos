using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour {

    [System.Serializable]
    public struct ObjetoInventarioId
    {
        public int id;
        public int cantidad;

        public ObjetoInventarioId(int id, int cantidad)
        {
            this.id = id;
            this.cantidad = cantidad;
        }
    }

    public GameObject Inventario;
    public InventarioBD baseDatos;
    public List<ObjetoInventarioId> inventario;

    public void addInventario(int id, int cantidad)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].id == id)
            {
                inventario[i] = new ObjetoInventarioId(inventario[i].id, inventario[i].cantidad + cantidad);
                updateInventario();
                return;
            }
        }
        inventario.Add(new ObjetoInventarioId(id, cantidad));
        updateInventario();
    }
    public void deleteInventario(int id, int cantidad)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].id == id)
            {
                inventario[i] = new ObjetoInventarioId(inventario[i].id, inventario[i].cantidad - cantidad);
                if (inventario[i].cantidad <= 0)
                    inventario.Remove(inventario[i]);
                updateInventario();
                return;
            }
        }
        Debug.LogError("No existe el objeto a eliminar");
    }

    public void IntercambiarPuestos(int i1 , int i2)
    {
        ObjetoInventarioId i = inventario[i1];
        inventario[i1] = inventario[i2];
        inventario[i2] = i;
        updateInventario();
    }

    public void Start()
    {
        updateInventario();
    }

    public void Update()
    {
        if( Input.GetKeyDown(KeyCode.Q))
        {
            updateInventario();
        }
    }


    public InventarioObjetoInterface prefab;
    public Transform inventarioUI;
    List<InventarioObjetoInterface> pool = new List<InventarioObjetoInterface>();

    public void updateInventario()
    {
        print("InventarioActualizado");
        for (int i = 0; i < pool.Count; i++)
        {
            if (i < inventario.Count)
            {
                ObjetoInventarioId o = inventario[i];
                pool[i].sprite.sprite = baseDatos.baseDatos[o.id].sprite;
                pool[i].cantidad.text = o.cantidad.ToString();
                pool[i].id = i;
                pool[i].isActive = false;
                pool[i].cambiarColorFondo();

                pool[i].boton.onClick.RemoveAllListeners();
                pool[i].boton.onClick.AddListener(() => gameObject.SendMessage(baseDatos.baseDatos[o.id].funcion, SendMessageOptions.DontRequireReceiver));

                pool[i].gameObject.SetActive(true);
            }
            else
            {
                pool[i].gameObject.SetActive(false);
            }
        }
        if (inventario.Count > pool.Count)
        {
            for (int i = pool.Count; i < inventario.Count; i++)
            {
                InventarioObjetoInterface oi = Instantiate(prefab, inventarioUI);
                pool.Add(oi);

                oi.transform.position = Vector3.zero; 
                oi.transform.localScale = Vector3.one;

                ObjetoInventarioId o = inventario[i];
                pool[i].sprite.sprite = baseDatos.baseDatos[o.id].sprite;
                pool[i].cantidad.text = o.cantidad.ToString();
                pool[i].id = i;
                pool[i].manager = this;
                pool[i].isActive = false;
                pool[i].cambiarColorFondo();

                pool[i].boton.onClick.RemoveAllListeners();
                pool[i].boton.onClick.AddListener(() => gameObject.SendMessage(baseDatos.baseDatos[o.id].funcion, SendMessageOptions.DontRequireReceiver));

                pool[i].gameObject.SetActive(true);
            }
        }
    }

    public void Pocion()
    {
        print("Usada");
        deleteInventario(3, 1);
    }
}
