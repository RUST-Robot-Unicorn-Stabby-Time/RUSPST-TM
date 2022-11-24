using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Data/Game Generator")]
public class GameGenerator : ScriptableObject
{
    public GameData gameData;
    public List<RoomGroup> roomGroups;

    public void GenerateGame () 
    {
        if (gameData.levelList == null) gameData.levelList = new List<string>();

        gameData.levelList.Clear();

        foreach (RoomGroup roomGroup in roomGroups)
        {
            List<string> pool = new List<string>(roomGroup.rooms);
            for (int i = 0; i < roomGroup.selectionCount; i++)
            {
                string room = pool[Random.Range(0, pool.Count)];
                
                gameData.levelList.Add(room);
                pool.Remove(room);
            }
        }

        gameData.LoadNextLevel();
    }
}

[System.Serializable]
public class RoomGroup
{
    public string label;
    public int selectionCount;
    public List<string> rooms;
}

// Propogates GameData's list of scenes with the current runs scenes.