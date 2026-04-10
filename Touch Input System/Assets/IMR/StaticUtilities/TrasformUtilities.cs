using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrasformUtilities 
{
    public static Transform GetHighestParent(Transform child)
    {
        // Keep moving up the hierarchy until there is no parent
        while (child.parent != null)
        {
            child = child.parent;
        }
        return child;
    }

    public static List<Transform> GetAllChildrenList(Transform parent)
    {
        List<Transform> allChildren = new List<Transform>();
        // Keep moving up the hierarchy until there is no parent
        for(int i = 0; i <= parent.childCount; i++)
        {
            allChildren.Add(parent.GetChild(i));
        }


        return allChildren;
    }

    public static List<T> GetComponentChildrenList<T>(Transform parent)
    {
       

        List<T> allChildren = new List<T>();
        // Keep moving up the hierarchy until there is no parent
        for (int i = 0; i < parent.childCount ; i++)
        {
            allChildren.Add(parent.GetChild(i).GetComponent<T>());
        }


        return allChildren;
    }
}
