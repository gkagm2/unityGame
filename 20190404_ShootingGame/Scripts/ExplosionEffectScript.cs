﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3.0f);
	}
}