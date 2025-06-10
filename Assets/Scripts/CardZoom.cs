using UnityEngine;

public class CardZoom : MonoBehaviour
{
    private bool isZoomed = false;
    private Vector3 originalPosition;
    private Vector3 originalScale;

    public Canvas canvas; // arraste o Canvas principal aqui

    public void ToggleZoom()
    {
        if (!isZoomed)
        {
            originalPosition = transform.position;
            originalScale = transform.localScale;

            // Traz para frente e centraliza
            transform.SetParent(canvas.transform, true);
            transform.SetAsLastSibling();
            transform.position = canvas.GetComponent<RectTransform>().position;
            transform.localScale = Vector3.one * 2f;

            isZoomed = true;
        }
        else
        {
            transform.position = originalPosition;
            transform.localScale = originalScale;
            isZoomed = false;
        }
    }
}
