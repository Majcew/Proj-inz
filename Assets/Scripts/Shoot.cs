using UnityEngine;

public class Shoot : MonoBehaviour
{
    private int id;
    public Camera fpscam;
    public float fireRate = 0.1f;
    public float damage = 10f;
    float fireTimer;
    private Ammunition ammoInfo;
    public AudioSource audiosource;
    public AudioClip shootingsound;
    private void Awake()
    {
        ammoInfo = GetComponentInParent<Ammunition>();
        id = GetCurrentId();
    }
    private void OnEnable()
    {
        id = GetCurrentId();
        ShowAmmoInMag();
        ShowAmmoLeft();
    }
    private int GetCurrentId()
    {
        switch (gameObject.tag)
        {
            case "ak47":
                id = 0;
                break;
            case "revolver":
                id = 1;
                break;
            case "shotgun":
                id = 2;
                break;
            default:
                id = -1;
                break;
        }
        return id;
    }

    private void ShowAmmoLeft()
    {
        ammoInfo.ShowAmmoLeft(id);
    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            ShootBullet();
            fireTimer = 0;
        }
        if (Input.GetKey("r") && ammoInfo.bulletsInMag[id] < ammoInfo.bulletsPerMag[id])
        {
            ReloadMag();
        }
    }


    private void ShootBullet()
    {
        if (ammoInfo.bulletsInMag[id] != 0)
        {
            ammoInfo.bulletsInMag[id]--;
            audiosource.PlayOneShot(shootingsound);
            RaycastHit hit;
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                Debug.Log(hit.transform.name);
            }
            ShowAmmoInMag();
        }
    }
    private void ReloadMag()
    {
        int amountToReload = ammoInfo.bulletsPerMag[id] - ammoInfo.bulletsInMag[id];
        if (ammoInfo.bulletsLeft[id] <= amountToReload && ammoInfo.bulletsLeft[id] != 0)
        {
            ammoInfo.bulletsInMag[id] += ammoInfo.bulletsLeft[id];
            ammoInfo.bulletsLeft[id] = 0;
            ShowAmmoInMag();
            ShowAmmoLeft();
        }
        else if (ammoInfo.bulletsLeft[id] != 0)
        {
            ammoInfo.bulletsInMag[id] += amountToReload;
            ammoInfo.bulletsLeft[id] -= amountToReload;
            ShowAmmoInMag();
            ShowAmmoLeft();
        }
    }

    private void ShowAmmoInMag()
    {
        ammoInfo.ShowAmmoInMag(id);
    }
}