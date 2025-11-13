using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Info")]
public class WeaponInfo : ScriptableObject {
    [Header("Info")]
    public string weaponname;
    public int cur_Ammo;
    public int cur_Carryng;
    public weaponType type;
}

public enum weaponType
{
    melee, heavy, small
}
