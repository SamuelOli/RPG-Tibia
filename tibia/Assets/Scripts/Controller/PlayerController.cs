using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	Joystick joystick;
	private float jH = 0, jV = 0; //jH -> X      jV -> Y

	PlayerStats playerStats;

	private Rigidbody2D rb2D;
	float sizeJoystick = 0;


	void Start()
	{
		playerStats = GetComponent<PlayerStats>();
		rb2D = GetComponent<Rigidbody2D>();


	}

	void Update()
	{
		
		if (joystick.Horizontal != 0 || joystick.Vertical != 0)			  //Verifica se o Joystick está sendo usado
		{
			Move();														//Movimentação
		}
		else
		{
			rb2D.velocity = new Vector2(0, 0);						//Freia o Player ao soltar o Joystick
		}
	}


	
	public void Move()
	{
		if (joystick.Horizontal > 0.2f)								//Verifica se vai para direita
		{
			jH = 1;
		}
		else if (joystick.Horizontal < -0.2f)					//Verifica se vai para esquerda
		{
			jH = -1;
		}
		else  												//Sem movimentação horizontal
		{
			jH = 0;
		}

		if (joystick.Vertical > 0.2f)					//Verifica se vai subir
		{
			jV = 1;
		}
		else if (joystick.Vertical < -0.2f)			//Verifica se vai descer
		{
			jV = -1;
		}
		else 									//Sem Movimentação Vertical
		{
			jV = 0;
		}

		rb2D.velocity = new Vector2(jH * playerStats.speed.Value * Time.deltaTime, jV * playerStats.speed.Value * Time.deltaTime);   //Executa a movimentação
	}


	public void SetJoystick(Joystick newJoystick)
	{
		joystick = newJoystick;
	}



}