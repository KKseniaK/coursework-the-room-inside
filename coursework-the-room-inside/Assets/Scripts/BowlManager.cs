using UnityEngine;

public class BowlManager : MonoBehaviour
{
    public Bowl foodBowl;
    public Bowl waterBowl;
    public CatWalkSequence catWalkController;
    public GameObject catWalkCanvas;

    private bool animationPlayed = false;

    public void CheckBowls()
    {
        if (!animationPlayed && foodBowl.isFilled && waterBowl.isFilled)
        {
            animationPlayed = true;
            Debug.Log("Обе миски заполнены! Запускаем анимацию лапок...");
            catWalkCanvas.SetActive(true);
            catWalkController.PlaySequence();
        }
    }
}

