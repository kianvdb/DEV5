using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackDamage : MonoBehaviour
{
public ZombieHand zombiehand;

public int zombieDamage;


private void Start()
{
    zombiehand.damage = zombieDamage;
}



}
