using UnityEngine;
using UnityEngine.UI;

public class ShotgunShoot : MonoBehaviour
{
    public int bulletsPerMag = 6;
    public int bulletsLeft = 30;
    public int bulletsInMag = 6;
    public Text ammoinmagText;
    public Text overallammoText;
    public Camera fpscam;
    public float fireRate;
    public float damage = 10f;
    float fireTimer;
    public AudioSource audiosource;
    public AudioClip shootingsound;
    private void OnEnable()
    {
        ShowAmmoInMag();
        ShowAmmoLeft();
    }

    private void ShowAmmoLeft()
    {
        overallammoText.text = bulletsLeft.ToString();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            ShootBullet();
            fireTimer = 0;
        }
        if (Input.GetKey("r") && bulletsInMag < bulletsPerMag)
        {
            ReloadMag();
        }
    }


    private void ShootBullet()
    {
        if (bulletsInMag != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                audiosource.PlayOneShot(shootingsound);
                RaycastHit hit;
                Vector3 spread = new Vector3();
                Vector3 direction = fpscam.transform.forward;
                spread += fpscam.transform.up * Random.Range(-1.5f, 1.5f);
                spread += fpscam.transform.right * Random.Range(-1.5f, 1.5f);
                direction += spread.normalized * Random.Range(0f, 0.2f);
                if (Physics.Raycast(fpscam.transform.position, direction, out hit, 25.0f))
                {
                    Debug.DrawLine(fpscam.transform.position, hit.point, Color.green, 3.0f);
                    Target target = hit.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }

                    Debug.Log(hit.transform.name);
                    EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }
                }
                else Debug.DrawRay(fpscam.transform.position, direction, Color.red, 3.0f);
            }
        }
        else return;
        bulletsInMag--;
        ShowAmmoInMag();
    }
    private void ReloadMag()
    {
        int amountToReload = bulletsPerMag - bulletsInMag;
        if (bulletsLeft <= amountToReload && bulletsLeft != 0)
        {
            bulletsInMag += bulletsLeft;
            bulletsLeft = 0;
            ShowAmmoInMag();
            ShowAmmoLeft();
        }
        else if (bulletsLeft != 0)
        {
            bulletsInMag += amountToReload;
            bulletsLeft -= amountToReload;
            ShowAmmoInMag();
            ShowAmmoLeft();
        }
    }

    private void ShowAmmoInMag()
    {
        ammoinmagText.text = bulletsInMag.ToString() + "/";
    }
}
