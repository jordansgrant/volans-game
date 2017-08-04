using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameManager : MonoBehaviour {

    public static GameManager game;

    // Player Information
    public PlayerData pData;

    // Solar System Information

    // Use this for initialization
    void Awake() {

        // Set Don't destroy if this is the first time GameManager is Instantiated
        if (game == null)
        {
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        // If the current object is not the same as the one referenced destroy it
        else if (game != this)
        {
            Destroy(gameObject);
        }

    }

    public void Save()
    {
        // Save Player information
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/player.dat");

        bf.Serialize(file, pData);
        file.Close();
    }

    public void Load()
    {
        // Load Player Information
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);

        pData = bf.Deserialize(file) as PlayerData;
        file.Close();
    }


}
