using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressInfo {

    private static int worldsCount = 4;

    public Dictionary<int, int> worldsProgress = new Dictionary<int, int>();


    public GameProgressInfo()
    {
        for (int world = 1; world <= worldsCount; world++)
        {
            worldsProgress[world] = 1;
        }
    }

    private void SaveGameProgress()
    {
        for (int world = 1; world <= worldsCount; world++)
        {
            PlayerPrefs.SetInt(world.ToString(), worldsProgress[world]);
        }
    }

    public GameProgressInfo LoadProgress(out bool existingProgress)
    {
        if (PlayerPrefs.HasKey("1"))
        {
            GameProgressInfo progress = new GameProgressInfo();
            for (int world = 1; world <= worldsCount; world++)
            {
                progress.worldsProgress[world] = PlayerPrefs.GetInt(world.ToString());
            }
            existingProgress = true;
            return progress;

        }
        else
        {
            existingProgress = false;
            return null;
        }
    }
}
