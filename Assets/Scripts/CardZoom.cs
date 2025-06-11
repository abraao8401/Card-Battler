using UnityEngine;

public class CardZoom : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 targetScale;

    public float moveSpeed = 5f;
    public float rotateSpeed = 540f;

    public float zoomDistance = 1.75f;
    public Vector3 zoomScale = new Vector3(1.7f, 1.7f, 1.7f);

    private bool isZoomed = false;
    private bool isSelected = false;
    private bool hasSavedOriginal = false;

    void Start()
    {
        // Inicializa com valores atuais
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;

        targetPosition = originalPosition;
        targetRotation = originalRotation;
        targetScale = originalScale;
    }

    void Update()
    {
        // Suaviza transformações
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, moveSpeed * Time.deltaTime);

        // Se selecionado e apertar Ctrl → faz zoom
        if (isSelected && !isZoomed && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            ZoomIn();
        }
    }

    private void OnMouseDown()
    {
        if (!hasSavedOriginal)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            originalScale = transform.localScale;
            hasSavedOriginal = true;
        }

        // Se já estiver com zoom, clicando novamente volta ao original
        if (isZoomed)
        {
            ZoomOut();
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
    }

    private void ZoomIn()
    {
        isZoomed = true;

        Vector3 camPos = Camera.main.transform.position;
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camUp = Camera.main.transform.up;

        targetPosition = camPos + camForward * zoomDistance + camUp * 0.2f;
        targetRotation = Quaternion.Euler(-15f, 0f, 0f);
        targetScale = zoomScale;
    }

    private void ZoomOut()
    {
        isZoomed = false;

        targetPosition = originalPosition;
        targetRotation = originalRotation;
        targetScale = originalScale;
    }
}
