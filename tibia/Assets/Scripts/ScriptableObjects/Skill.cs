using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Skill : ScriptableObject
{
	public string skilllName;

	[Header("Graph")]
	public Sprite skillIcon;
	public GameObject skillPrefs;

	[Header("Basic Attribute")]
	public TypeDamage typeDamage;
	public float basicDamage;
	[Header("1 = 100%")]
	public float scaleAD;
	public float scaleAP;
	public float scaleArmor;
	public float scaleMagicResist;
	public float scaleHP;
	public float scaleMana;
	float totalDamage = 0;

	[Header("Required")]
	public int lvlRequired;
	public int mana;
	public float cooldown;
	float currentCooldown = 0;

	public float maxDistance;

	[Header("Skill Type")]
	public SkillType skillType;

	Canvas canvas;


	public void Cooldown()
	{
		if (currentCooldown > 0)
		{
			currentCooldown -= Time.deltaTime;
		}
	}

	public float GetCurrentCoolDown()
	{
		return currentCooldown;
	}

	public void CastSkill(GameObject caster, GameObject target, Canvas canvas2)
	{
		canvas = canvas2;

		CharacterCombat myCombat = caster.GetComponent<CharacterCombat>();

		totalDamage = basicDamage;
		totalDamage += myCombat.myStats.ad.Value * scaleAD;
		totalDamage += myCombat.myStats.ap.Value * scaleAP;
		totalDamage += myCombat.myStats.armor.Value * scaleArmor;
		totalDamage += myCombat.myStats.hp.Value * scaleHP;
		totalDamage += myCombat.myStats.mana.Value * scaleMana; 
		totalDamage += myCombat.myStats.magicResist.Value * scaleMagicResist;

		if (caster.GetComponent<Stats>().lvl >= lvlRequired && myCombat.currentMana >= mana && currentCooldown <= 0)
		{
			currentCooldown = cooldown;
			myCombat.currentMana -= mana;

			switch (skilllName)
			{
				case "FireBall":
					FireBall(skillPrefs, caster, target);
					break;
				case "IceBall":
					IceBall(skillPrefs, caster, target);
					break;
				case "Punch":
					Punch(caster, target);
					break;
			}
		}
	}

	public void FireBall(GameObject pref, GameObject caster, GameObject target)
	{
		GameObject skillPref = Instantiate(pref);
		skillPref.transform.SetParent(canvas.transform);
		skillPref.transform.position = caster.transform.position;
		skillPref.GetComponent<SkillController>().StartSkillController(totalDamage, typeDamage, this, caster, target, 1000);
	}

	public void IceBall(GameObject pref, GameObject caster, GameObject target)
	{
		GameObject skillPref = Instantiate(pref);
		skillPref.transform.SetParent(canvas.transform);
		skillPref.transform.position = caster.transform.position;
		skillPref.GetComponent<SkillController>().StartSkillController(totalDamage, typeDamage, this, caster, target, 1000);
	}

	public void Punch(GameObject caster, GameObject target)
	{
		if (Vector2.Distance(caster.transform.position, target.transform.position) <= maxDistance)
		{
			CharacterCombat targetCombat = target.GetComponent<CharacterCombat>();
			CharacterCombat casterCombat = caster.GetComponent<CharacterCombat>();

			casterCombat.CaculateCritical(targetCombat.CalculateDamage(typeDamage, casterCombat, totalDamage));
		}
	}

}

public enum SkillType
{
	MeleeAttack = 0,
	Cast = 1,
	Buff = 2,
	Summon = 3,
	Defense = 4, 
	Heal = 5
}
