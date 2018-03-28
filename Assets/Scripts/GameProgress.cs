using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameProgress : MonoBehaviour
{

    public static GameProgress Instance { get; private set; }

    public Progress PlayerProgress { get; set; }
    [SerializeField]
    string[] allGeomitNames;

    //[HideInInspector]
    //public bool progressLoaded;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadProgress();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadProgress()
    {
        PlayerProgress = new Progress();
        PlayerProgress.Load(allGeomitNames);
    }

    public void SaveProgress()
    {
        PlayerProgress.Save();
    }

    public class Progress
    {
        private const int worldsCount = 4;
        private static readonly string[] defaultProjectiles = { "Blues Baller", "Beiges Baller" };

        public Dictionary<int, int> worldsProgress = new Dictionary<int, int>();
        public List<string> projectilesOwned = new List<string>();
        public int goldCoins = 0;
        public int diamondCoins = 0;
        public int goldStars = 0;
        public int diamondStars = 0;

        public void Load(string[] geomitsNames)
        {
            //Cargamos el progreso de niveles por mundos

            //Si ya se ha guardado antes, (existe la llave 1 que contiene el progreso del mundo 1, si existe esta llave entonces existen todas las demas)
            if (PlayerPrefs.HasKey("1"))
            {
                for (int world = 1; world <= worldsCount; world++)
                {
                    worldsProgress.Add(world, PlayerPrefs.GetInt(world.ToString()));
                }
            }
            else
            {
                for (int world = 1; world <= worldsCount; world++)
                {
                    worldsProgress.Add(world, 1);
                }
            }

            //Cargamos los geomits que posee el jugador
            for (int index = 0; index < geomitsNames.Length; index++)
            {
                if (PlayerPrefs.HasKey(geomitsNames[index]))
                {
                    //Si el jugador posee el geomit (valor = 1)
                    if(PlayerPrefs.GetInt(geomitsNames[index]) == 1)
                    {
                        projectilesOwned.Add(geomitsNames[index]);
                    }
                }
                else
                {
                    //Si la ista de defaults contiene el nombre actual
                    if (defaultProjectiles.ToArray().Contains(geomitsNames[index]))
                    {
                        //Creamos la llave y valor 1 (si lo posee el jugador)
                        PlayerPrefs.SetInt(geomitsNames[index], 1);
                        projectilesOwned.Add(geomitsNames[index]);
                    }
                    else
                    {
                        //Para la primera vez, creamos la llave, con el nombre del geomit y valor 0, (no lo posee el jugador)
                        PlayerPrefs.SetInt(geomitsNames[index], 0);
                    }
                }
            }


            //Cargamos las monedas y estrellas
            if (PlayerPrefs.HasKey("GoldCoins"))
                goldCoins = PlayerPrefs.GetInt("GoldCoins");

            if (PlayerPrefs.HasKey("DiamondCoins"))
                diamondCoins = PlayerPrefs.GetInt("DiamondCoins");

            if (PlayerPrefs.HasKey("GoldStars"))
                goldStars = PlayerPrefs.GetInt("GoldStars");

            if (PlayerPrefs.HasKey("DiamondStars"))
                diamondStars = PlayerPrefs.GetInt("DiamondStars");
        }

        public void Save()
        {
            //Guardmos el progreso de los mundos
            for (int world = 1; world <= worldsCount; world++)
            {
                PlayerPrefs.SetInt(world.ToString(), worldsProgress[world]);
            }

            for (int index = 0; index < projectilesOwned.Count; index++)
            {
                PlayerPrefs.SetInt(projectilesOwned[index], 1);
            }

            PlayerPrefs.SetInt("GoldCoins", goldCoins);
            PlayerPrefs.SetInt("DiamondCoins", diamondCoins);
            PlayerPrefs.SetInt("GoldStars", goldStars);
            PlayerPrefs.SetInt("DiamondStars", diamondStars);
        }
    }
}
