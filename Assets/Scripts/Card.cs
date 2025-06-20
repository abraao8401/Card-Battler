using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardScriptableObject cardSO;
    public bool isPlayer;
    public int currentHealth;
    public int attackPower;
    public TMP_Text healthText, attackText, nameText, descriptionText;
    public Image characterArt, templateArt;

    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed = 5f, rotateSpeed = 540f;

    public bool inHand;
    public int handPosition;
    private HandController theHC;

    private bool isSelected;
    private Collider theCol;

    public LayerMask whatIsDesktop, whatIsPlacement;
    private bool justPressed;

    public CardPlacePoint assignedPlace;
    public Animator anim;

    public float zoomDistance = 1.55f;

    void Start()
    {
        if (targetPoint == Vector3.zero)
        {
            targetPoint = transform.position;
            targetRot = transform.rotation;
        }

        SetupCard();
        theHC = FindObjectOfType<HandController>();
        theCol = GetComponent<Collider>();
    }

    public void SetupCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        UpdateCardDisplay();
        nameText.text = cardSO.cardName;
        descriptionText.text = cardSO.description;
        characterArt.sprite = cardSO.characterSprite;
        templateArt.sprite = cardSO.templateSprite;
    }

    void Update()
    {
        if (isSelected && Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 camForward = Camera.main.transform.forward;
            targetPoint = camPos + camForward * zoomDistance;

            targetRot = Quaternion.Euler(-15f, 0f, 0f);
        }

        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        if (isSelected && BattleController.instance.battleEnded == false && Time.timeScale != 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, whatIsDesktop))
                MoveToPoint(hit.point + new Vector3(0f, 2f, 0f), Quaternion.identity);

            if (Input.GetMouseButtonDown(1))
                ReturnToHand();

            if (Input.GetMouseButtonDown(0) && justPressed == false)
            {
                if (Physics.Raycast(ray, out hit, 100f, whatIsPlacement) && BattleController.instance.currentPhase == BattleController.TurnOrder.playerActive)
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();

                    if (selectedPoint.activeCard == null && selectedPoint.isPlayerPoint)
                    {
                        selectedPoint.activeCard = this;
                        assignedPlace = selectedPoint;
                        MoveToPoint(selectedPoint.transform.position, Quaternion.identity);
                        inHand = false;
                        isSelected = false;
                        theHC.RemoveCardFromHand(this);
                        AudioManager.instance.PlaySFX(4);
                        AvisoZoom.instance.HideHint();
                    }
                    else
                        ReturnToHand();
                }
                else
                    ReturnToHand();
            }
        }

        justPressed = false;
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRot = rotToMatch;
    }

    private void OnMouseOver()
    {
        if (inHand && isPlayer && BattleController.instance.battleEnded == false && !isSelected)
            MoveToPoint(theHC.cardPositions[handPosition] + new Vector3(0f, 1f, .5f), Quaternion.identity);
    }

    private void OnMouseExit()
    {
        if (inHand && isPlayer && BattleController.instance.battleEnded == false && !isSelected)
            MoveToPoint(theHC.cardPositions[handPosition], theHC.minPos.rotation);
    }

    private void OnMouseDown()
    {
        if (inHand && BattleController.instance.currentPhase == BattleController.TurnOrder.playerActive && isPlayer && BattleController.instance.battleEnded == false && Time.timeScale != 0f)
        {
            isSelected = true;
            AvisoZoom.instance.ShowHint();
            theCol.enabled = false;
            justPressed = true;
        }
    }

    public void ReturnToHand()
    {
        isSelected = false;
        theCol.enabled = true;
        MoveToPoint(theHC.cardPositions[handPosition], theHC.minPos.rotation);
        AvisoZoom.instance.HideHint();
    }

    public void DamageCard(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            assignedPlace.activeCard = null;
            MoveToPoint(BattleController.instance.discardPoint.position, BattleController.instance.discardPoint.rotation);
            anim.SetTrigger("Jump");
            Destroy(gameObject, 5f);
            AudioManager.instance.PlaySFX(2);
        }
        else
        {
            AudioManager.instance.PlaySFX(1);
        }

        anim.SetTrigger("Hurt");
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        healthText.text = currentHealth.ToString();
        attackText.text = attackPower.ToString();
    }
}