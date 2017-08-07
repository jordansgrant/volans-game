using UnityEngine;

[System.Serializable]
public class ArmorMod : MonoBehaviour, IModule {

    public int additionalArmor = 50;
    private bool active = false;

    public string name; 

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
        GameManager.game.pData.maxArmor += additionalArmor;
        active = true;
    }

    public void removeEffect()
    {
        GameManager.game.pData.maxArmor -= additionalArmor;
        active = false;
    }
}
