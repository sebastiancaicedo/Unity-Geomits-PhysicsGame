using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StarSpawnInfo
{
    [SerializeField]
    CollectableType type;
    [Range(0.01f, 1)]
    [SerializeField]
    float spawnProbability;

    public CollectableType Type { get { return type; } }
    public float SpawnProbability { get { return spawnProbability; } }

}

public class StarsHandler : MonoBehaviour {

    [SerializeField]
    Star[] starsPrefabs;
    [SerializeField]
    StarSpawnInfo[] starsTypeSpawnInfo;
    [SerializeField]
    float spawnFrecuence;
    [SerializeField]
    float starsSpeed = 10;


    Transform[] spawnPointsG1;
    Transform[] spawnPointsG2;
    Dictionary<CollectableType, Star> starsDictionary = new Dictionary<CollectableType, Star>();

    float timeCounter = 0;
    List<Star> stars = new List<Star>();

    private void Awake()
    {
        spawnPointsG1 = new Transform[transform.GetChild(0).childCount];
        for (int index = 0; index < spawnPointsG1.Length; index++)
        {
            spawnPointsG1[index] = transform.GetChild(0).GetChild(index);
        }

        spawnPointsG2 = new Transform[transform.GetChild(1).childCount];
        for (int index = 0; index < spawnPointsG2.Length; index++)
        {
            spawnPointsG2[index] = transform.GetChild(1).GetChild(index);
        }

        for (int index = 0; index < starsPrefabs.Length; index++)
        {
            starsDictionary.Add(starsPrefabs[index].Type, starsPrefabs[index]);
        }
    }

    public void Update()
    {
        timeCounter += Time.deltaTime;
        print(stars.Count);
        if (timeCounter >= spawnFrecuence)
        {
            timeCounter = 0;
            int index = 0;
            do
            {
                if (Random.Range(0, 1) <= starsTypeSpawnInfo[index].SpawnProbability)
                {
                    SpawnStar(starsTypeSpawnInfo[index].Type);
                }
                index++;
            } while (index < starsTypeSpawnInfo.Length);
        }
    }

    private void FixedUpdate()
    {
        if (stars.Count == 0) return;

        for (int index = 0; index < stars.Count; index++)
        {
            if(stars[index] != null)
                stars[index].Rigidbody_.velocity = stars[index].VelocityDir * starsSpeed;
        }
    }

    private void SpawnStar(CollectableType type)
    {
        print("Star Spawned");
        Star starPrefab = starsDictionary[type];
        Transform sp1 = spawnPointsG1[Random.Range(0, spawnPointsG1.Length)];
        Transform sp2 = spawnPointsG2[Random.Range(0, spawnPointsG2.Length)];
        Vector3 starDir = sp2.position - sp1.position;
        Star star = Instantiate(starPrefab, sp1.position, Quaternion.identity);
        star.VelocityDir = starDir.normalized;
        stars.Add(star);
        StartCoroutine(DestroyStar(star, 8));
    }

    IEnumerator DestroyStar(Star star, float afetrTime = 8)
    {
        yield return new WaitForSeconds(afetrTime);

        print("hola");
        if (stars.Contains(star))
        {
            print("Si esta");
            stars.Remove(star);
            if(star)
                Destroy(star.gameObject);
        }
    }

}
