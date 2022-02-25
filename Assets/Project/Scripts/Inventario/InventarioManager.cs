using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventarioManager : MonoBehaviour
{
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
    public GameObject objects;
    public InventarioBD baseDatos;

    public void addInventario(int id, int cantidad)
    {
        for (int i = 0; i < GameManager.instance.inventario.Count; i++)
        {
            if (GameManager.instance.inventario[i].id == id)
            {
                GameManager.instance.inventario[i] = new ObjetoInventarioId(GameManager.instance.inventario[i].id,
                    GameManager.instance.inventario[i].cantidad + cantidad);
                updateInventario();
                return;
            }
        }

        GameManager.instance.inventario.Add(new ObjetoInventarioId(id, cantidad));
        updateInventario();
    }

    public void deleteInventario(int id, int cantidad)
    {
        for (int i = 0; i < GameManager.instance.inventario.Count; i++)
        {
            if (GameManager.instance.inventario[i].id == id)
            {
                GameManager.instance.inventario[i] = new ObjetoInventarioId(GameManager.instance.inventario[i].id,
                    GameManager.instance.inventario[i].cantidad - cantidad);
                if (GameManager.instance.inventario[i].cantidad <= 0)
                    GameManager.instance.inventario.Remove(GameManager.instance.inventario[i]);
                updateInventario();
                return;
            }
        }

        Debug.LogError("No existe el objeto a eliminar");
    }

    public void IntercambiarPuestos(int i1, int i2)
    {
        ObjetoInventarioId i = GameManager.instance.inventario[i1];
        GameManager.instance.inventario[i1] = GameManager.instance.inventario[i2];
        GameManager.instance.inventario[i2] = i;
        updateInventario();
    }

    public void Start()
    {
        GameManager.instance.container = objects;
        GameManager.instance.ChargeItems(SceneManager.GetActiveScene().name);
        updateInventario();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sacarDelInventario();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            updateInventario();
        }
    }


    public InventarioObjetoInterface prefab;
    public Transform inventarioUI;
    List<InventarioObjetoInterface> pool = new List<InventarioObjetoInterface>();

    public void updateInventario()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (i < GameManager.instance.inventario.Count)
            {
                ObjetoInventarioId o = GameManager.instance.inventario[i];
                pool[i].sprite.sprite = baseDatos.baseDatos[o.id].sprite;
                pool[i].cantidad.text = o.cantidad.ToString();
                pool[i].id = i;
                pool[i].isActive = false;
                pool[i].cambiarColorFondo();

                pool[i].boton.onClick.RemoveAllListeners();
                pool[i].boton.onClick.AddListener(() =>
                    gameObject.SendMessage(baseDatos.baseDatos[o.id].funcion, SendMessageOptions.DontRequireReceiver));

                pool[i].gameObject.SetActive(true);
            }
            else
            {
                pool[i].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.inventario.Count > pool.Count)
        {
            for (int i = pool.Count; i < GameManager.instance.inventario.Count; i++)
            {
                InventarioObjetoInterface oi = Instantiate(prefab, inventarioUI);
                pool.Add(oi);

                oi.transform.position = Vector3.zero;
                oi.transform.localScale = Vector3.one;

                ObjetoInventarioId o = GameManager.instance.inventario[i];
                pool[i].sprite.sprite = baseDatos.baseDatos[o.id].sprite;
                pool[i].cantidad.text = o.cantidad.ToString();
                pool[i].id = i;
                pool[i].manager = this;
                pool[i].isActive = false;
                pool[i].cambiarColorFondo();

                pool[i].boton.onClick.RemoveAllListeners();
                pool[i].boton.onClick.AddListener(() =>
                    gameObject.SendMessage(baseDatos.baseDatos[o.id].funcion, SendMessageOptions.DontRequireReceiver));

                pool[i].gameObject.SetActive(true);
            }
        }
    }

    public void sacarDelInventario()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            print(pool[i].isActive);
            if (!pool[i].isActive)
            {
                deleteInventario(pool[i].id, 1);
                print("Objeto eliminado");
                break;
            }
        }
    }

    public void Pocion()
    {
        print("Usada");
        deleteInventario(3, 1);
    }
}