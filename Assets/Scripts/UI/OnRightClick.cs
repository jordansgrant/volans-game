using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OnRightClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData data)
    {
        switch (data.button)
        {
            case PointerEventData.InputButton.Left:
                Debug.Log("Left click");
                break;
            case PointerEventData.InputButton.Right:
                Debug.Log("Right click");
                print(transform.parent.name);
                //FittingManager.TrashModInInventory(transform.parent.name);
                print(GameManager.game.pData.moduleInventory);

                break;
            case PointerEventData.InputButton.Middle:
                Debug.Log("Middle click");
                break;
        }

    }
}