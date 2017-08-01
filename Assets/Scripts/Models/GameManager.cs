using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameManager : MonoBehaviour {

    public static GameManager game;

    // Player Information
    public int playerArmor;
    public int playerPower;

    public float playerRotationSpeed;
    public int playerAcceleration;
    public int playerMaxVelocity;

    public string playerProjectile;

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
        PlayerData pData = new PlayerData();
        pData.armor = playerArmor;
        pData.power = playerPower;

        pData.rotationSpeed = playerRotationSpeed;
        pData.acceleration = playerAcceleration;
        pData.maxVelocity = playerMaxVelocity;

        pData.projectile = playerProjectile;

        bf.Serialize(file, pData);
        file.Close();
    }

    public void Load()
    {
        // Load Player Information
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);

        PlayerData pData = (PlayerData)bf.Deserialize(file);
        file.Close();

        playerArmor = pData.armor;
        playerPower = pData.power;
    }


}
