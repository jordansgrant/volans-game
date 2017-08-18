using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class GameManager : MonoBehaviour {

    public static GameManager game;

    // Player Information
    public PlayerData pData;

    // Solar System Information
    public SolarSystem sData;
   
    public Dictionary<string, WeaponInfo> weaponTypes;
    
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

        weaponTypes = new Dictionary<string, WeaponInfo>();

        weaponTypes.Add("laser_bolt", new WeaponInfo());
        weaponTypes["laser_bolt"].fireCount = 1;
        weaponTypes["laser_bolt"].fireDelay = 0.05f;
        weaponTypes["laser_bolt"].fireRate = 0.3f;
        weaponTypes["laser_bolt"].projectile = "laser_bolt";

        weaponTypes.Add("bullet", new WeaponInfo());
        weaponTypes["bullet"].fireCount = 3;
        weaponTypes["bullet"].fireDelay = 0.05f;
        weaponTypes["bullet"].fireRate = 0.3f;
        weaponTypes["bullet"].projectile = "bullet";

    }

    public void Save()
    {
        // Save Player information
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/player.dat");
        bf.Serialize(file, pData);
        file.Close();

        SurrogateSelector surrogateSelector = new SurrogateSelector();
        Vector2SerializationSurrogate vector2SS = new Vector2SerializationSurrogate();

        surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SS);
        bf.SurrogateSelector = surrogateSelector;

        file = File.Create(Application.persistentDataPath + "/solar_system.dat");
        bf.Serialize(file, sData);
        file.Close();
    }

    public void Load()
    {
        // Load Player Information
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
        pData = bf.Deserialize(file) as PlayerData;
        file.Close();

        SurrogateSelector surrogateSelector = new SurrogateSelector();
        Vector2SerializationSurrogate vector2SS = new Vector2SerializationSurrogate();

        surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SS);
        bf.SurrogateSelector = surrogateSelector;

        file = File.Open(Application.persistentDataPath + "/solar_system.dat", FileMode.Open);
        sData = bf.Deserialize(file) as SolarSystem;
        file.Close();
    }


}
