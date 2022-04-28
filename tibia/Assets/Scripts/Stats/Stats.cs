using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
	public Hero hero;

	public BasicAttribute ad;
	public BasicAttribute ap;
	public BasicAttribute armor;
	public BasicAttribute armorPenetration;
	public BasicAttribute attackSpeed;
	public BasicAttribute criticalDamage;
	public BasicAttribute criticalRate;
	public BasicAttribute hp;
	public BasicAttribute hpRegen;
	public BasicAttribute magicResist;
	public BasicAttribute magicPenetration;
	public BasicAttribute mana;
	public BasicAttribute manaRegen;
	public BasicAttribute range;
	public BasicAttribute speed;

	public BasicAttribute xpMultiplier;

	public int lvl = 1;
	public float currentXP = 0;
	float xpNextLvl = 1000;
	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void AddBasicAttributeHero (float mult) {
		//Debug.Log(hero.heroAttributes.listAttributes.Length + "   " + hero.heroAttributes.listAttributes[0].Value);
		ad.Value += hero.heroAttributes.GetBasicAttribute (ad.MyAttribute.AttributeName) * mult;
		ap.Value += hero.heroAttributes.GetBasicAttribute (ap.MyAttribute.AttributeName) * mult;
		armor.Value += hero.heroAttributes.GetBasicAttribute (armor.MyAttribute.AttributeName) * mult;
		armorPenetration.Value += hero.heroAttributes.GetBasicAttribute (armorPenetration.MyAttribute.AttributeName) * mult;
		attackSpeed.Value += hero.heroAttributes.GetBasicAttribute (attackSpeed.MyAttribute.AttributeName) * mult;
		criticalDamage.Value += hero.heroAttributes.GetBasicAttribute (criticalDamage.MyAttribute.AttributeName) * mult;
		criticalRate.Value += hero.heroAttributes.GetBasicAttribute (criticalRate.MyAttribute.AttributeName) * mult;
		hp.Value += hero.heroAttributes.GetBasicAttribute (hp.MyAttribute.AttributeName) * mult;
		hpRegen.Value += hero.heroAttributes.GetBasicAttribute (hpRegen.MyAttribute.AttributeName) * mult;
		magicResist.Value += hero.heroAttributes.GetBasicAttribute (magicResist.MyAttribute.AttributeName) * mult;
		magicPenetration.Value += hero.heroAttributes.GetBasicAttribute (magicPenetration.MyAttribute.AttributeName) * mult;
		mana.Value += hero.heroAttributes.GetBasicAttribute (mana.MyAttribute.AttributeName) * mult;
		manaRegen.Value += hero.heroAttributes.GetBasicAttribute (manaRegen.MyAttribute.AttributeName) * mult;
		range.Value += hero.heroAttributes.GetBasicAttribute (range.MyAttribute.AttributeName) * mult;
		speed.Value += hero.heroAttributes.GetBasicAttribute (speed.MyAttribute.AttributeName) * mult;
		xpMultiplier.Value += 1;
	}

	public void AddAttribute (BasicAttribute attribute) {
		AddAttribute (attribute, attribute.Value, attribute.ValueMultiplier);
	}

	public void AddAttribute (BasicAttribute attribute, float value) {
		AddAttribute (attribute, value, attribute.ValueMultiplier);
	}

	public void AddAttribute (BasicAttribute attribute, float value, float valueMultiplier) {
		if (attribute.MyAttribute.AttributeName.Equals (ad.MyAttribute.AttributeName)) {
			ad.Value += value;
			ad.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (ap.MyAttribute.AttributeName)) {
			ap.Value += value;
			ap.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (armor.MyAttribute.AttributeName)) {
			armor.Value += value;
			armor.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (armorPenetration.MyAttribute.AttributeName)) {
			armorPenetration.Value += value;
			armorPenetration.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (attackSpeed.MyAttribute.AttributeName)) {
			attackSpeed.Value += value;
			attackSpeed.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (criticalDamage.MyAttribute.AttributeName)) {
			criticalDamage.Value += value;
			criticalDamage.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (criticalRate.MyAttribute.AttributeName)) {
			criticalRate.Value += value;
			criticalRate.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (hp.MyAttribute.AttributeName)) {
			hp.Value += value;
			hp.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (hpRegen.MyAttribute.AttributeName)) {
			hpRegen.Value += value;
			hpRegen.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (magicResist.MyAttribute.AttributeName)) {
			magicResist.Value += value;
			magicResist.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (magicPenetration.MyAttribute.AttributeName)) {
			magicPenetration.Value += value;
			magicPenetration.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (mana.MyAttribute.AttributeName)) {
			mana.Value += value;
			mana.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (manaRegen.MyAttribute.AttributeName)) {
			manaRegen.Value += value;
			manaRegen.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (range.MyAttribute.AttributeName)) {
			range.Value += value;
			range.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (speed.MyAttribute.AttributeName)) {
			speed.Value += value;
			speed.ValueMultiplier += valueMultiplier;
		} else if (attribute.MyAttribute.AttributeName.Equals (xpMultiplier.MyAttribute.AttributeName)) {
			xpMultiplier.Value += value;
			xpMultiplier.ValueMultiplier += valueMultiplier;
		}
	}
	public void RemoveAttribute (BasicAttribute attribute, float value) {
		value *= -1;
		AddAttribute (attribute, value);
	}
	public void RemoveAttribute (BasicAttribute attribute) {
		float value = -1 * attribute.Value;
		AddAttribute (attribute, value);
	}

	public void AddAttributeWhenUpgrade () {
		float incrementAd = 0;
		float incrementAP = 0;
		float incrementArmor = 0;
		float incrementMagicResist = 0;
		float incrementHP = 0;
		float incrementMana = 0;
		float incrementSpeed = 1;
		float incrementAttakSpeed = 0;

		if (hero.heroType == HeroType.Mage || hero.heroType == HeroType.Druid) {
			incrementAd = 1;
			incrementAP = 3;
			incrementHP = 35;
			incrementMana = 50;
			incrementArmor = 1;
			incrementMagicResist = 1;
			incrementAttakSpeed = 0.005f;
		} else if (hero.heroType == HeroType.Archer) {
			incrementAd = 3;
			incrementAP = 0;
			incrementHP = 50;
			incrementMana = 20;
			incrementArmor = 1;
			incrementMagicResist = 1;
			incrementAttakSpeed = 0.01f;
		} else if (hero.heroType == HeroType.Warrior) {
			incrementAd = 2;
			incrementAP = 0;
			incrementHP = 75;
			incrementMana = 20;
			incrementArmor = 2;
			incrementMagicResist = 2;
			incrementAttakSpeed = 0.005f;
		} else if (hero.heroType == HeroType.Tank) {
			incrementAd = 1;
			incrementAP = 0;
			incrementHP = 100;
			incrementMana = 20;
			incrementArmor = 3;
			incrementMagicResist = 3;
			incrementAttakSpeed = 0.005f;
		}

		ad.Value += incrementAd;
		ap.Value += incrementAP;
		attackSpeed.Value += incrementAttakSpeed;
		armor.Value += incrementArmor;
		hp.Value += incrementHP;
		magicResist.Value += incrementMagicResist;
		mana.Value += incrementMana;
		speed.Value += incrementSpeed;
	}

	public virtual void NextLvl () {
		AddAttributeWhenUpgrade ();
		xpNextLvl = 1000 + (lvl - 1) * 1538;
		lvl++;

		GetComponent<CharacterCombat> ().WhenUpgrade ();
	}

	public void AddXp (float xp) {
		currentXP += xp * xpMultiplier.Value;
		if (currentXP >= xpNextLvl) {
			xp += currentXP - xpNextLvl;
			currentXP -= xpNextLvl;
			NextLvl ();
			AddXp (xp / xpMultiplier.Value);
		}
	}

	public float DropXp (GameObject enemy) {
		Stats enemyStats = enemy.GetComponent<Stats> ();

		float xp = ((lvl / enemyStats.lvl) * 637) + currentXP;
		currentXP = 0;
		return xp;
	}
}

public enum TypeDamage {
	AD = 0,
	Critical = 1,
	AP = 2
}