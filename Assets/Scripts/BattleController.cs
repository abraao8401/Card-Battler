using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;

    private void Awake()
    {
        instance = this;
    }

    public int startingMana = 5, maxMana = 10;
    public int playerMana, enemyMana;
    private int currentPlayerMaxMana, currentEnemyMaxMana;

    public int startingCardsAmount = 5;
    public int cardsToDrawPerTurn = 1;

    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;

    public Transform discardPoint;

    public int playerHealth, enemyHealth;

    public bool battleEnded;

    public float resultScreenDelayTime = 1f;

    [Range(0f, 1f)]
    public float playerFirstChance = .5f;

    private bool playerDidFirstTurn = false;
    private bool enemyDidFirstTurn = false;

    void Start()
    {
        currentPlayerMaxMana = startingMana;
        FillPlayerMana();

        DeckController.instance.DrawMultipleCards(startingCardsAmount);

        UIController.instance.SetPlayerHealthText(playerHealth);
        UIController.instance.SetEnemyHealthText(enemyHealth);

        currentEnemyMaxMana = startingMana;
        FillEnemyMana();

        if (Random.value > playerFirstChance)
        {
            currentPhase = TurnOrder.playerCardAttacks;
            AdvanceTurn();
        }

        AudioManager.instance.PlayBGM();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AdvanceTurn();
        }
    }

    public void SpendPlayerMana(int amountToSpend)
    {
        playerMana -= amountToSpend;

        if (playerMana < 0)
        {
            playerMana = 0;
        }

        UIController.instance.SetPlayerManaText(playerMana);
    }

    public void FillPlayerMana()
    {
        playerMana = currentPlayerMaxMana;
        UIController.instance.SetPlayerManaText(playerMana);
    }

    public void SpendEnemyMana(int amountToSpend)
    {
        enemyMana -= amountToSpend;

        if (enemyMana < 0)
        {
            enemyMana = 0;
        }

        UIController.instance.SetEnemyManaText(enemyMana);
    }

    public void FillEnemyMana()
    {
        enemyMana = currentEnemyMaxMana;
        UIController.instance.SetEnemyManaText(enemyMana);
    }

    public void AdvanceTurn()
    {
        if (battleEnded == false)
        {
            currentPhase++;

            if ((int)currentPhase >= System.Enum.GetValues(typeof(TurnOrder)).Length)
            {
                currentPhase = 0;
            }

            switch (currentPhase)
            {
                case TurnOrder.playerActive:

                    UIController.instance.endTurnButton.SetActive(true);
                    UIController.instance.drawCardButton.SetActive(true);

                    if (playerDidFirstTurn)
                    {
                        if (playerMana < maxMana)
                        {
                            playerMana++;
                            UIController.instance.SetPlayerManaText(playerMana);
                        }

                        DeckController.instance.DrawMultipleCards(cardsToDrawPerTurn);
                    }

                    break;

                case TurnOrder.playerCardAttacks:

                    if (!playerDidFirstTurn)
                    {
                        playerDidFirstTurn = true;
                        AdvanceTurn();
                    }
                    else
                    {
                        CardPointsController.instance.PlayerAttack();
                    }

                    break;

case TurnOrder.enemyActive:

    if (!enemyDidFirstTurn)
    {
        enemyDidFirstTurn = true;
        EnemyController.instance.StartAction();
    }
    else
    {
        if (enemyMana < maxMana)
        {
            enemyMana++;
            UIController.instance.SetEnemyManaText(enemyMana);
        }

        while (enemyMana >= 2)
        {
            EnemyController.instance.SpawnCardInHand();
            enemyMana -= 2;
            UIController.instance.SetEnemyManaText(enemyMana);
        }

        EnemyController.instance.StartAction();
    }

    break;
            }
        }
    }

    public void EndPlayerTurn()
    {
        UIController.instance.endTurnButton.SetActive(false);
        UIController.instance.drawCardButton.SetActive(false);

        AdvanceTurn();
    }

    public void DamagePlayer(int damageAmount)
    {
        if (playerHealth > 0 || !battleEnded)
        {
            playerHealth -= damageAmount;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                EndBattle();
            }

            UIController.instance.SetPlayerHealthText(playerHealth);

            UIDamageIndicator damageClone = Instantiate(UIController.instance.playerDamage, UIController.instance.playerDamage.transform.parent);
            damageClone.damageText.text = damageAmount.ToString();
            damageClone.gameObject.SetActive(true);

            AudioManager.instance.PlaySFX(6);
        }
    }

    public void DamageEnemy(int damageAmount)
    {
        if (enemyHealth > 0 || battleEnded == false)
        {
            enemyHealth -= damageAmount;

            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                EndBattle();
            }

            UIController.instance.SetEnemyHealthText(enemyHealth);

            UIDamageIndicator damageClone = Instantiate(UIController.instance.enemyDamage, UIController.instance.enemyDamage.transform.parent);
            damageClone.damageText.text = damageAmount.ToString();
            damageClone.gameObject.SetActive(true);

            AudioManager.instance.PlaySFX(5);
        }
    }

    void EndBattle()
    {
        battleEnded = true;

        HandController.instance.EmptyHand();

        if (enemyHealth <= 0)
        {
            UIController.instance.battleResultText.text = "VOCÊ VENCEU!";

            foreach (CardPlacePoint point in CardPointsController.instance.enemyCardPoints)
            {
                if (point.activeCard != null)
                {
                    point.activeCard.MoveToPoint(discardPoint.position, point.activeCard.transform.rotation);
                }
            }

        }
        else
        {
            UIController.instance.battleResultText.text = "VOCÊ PERDEU!";

            foreach (CardPlacePoint point in CardPointsController.instance.playerCardPoints)
            {
                if (point.activeCard != null)
                {
                    point.activeCard.MoveToPoint(discardPoint.position, point.activeCard.transform.rotation);
                }
            }
        }

        StartCoroutine(ShowResultCo());
    }

    IEnumerator ShowResultCo()
    {
        yield return new WaitForSeconds(resultScreenDelayTime);
        UIController.instance.battleEndScreen.SetActive(true);
    }
}
