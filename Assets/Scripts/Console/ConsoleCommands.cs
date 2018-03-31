using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DevConsole;

public class ConsoleCommands : MonoBehaviour {

	void Start() {
        Console.AddCommand(new Command<string>("weapon_clip", SetAmmoInClip));
        Console.AddCommand(new Command<string>("weapon_ammo", SetWeaponAmmo));
        Console.AddCommand(new Command<string>("time_scale", SetTimeScale));
    }
    static void SetAmmoInClip(string sValue) {
        if(sValue == "") {
            return;
        }
        int iValue;
        if(int.TryParse(sValue, out iValue)) {
            Console.Log("Change successful", Color.green);
        } else
            Console.LogError("The entered value is not a valid int value");
    }
    static void SetWeaponAmmo(string sValue) {
        if(sValue == "") {
            return;
        }
        int iValue;
        if(int.TryParse(sValue, out iValue)) {
            Console.Log("Change successful", Color.green);
        } else
            Console.LogError("The entered value is not a valid int value");
    }
    static void SetTimeScale(string sValue) {
        if(sValue == "") {
            Console.LogInfo(Time.timeScale);
            return;
        }
        float fValue;
        if(float.TryParse(sValue, out fValue)) {
            Time.timeScale = fValue;
            Console.Log("Change successful", Color.green);
        } else
            Console.LogError("The entered value is not a valid float value");
    }
}