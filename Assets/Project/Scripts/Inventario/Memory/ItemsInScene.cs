using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable, CreateAssetMenu(
     menuName = "MemoryObjects",
     fileName = "ObjectsInScene",
     order = 0)]
public class ItemsInScene : ScriptableObject
{
    public string sceneName;
    [InlineEditor] public List<MarkableObject> objectsInScene;
}