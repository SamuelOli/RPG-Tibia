using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsJoystick : MonoBehaviour
{
	public Joystick variableJoystick;
	public Joystick floatJoystick;
	public GameObject panelJoystick;


	private PlayerController controller;
	public GameObject player;


	// Start is called before the first frame update
	void Start()
	{
		controller = player.GetComponent<PlayerController>();
		ChooseJoystick();
		//panelJoystick.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.3f, Screen.height * 0.3f);
		//panelJoystick.transform.position = new Vector2(2, 2);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("space"))
        {			
			PlayerPrefs.SetInt("TypeJoystick", 0);
            ChooseJoystick();
        }

		if (Input.GetKeyDown(KeyCode.A))
        {
			PlayerPrefs.SetInt("TypeJoystick", 1);
            ChooseJoystick();
        }


	}

	public void ChooseJoystick()
	{
		if (PlayerPrefs.GetInt("TypeJoystick") == 0)
		{
			variableJoystick.gameObject.SetActive(true);
			floatJoystick.gameObject.SetActive(false);

			controller.SetJoystick(variableJoystick);
			SetSizeJoystick(variableJoystick);
			SetPositionJoystick(variableJoystick);
		}
		else
		{
			variableJoystick.gameObject.SetActive(false);
			floatJoystick.gameObject.SetActive(true);

			PlayerPrefs.SetInt("TypeJoystick", 1);
			controller.SetJoystick(floatJoystick);
			SetSizeJoystick(floatJoystick);

		}
	}

	public void SetSizeJoystick(Joystick setJoystick)
	{
		float sizeJoystick = 0;

		if (Screen.height <= Screen.width)
		{
			sizeJoystick = Screen.height * .15f;
		}
		else
		{
			sizeJoystick = Screen.height * .15f;
		}
		setJoystick.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector3(sizeJoystick, sizeJoystick);
		setJoystick.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector3(sizeJoystick / 3, sizeJoystick / 3);
	}

	public void SetPositionJoystick(Joystick setJoystick)
	{
		setJoystick.transform.GetChild(0).position = new Vector2(0, 0);

	}
}