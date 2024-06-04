using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class Functions
{
    // alternative unity function FindObjectsOfType
    static public int FindAvailableGameObjectFromList(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(!list[i].activeSelf)
            {
                return i;
            }
        }
        return -1;
    }

    static public List<GameObject> GetAllChildObjects(GameObject parent)
    {
        List<GameObject> childObjects = new List<GameObject>();

        // loop through each child transform of the parent
        foreach (Transform childTransform in parent.transform)
        {
            // add the child GameObject to the list
            childObjects.Add(childTransform.gameObject);
        }
        return childObjects;
    }

    static public string ChangeAnimationState(string newState, Animator animator)
    {
        // play animation
        animator.Play(newState);

        return newState;
    }

    // find descendent object's transform
    public static Transform FindDescendentTransform(this Transform parent, string name) 
    {

        var searchResult = parent.Find(name);

        if (searchResult != null)
            return searchResult;

        foreach (Transform child in parent) {
            searchResult = child.Find(name);
            if (searchResult != null)
                return searchResult;
        }

        return null;
    }
}
