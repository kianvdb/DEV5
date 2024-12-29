using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Weapon : MonoBehaviour
{
public bool isActiveWeapon;
public int WeaponDamage;


    // Shooting 
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    // Spread
    public float spreadIntensity;

    // Bullet
   public GameObject bulletPrefab;
   public Transform bulletSpawn;
   public float bulletVelocity = 30;
   public float bulletPrefabLifeTime = 3f; //SAeconds

public GameObject muzzleEffect;
internal Animator animator;

// Loading
public float reloadTime;
public int magazineSize, bulletsLeft;
public bool isReloading;
public Vector3 spawnPosition;
public Vector3 spawnRotation;

public enum WeaponModel
{
Pistol,
Rifle

}
public WeaponModel thisWeaponModel;
//UI 
public TextMeshProUGUI ammoDisplay;
   public enum ShootingMode
   {
    Single,
    Burst,
    Auto
   }
  public ShootingMode currentShootingMode;

private void Awake()
{
readyToShoot = true;
burstBulletsLeft = bulletsPerBurst;
animator = GetComponent<Animator>();

bulletsLeft = magazineSize;
}


    // Update is called once per frame
    void Update()
    {

        GetComponent<Outline>().enabled = false;

    if(isActiveWeapon)
    {
        // Empty magazine sound
if (bulletsLeft ==0 && isShooting)
{
    SoundManager.Instance.emptyMagazineSoundPistol.Play();
}


    
 if (currentShootingMode == ShootingMode.Auto)
 {
 // Holding down left mouse button
 isShooting = Input.GetKey(KeyCode.Mouse0);
 }
 else if (currentShootingMode == ShootingMode.Single ||
 currentShootingMode == ShootingMode.Burst)
 {
// Clicking left mouse
isShooting = Input.GetKeyDown(KeyCode.Mouse0);


 }

 // Reload system
if (Input.GetKeyDown(KeyCode.R) &&bulletsLeft < magazineSize && isReloading == false )
{
Reload();
}

// Reload automatically when magazine is empty
if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0)
{
// Reload();
}


 if (readyToShoot && isShooting && bulletsLeft > 0)
 {
burstBulletsLeft = bulletsPerBurst;
FireWeapon();

 }

 if (AmmoManager.Instance.ammoDisplay != null)
 {
   AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";
 }

 }}
    

private void FireWeapon(){

    bulletsLeft--;

    muzzleEffect.GetComponent<ParticleSystem>().Play();
    animator.SetTrigger("RECOIL");


SoundManager.Instance.PlayShootingSound(thisWeaponModel);
    readyToShoot = false;
    Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

//Instantiate the bullet
GameObject bullet = Instantiate (bulletPrefab, bulletSpawn.position, Quaternion.identity);



// Pointing the bullet to face the shooting direction
bullet.transform.forward = shootingDirection;

// Shoot the bullet
bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

// Destroy the bullet after some time
StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

// Checking if we are done shooting
if (allowReset)
{
Invoke("ResetShot", shootingDelay);
allowReset = false;

}

//Burst Mode
if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
{
Invoke ("FireWeapon", shootingDelay);

}

}


private void Reload()
{
        
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
        animator.SetTrigger("RELOAD");

isReloading = true;
Invoke("ReloadCompleted", reloadTime);

}
private void ReloadCompleted()
{
bulletsLeft = magazineSize;
isReloading = false;

}

private void ResetShot()
{
readyToShoot = true;
allowReset = true;

}
public Vector3 CalculateDirectionAndSpread(){
    //Shooting from the middle of the screen to check where we are pointing at
Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
RaycastHit hit;

Vector3 targetPoint;
if (Physics.Raycast(ray, out hit))
{
    // Hitting something
targetPoint = hit.point;
}
else 
{
    // Shooting at the air
       targetPoint = ray.GetPoint(100);
}

Vector3 direction = targetPoint - bulletSpawn.position;

float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity); 

// Returning the shooting direction and spread
return direction + new Vector3(x,y, 0);

}



private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
{
    yield return new WaitForSeconds(delay);
    Destroy(bullet);
}

}