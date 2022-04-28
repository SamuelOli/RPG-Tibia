using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Hero : ScriptableObject
{
	public string heroName;
	[Header("Graph")]
	public Sprite heroIcon;

	[Header("Hero Type")]
	public HeroType heroType;
	[Header("Basic Attributes")]
	public ListBasicAttribute heroAttributes;
	[Header("Hero Skills")]
	public List<Skill> heroSkills;

}

public enum HeroType
{
	Mage = 0,
	Archer = 1,
	Warrior = 2,
	Tank = 3,
	Druid = 4,
	Any = 5
}
