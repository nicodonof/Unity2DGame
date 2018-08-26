using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStorage {
	public static int coins;

	public static int Coins{
		get{
			return coins;
		} 
		set {
			coins = value;
		}
	}
}
