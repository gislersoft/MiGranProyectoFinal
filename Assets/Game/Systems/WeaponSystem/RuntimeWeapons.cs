using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Runtime Weapons")]
public class RuntimeWeapons : ScriptableObject {

    [Header("List of weapons")]
    public List<Weapon> weapons = new List<Weapon>();
    [Space(5)]

    [Header("Current Weapon")]
    public Weapon cur_Weapon;
    [Space(5)]
    public int r_curAmmo;
    public int r_curCarryng;
    public GameObject m_instance;

    public void Init()
    {
        weapons.Clear();
    }

    public void weaponToActual(Weapon w)
    {
        cur_Weapon = w;
        m_instance = w.w_prefab;

        r_curAmmo = w.w_Stats.cur_Ammo;
        r_curCarryng = w.w_Stats.cur_Carryng;
    }

    public void UnequipWeapon()
    {
        if (m_instance) {
            Destroy(m_instance);
        }
    }
}
