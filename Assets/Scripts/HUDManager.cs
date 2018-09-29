using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public RectTransform energyBar;
    public Text energy;
    public Text time;
    public PlayerController player;
    
	// Update is called once per frame
	void Update () {
        energyBar.localScale = new Vector3(player.getEnergyLevel()/100f, energyBar.localScale.y, energyBar.localScale.z);
        time.text = GameManager.instance.getHour().ToString("00") + ":" + GameManager.instance.getMinute().ToString("00");
        energy.text = player.getEnergyLevel().ToString("000");
    }

}
