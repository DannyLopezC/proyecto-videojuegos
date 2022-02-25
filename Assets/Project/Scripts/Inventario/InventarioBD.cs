using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BDInventario", menuName = "Inventory/BaseDatos", order = 1)]
public class InventarioBD : ScriptableObject
{
    [System.Serializable]
    public struct ObjetoInventario
    {
        public string nombre;
        public Sprite sprite;
        public Uso uso;
        public string caracteristicas;
        public string funcion;
        public ItemsInScene scene;
    }

    public enum Uso
    {
        acumulable,
        equipable,
        consumible
    }

    public ObjetoInventario[] baseDatos;
}