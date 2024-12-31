using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
public int HP = 100;

public void TakeDamage(int damageAmount)
{

HP -= damageAmount;

if (HP <= 0)
{

int randomValue = Random.Range(0,2); // 0 or 1
if (randomValue == 0)
{
    print("Player Dead");
}
else{
    print("Player Hit");
}
}
}
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("ZombieHand"))
    {
        TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
    }
}
}
