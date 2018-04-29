using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	

	public float fullHealth = 100f;

	public enum UnitType {Player,Mob};
	public UnitType unitType;

	public float currentHealth;

	private GameObject unit;
	private Player player;
	private UIPlayerStatusBar uiPlayerStatusBar;
	//private UI
	//private	

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<UIPlayerStatusBar>()) {
			uiPlayerStatusBar = GameObject.FindObjectOfType<UIPlayerStatusBar> ();
		}else {Debug.LogWarning ( name + ", missing UIHealth!");}

		if (unitType == UnitType.Player) {
			if (GetComponent<Player> ()) {
				player = GetComponent<Player> ();
			} else {Debug.LogWarning (name + ", missing player or setting the wrong type.");}
		}else if (unitType == UnitType.Mob) {
			// mob = GetComponent<Mob> ();
		}
		currentHealth = fullHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			if (unitType==UnitType.Player) {
				PlayerDie ();
			}else if (unitType == UnitType.Mob) {
				MobDie ();
			}

		}
	}

	void PlayerDie ()
	{
		player.reSpawn = true;
		currentHealth = fullHealth;
		UpdateUIPlayerHealthBar ();
		Debug.Log ("Respawn");
	}

	void MobDie (){
		// How mob do on death.
		GetComponent<MobAttack>().MobDies();
		EnemiesSpawn.enemiesSpawnedCount--;
	}

	public void TakeDamage(float damage){
		currentHealth = Mathf.Clamp(currentHealth - damage , 0 , fullHealth);
		if (unitType == UnitType.Mob) {
			GetComponent<MobAttack> ().MobAggro();
		}else if (unitType == UnitType.Player) {
			UpdateUIPlayerHealthBar ();
		}
	}

	public void TakeHeal(float heal){
		if (currentHealth < fullHealth) {
			if (unitType == UnitType.Player) {
				currentHealth = Mathf.Clamp(currentHealth + heal , 0 , fullHealth);
				UpdateUIPlayerHealthBar ();
			}else if (unitType == UnitType.Mob) {
				fullHealth += heal;
				currentHealth = Mathf.Clamp(currentHealth + heal , 0 , fullHealth);
			}
		}

	}

	public void UpdateUIPlayerHealthBar ()
	{
		uiPlayerStatusBar.UpdateHealthBar (currentHealth / fullHealth);
	}

}
