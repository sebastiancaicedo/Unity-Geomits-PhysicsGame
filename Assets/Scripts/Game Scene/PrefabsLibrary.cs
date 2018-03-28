using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsLibrary : MonoBehaviour {

    public static PrefabsLibrary Instance { get; private set; }

    [SerializeField]
    GeomitProjectile[] projectilesPrefabs;

    private Dictionary<string, GeomitProjectile> projectilesDictionary = new Dictionary<string, GeomitProjectile>();

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            for (int index = 0; index < projectilesPrefabs.Length; index++)
            {
                projectilesDictionary.Add(projectilesPrefabs[index].gameObject.name, projectilesPrefabs[index]);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GeomitProjectile GetProjectile(string name)
    {
        return projectilesDictionary[name];
    }

    public GeomitProjectile GetAvailableRandomProjectile()
    {
        int maxIndex = GameProgress.Instance.PlayerProgress.projectilesOwned.Count;
        int selectedIndex = Random.Range(0, maxIndex);
        return GetProjectile(GameProgress.Instance.PlayerProgress.projectilesOwned[selectedIndex]);
    }
}
