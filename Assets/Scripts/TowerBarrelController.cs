﻿using UnityEngine;
using System.Collections;

/**
  * Control code for barrels, or whatever fires the bullets. Generates bullets at a predetermined rate
  * (fireRate seconds between each generation) and sets the target for each bullet.
  **/
public class TowerBarrelController : MonoBehaviour {

	public GameObject bullet;
	public float fireRate;
	public Transform bulletSpawn;
	
	private float nextShot;

	void ShootAt(GameObject target) {
		if (Time.time > nextShot) {
			GameObject b = Instantiate(bullet, bulletSpawn.position, gameObject.transform.rotation) as GameObject;
			b.SendMessage("SetTarget", target);
			nextShot = Time.time + fireRate;
			audio.Play();
		}
	}
}
