using UnityEngine;

public class UIBonusAttackSpawner : MonoBehaviour
{
    public static UIBonusAttackSpawner instance;

    public GameObject bonusAttackPrefab; // arraste o prefab aqui
    public Canvas canvas; // arraste o Canvas da batalha aqui

    private void Awake()
    {
        instance = this;
    }

    public void ShowBonusAttackText(Vector3 worldPosition)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        GameObject newText = Instantiate(bonusAttackPrefab, canvas.transform);
        RectTransform rect = newText.GetComponent<RectTransform>();
        rect.anchoredPosition = screenPos;
    }
}