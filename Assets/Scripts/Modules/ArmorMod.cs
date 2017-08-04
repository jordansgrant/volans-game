[System.Serializable]
public class ArmorMod : IModule {

    public int additionalArmor = 50;
    private bool active = false;

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
