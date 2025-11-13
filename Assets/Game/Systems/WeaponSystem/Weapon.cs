using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon")]
public class Weapon : ScriptableObject {

    [Header("Info")]
    public string w_Name;
    public weaponType type;
    [Space(5)]

    public weaponStats w_Stats;
    [Space(5)]

    [Header("Models")]
    public GameObject w_prefab;

    [System.Serializable]
    public class weaponStats
    {
        public int cur_Ammo;
        public int cur_Carryng;
        public int cur_damage;
        public int mod_damage;
        public int cur_spread;
        public int FireRate;
    }

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        w_Stats = new weaponStats();
    }
}
