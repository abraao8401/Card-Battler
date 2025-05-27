using UnityEngine;
using TMPro;

public class UIBonusAttackIndicator : MonoBehaviour
{
    public TMP_Text bonusText;
    public float moveSpeed = 20f;
    public float lifetime = 2f;

    private RectTransform myRect;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        myRect.anchoredPosition += new Vector2(0f, moveSpeed * Time.deltaTime);
    }
}
