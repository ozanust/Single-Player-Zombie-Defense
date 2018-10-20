using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

	public enum GunType
	{
		Pistol = 0,
		Sniper = 1
	}

	public enum BulletType
	{
		Red = 0,
		Green = 1,
		Blue = 2
	}

	[SerializeField]
	private int m_health = 100;

	[SerializeField]
	private int m_power;

	[SerializeField]
	private GunType m_gunType;

	[SerializeField]
	private BulletType m_bulletType;

	public int Health {
		get{ return m_health; }
		set{ m_health = value; }
	}

	void Shoot ()
	{

	}
}
