using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{
	public CharacterCombat enemyCombat = null;

	public Stats myStats = null;
	public SkillsManager mySkills;

	public float maxHP;
	public float currentHP;
	public GameObject uiHP;

	public float maxMana;
	public float currentMana;

	float currentAttackSpeed;

	float distance = 0;

	public bool inCombat = false;
	public float timeOutCombat = 0;

	public Text spriteDamage;
	public Canvas canvasGame;
    public Canvas canvasbackgroungGame;

    // Start is called before the first frame update
    public virtual void Start()
    {
		AttStatsCombat();
    }

    // Update is called once per frame
	public virtual void Update()
    {
		if (maxHP <= 0 || maxMana <= 0)
		{
			AttStatsCombat();
		}
		UiHP();
		currentAttackSpeed -= Time.deltaTime;
		if (currentAttackSpeed <= 0 && enemyCombat != null )
		{
			distance = Vector2.Distance(transform.position, enemyCombat.transform.position);
			if (myStats.range.Value >= (distance))
			{
				AutoAttack();
				currentAttackSpeed = 1 / myStats.attackSpeed.Value;
			}
		}


		Regen();
    }

	public void WhenUpgrade()
	{
		myStats = GetComponent<Stats>();
		currentHP = maxHP = myStats.hp.Value;
		currentMana = maxMana = myStats.mana.Value;
		currentAttackSpeed = myStats.attackSpeed.Value;
	}

	public void AttStatsCombat()
	{
		mySkills = GetComponent<SkillsManager>();
		myStats = GetComponent<Stats>();

		float lostHP = 0;
		float lostMana = 0;

		if (maxHP > 0 || maxMana >= 0)
		{
			lostHP = maxHP - currentHP;
			lostMana = maxMana - currentMana;
		}

		currentHP = maxHP = myStats.hp.Value;
		currentMana = maxMana = myStats.mana.Value;
		currentAttackSpeed = 1 / myStats.attackSpeed.Value;

		currentHP -= lostHP;
		currentMana -= lostMana;
	}

	public void Regen()                         //Verifica se está em combat, se não estiver regenera HP
	{
		if (inCombat)
		{
			timeOutCombat += Time.deltaTime;
			if (timeOutCombat >= 5)
			{
				inCombat = false;
				timeOutCombat = 0;
			}
		}
		else
		{
			currentHP += myStats.hpRegen.Value * myStats.hp.Value * Time.deltaTime;

			if (currentHP > myStats.hp.Value)
			{
				currentHP = myStats.hp.Value;
			}
		}

		currentMana += myStats.manaRegen.Value * myStats.mana.Value * Time.deltaTime;
		if (currentMana > myStats.mana.Value)
		{
			currentMana = myStats.mana.Value;
		}

	}
	public void SelectEnemy(CharacterCombat enemy)
	{
		enemyCombat = enemy;
	}

	public CharacterCombat GetEnemy()
	{
		return enemyCombat;
	}

	public void AutoAttack()
	{
		float myAD = myStats.ad.Value;

		CaculateCritical(enemyCombat.CalculateDamage(TypeDamage.AD, this, myAD));

			//Detect Combat 
		timeOutCombat = 0;
		inCombat = true;
	}


	public void CaculateCritical(float damage)
	{
		float random = Random.Range(0, 100);

		if (random <= myStats.criticalRate.Value && random > 0)
		{
			damage *= myStats.criticalDamage.Value;

			damage = Mathf.Round(damage);
			enemyCombat.InstantiateSprideDamage(damage, this, TypeDamage.Critical);
		}
		else
		{
			damage = Mathf.Round(damage);
			enemyCombat.InstantiateSprideDamage(damage, this, TypeDamage.AD);
		}

		if (damage > 0)
		{
			enemyCombat.currentHP -= damage;
			if (enemyCombat.currentHP <= 0)
			{
				enemyCombat.Die(this.gameObject);
				enemyCombat = null;
			}
		}
	}

	public float CalculateDamage(TypeDamage typeDamage, CharacterCombat enemy, float damage)
	{
		if (typeDamage == TypeDamage.AD || typeDamage == TypeDamage.Critical )
		{
			float myArmor = myStats.armor.Value - enemy.myStats.armorPenetration.Value;

			damage = damage - damage * (myArmor / (100 + myArmor));
		}
		else if (typeDamage == TypeDamage.AP)
		{
			float myMagicResist = myStats.magicResist.Value - enemy.myStats.magicPenetration.Value;

			damage = damage - damage * (myMagicResist / (100 + myMagicResist));
		}

		if (damage < 0)
		{
			damage = 0;
		}

		return Mathf.Round(damage);
	}

	public float TakeDamage(TypeDamage typeDamage, CharacterCombat enemy, float damage)
	{
		damage = CalculateDamage(typeDamage, enemy, damage);

		currentHP -= damage;
		if (currentHP <= 0)
		{
			Die(enemy.gameObject);
		}


		timeOutCombat = 0;
		inCombat = true;

		InstantiateSprideDamage(damage, enemy, typeDamage);
		return damage;
	}

	public void UiHP()
	{
		float currenUIHP = currentHP / maxHP; 
		uiHP.transform.GetChild(0).localScale = new Vector2(currenUIHP,1);
	}

	public void InstantiateSprideDamage(float damage, CharacterCombat enemy, TypeDamage typeDamage)
	{
		Text currentSpriteDamage = Instantiate(spriteDamage);
		currentSpriteDamage.text = damage.ToString();
		if (typeDamage == TypeDamage.Critical)
		{
			currentSpriteDamage.color = Color.red;
		}
		else if (typeDamage == TypeDamage.AP)
		{
			currentSpriteDamage.color = Color.cyan;
		}

		if (damage <= 0)
		{
			currentSpriteDamage.color = Color.gray;
		}
		currentSpriteDamage.transform.SetParent(canvasGame.transform);

		Vector3 spritePosition = transform.position - enemy.transform.position;
		spritePosition = spritePosition.normalized;
		currentSpriteDamage.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Round(spritePosition.x) * 3, 5);

		currentSpriteDamage.transform.position = transform.position;


	}

	public virtual void Die(GameObject enemy)
	{
		
		enemy.GetComponent<Stats>().AddXp(GetComponent<Stats>().DropXp(enemy));
	}
}
