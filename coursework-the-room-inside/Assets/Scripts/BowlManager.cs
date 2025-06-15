using UnityEngine;

public class BowlManager : MonoBehaviour
{
    public Bowl foodBowl;
    public Bowl waterBowl;

    private bool animationPlayed = false;

    public void CheckBowls()
    {
        if (!animationPlayed && foodBowl.isFilled && waterBowl.isFilled)
        {
            animationPlayed = true;
            Debug.Log("Обе миски заполнены! Запускаем анимацию лапок...");
        }
    }
}

