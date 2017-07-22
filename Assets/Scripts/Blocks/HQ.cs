using UnityEngine;
using System.Collections;

public class HQ : MonoBehaviour {
	public float currentGold;
	public float currentGoldIncome;

	public int CurrentGoldInt {
		get { return Mathf.RoundToInt (currentGold); }
	}

	void Start () {
	
	}

	void Update () {
		currentGold += currentGoldIncome * Time.deltaTime;
	}
}
