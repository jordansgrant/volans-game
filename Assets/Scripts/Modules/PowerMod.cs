[System.Serializable]
public class PowerMod : IModule {

    public int additionalPower = 50;
    private bool active = false;

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
