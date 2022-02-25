using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public class MarkableObject : MonoBehaviour
{
    [InlineEditor] public List<GameObject> objectTaken;
    public bool isTaken;
}