using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameStateVariables
{
    public static List<SceneVariables> sceneVariablesList = new List<SceneVariables>();

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static SceneVariables GetCurrentSceneVariables()
    {
        SceneVariables currentSceneVariables = GetSceneVariablesByName(SceneManager.GetActiveScene().name);
        if(currentSceneVariables == null)
        {
            sceneVariablesList.Add(new SceneVariables(GetCurrentSceneName()));
            currentSceneVariables = GetSceneVariablesByName(SceneManager.GetActiveScene().name);
        }

        return currentSceneVariables;        
    }

    public static SceneVariables GetSceneVariablesByName(string sceneName)
    {
        SceneVariables sceneVariablesToReturn = null;
        bool found = false;
        int i = 0;
        while (i < sceneVariablesList.Count && !found)
        {
            if (sceneVariablesList[i].ToString() == sceneName)
            {
                sceneVariablesToReturn = sceneVariablesList[i];
                found = true;
            }
            else
            {
                i++;
            }
        }

        return sceneVariablesToReturn;
    }

    public static DoorState GetDoorStateByName(string inSceneName, string inDoorName)
    {
        return GetSceneVariablesByName(inSceneName).GetDoorStateByName(inDoorName);
    }

    public static void LoadSceneVariables(string inSceneName)
    {
        SceneVariables savedVariables = GetSceneVariablesByName(inSceneName);
        Debug.Log(savedVariables.ToString());
        savedVariables.VerifyDoorStates();
    }

    public static void UpdateVariableStateInCurrentScene(string type, object inObjectState)
    {
        switch(type)
        {
            case "door":
                GetCurrentSceneVariables().UpdateDoorState((DoorState)inObjectState);
                break;
            default:
                break;
                  
        }
    }
}
