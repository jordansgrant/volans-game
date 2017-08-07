using UnityEngine;

[System.Serializable]
public class PowerMod : MonoBehaviour, IModule {

    public int additionalPower = 50;
    private bool active = false;
    public string name = "Flux Capacitor";

    public string getName()
    {
        return name;
    }

    public bool isActive()
    {
        return active;
    }

    public void addEffect()
    {
        GameManager.game.pData.maxPower += additionalPower;
        active = true;
    }

    public void removeEffect()
    {
        GameManager.game.pData.maxPower -= additionalPower;
        active = false;
    }
}
