using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestMonoBehavior : MonoBehaviour
{
    public CombatManager manager;
    public Encounter encounter;

    void Start()
    {
        PlayerStats.Wipe();
        manager.Begin(encounter);
    }
}
