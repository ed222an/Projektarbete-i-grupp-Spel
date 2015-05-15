﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StickBetweenScenes : MonoBehaviour
{
    private static List<GameObject> gameObjects = new List<GameObject>();
    private string[] tagsOnStickyObjects = new string[] { "GameController", "Inventory", "Player", "UI" };

    public static int[] nonGameLevels = new int[] { 0, 18 };

    void Awake()
    {
        GameObject[] objectsToAdd;

        //Add all object that shall stick between every scene.
        foreach(string tag in tagsOnStickyObjects)
        {
            objectsToAdd = GameObject.FindGameObjectsWithTag(tag);

            if (objectsToAdd.Length != 0)
            {
                if (CheckIfObjectExists(objectsToAdd))
                {
                    GameObject objectToDestroy = GetObjectForRemoval(objectsToAdd);
                    if (objectToDestroy == null)
                    {
                        Debug.LogWarning("Unable to get object for destruction in " + this.GetType() + "::Awake()");
                        break;
                    }
                    Debug.Log(objectToDestroy + " destroyed Unique ID = " + objectToDestroy.GetInstanceID());
                    Destroy(objectToDestroy);
                }
                else
                {
                    Debug.Log("obj " + objectsToAdd[0] + " added to sticky list");
                    DontDestroyOnLoad(objectsToAdd[0]);
                    gameObjects.Add(objectsToAdd[0]);
                }
            }
        }
    }

    void Update()
    {
        //If were in a blacklisted scene, remove all objects
        if (IsInBlacklistedScene())
        {
            HandleBlacklistedScenes();
            return;
        }
    }

    GameObject GetObjectForRemoval(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            //Only runs once, always removes the first object. 
            if (!gameObjects.Find(gObj => gObj.GetInstanceID() == obj.GetInstanceID()))
                return obj;
        }

        return null;
    }

    void HandleBlacklistedScenes()
    {
        int levelId = Application.loadedLevel;
        foreach (int level in nonGameLevels)
        {
            //If the current level is blacklisted for the objects, destroy them.
            if (levelId == level)
            {
                //Clear all objects
                foreach (GameObject obj in gameObjects)
                {
                    Debug.Log("Removing obj " + obj + ", blacklisted scene.");
                    Destroy(obj);
                }

                gameObjects.Clear();
            }
        }
    }

    bool IsInBlacklistedScene()
    {
        int levelId = Application.loadedLevel;

        foreach (int level in nonGameLevels)
        {
            if (levelId == level)
                return true;
        }

        return false;
    }

    bool CheckIfObjectExists(GameObject[] otherObjs)
    {
        
        foreach (GameObject aObj in otherObjs)
        {
            //Debug.Log(aObj);
            if (gameObjects.Find(obj => obj == aObj))
                return true;
        }
        return false;
    }
}