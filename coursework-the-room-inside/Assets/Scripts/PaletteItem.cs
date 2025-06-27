// Script на PaletteZone
using UnityEngine;

public class PaletteDropZone : MonoBehaviour
{
    [Header("Большая палитра")]
    public GameObject bigPaletteObject;

    public void ShowBigPalette()
    {
        if (bigPaletteObject != null)
            bigPaletteObject.SetActive(true);
    }
}
