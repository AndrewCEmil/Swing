using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Level {
	public int level;
	public string name;
	public bool locked;
	public bool completed;
	public int[] preReqs;
}