using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	public static MoneyManager Instance;
	public int initialAmount = 250;
	int currentAmount;
	public static MoneyManager instance
	{
		get
		{
			if (Instance == null)
			{
				Instance = UnityEngine.Object.FindObjectOfType<MoneyManager>();
				Object.DontDestroyOnLoad(Instance.gameObject);
			}
			return instance;
		}
	}
	private void OnEnable()
	{
		InitialMoney();
	}
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Object.DontDestroyOnLoad(this);
		}
		else if (this != Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
	public void AddMoney(int amount)
	{
		currentAmount = PlayerPrefs.GetInt(PlayerPrefsHandler.money);
		currentAmount += amount;
		PlayerPrefs.SetInt(PlayerPrefsHandler.money, currentAmount);
	}
	public void SubtractMoney(int amount)
	{
		currentAmount = PlayerPrefs.GetInt(PlayerPrefsHandler.money);
		if (amount <= currentAmount)
		{
			currentAmount -= amount;
			PlayerPrefs.SetInt(PlayerPrefsHandler.money, currentAmount);
		}
		else
		{
			// Toast Show
		}
	}
	void InitialMoney()
	{
		if (!(PlayerPrefs.HasKey(PlayerPrefsHandler.money)))
		{
			PlayerPrefs.SetInt(PlayerPrefsHandler.money, initialAmount);
		}
	}
}