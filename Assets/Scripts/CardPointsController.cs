using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPointsController : MonoBehaviour
{
    public static CardPointsController instance;

    private void Awake()
    {
        instance = this;
    }

    public CardPlacePoint[] playerCardPoints, enemyCardPoints;

    public float timeBetweenAttacks = .25f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackCo());
    }

    IEnumerator PlayerAttackCo()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        for(int i = 0; i < playerCardPoints.Length; i++)
        {
            if(playerCardPoints[i].activeCard != null)
            {
                if(enemyCardPoints[i].activeCard != null)
                {
                    // Cálculo de dano com vantagem elemental
                    int damage = CalculateDamage(playerCardPoints[i].activeCard.cardSO, enemyCardPoints[i].activeCard.cardSO);
                    enemyCardPoints[i].activeCard.DamageCard(damage);
                } 
                else
                {
                    // Ataque direto à vida do inimigo
                    BattleController.instance.DamageEnemy(playerCardPoints[i].activeCard.attackPower);
                }

                playerCardPoints[i].activeCard.anim.SetTrigger("Attack");

                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            if(BattleController.instance.battleEnded == true)
            {
                i = playerCardPoints.Length;
            }
        }

        CheckAssignedCards();

        BattleController.instance.AdvanceTurn();
    }

    public void EnemyAttack()
    {
        StartCoroutine(EnemyAttackCo());
    }

    IEnumerator EnemyAttackCo()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        for (int i = 0; i < enemyCardPoints.Length; i++)
        {
            if (enemyCardPoints[i].activeCard != null)
            {
                if (playerCardPoints[i].activeCard != null)
                {
                    // Cálculo de dano com vantagem elemental
                    int damage = CalculateDamage(enemyCardPoints[i].activeCard.cardSO, playerCardPoints[i].activeCard.cardSO);
                    playerCardPoints[i].activeCard.DamageCard(damage);
                }
                else
                {
                    // Ataque direto à vida do jogador
                    BattleController.instance.DamagePlayer(enemyCardPoints[i].activeCard.attackPower);
                }

                enemyCardPoints[i].activeCard.anim.SetTrigger("Attack");

                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            if (BattleController.instance.battleEnded == true)
            {
                i = enemyCardPoints.Length;
            }
        }

        CheckAssignedCards();

        BattleController.instance.AdvanceTurn();
    }

    public void CheckAssignedCards()
    {
        foreach(CardPlacePoint point in enemyCardPoints)
        {
            if(point.activeCard != null)
            {
                if(point.activeCard.currentHealth <= 0)
                {
                    point.activeCard = null;
                }
            }
        }

        foreach (CardPlacePoint point in playerCardPoints)
        {
            if (point.activeCard != null)
            {
                if (point.activeCard.currentHealth <= 0)
                {
                    point.activeCard = null;
                }
            }
        }
    }

    // Novo método para aplicar vantagem elemental
    private int CalculateDamage(CardScriptableObject attacker, CardScriptableObject defender)
    {
        int damage = attacker.attackPower;

        if (!string.IsNullOrEmpty(attacker.advantage) && !string.IsNullOrEmpty(defender.element))
        {
            if (attacker.advantage.ToLower() == defender.element.ToLower())
            {
                damage += 2; // bônus de vantagem
            }
        }

        return damage;
    }
}