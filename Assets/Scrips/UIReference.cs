using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReference : MonoBehaviour
{
    public List<GameObject> ObjectList = new List<GameObject>();

    public T Get<T>(int index) where T : Component
    {
        return ObjectList[index].GetComponent<T>();
    }
}
