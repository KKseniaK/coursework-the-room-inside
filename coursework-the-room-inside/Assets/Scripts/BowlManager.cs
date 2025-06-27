using UnityEngine;

public class BowlManager : MonoBehaviour
{
    public Bowl foodBowl;
    public Bowl waterBowl;

    public GameObject puzzle;
    public GameObject catOnChair; //  Кот, сидящий на стуле

    public GameObject pawCanvas;
    public PawAnimationController pawAnimationController;

    private bool animationPlayed = false;

    private void Start()
    {
        if (pawAnimationController != null)
            pawAnimationController.AnimationCompleted += ResetStage;

        if (catOnChair != null)
            catOnChair.SetActive(false); // Кот изначально невидим
    }

    public void CheckBowls()
    {
        if (!animationPlayed && foodBowl.isFilled && waterBowl.isFilled)
        {
            animationPlayed = true;

            pawCanvas.SetActive(true);
            pawAnimationController.StartSequence();
        }
    }

    private void ResetStage()
    {
        Debug.Log("Анимация завершена — возвращаем сцену в начальное состояние");

        foodBowl.ResetBowl();
        waterBowl.ResetBowl();

        if (puzzle != null)
            puzzle.SetActive(true);

        if (catOnChair != null)
            catOnChair.SetActive(true); //  Включаем кота

        animationPlayed = false;
    }
}
