// Script �� PaletteZone
using UnityEngine;

public class PaletteDropZone : MonoBehaviour
{
    [Header("������� �������")]
    public GameObject bigPaletteObject;

    public void ShowBigPalette()
    {
        if (bigPaletteObject != null)
            bigPaletteObject.SetActive(true);
    }
}
