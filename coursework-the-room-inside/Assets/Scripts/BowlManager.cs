using UnityEngine;

public class BowlManager : MonoBehaviour
{
    public Bowl foodBowl;
    public Bowl waterBowl;
    public GameObject puzzle;

    public GameObject pawCanvas; 
    public PawAnimationController pawAnimationController;

    private bool animationPlayed = false;
    private void Start()
    {
        pawAnimationController.AnimationCompleted += ResetStage; 
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
        Debug.Log("�������� ��������� � ���������� ����� � ��������� ���������");

        // ����� ����������
        foodBowl.ResetBowl();
        waterBowl.ResetBowl();

        // �������� ������ � ������ 
        puzzle.SetActive(true);

        animationPlayed = false;
    }
}


