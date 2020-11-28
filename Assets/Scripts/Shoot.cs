using UnityEngine;
using Mirror;

public class Shoot : NetworkBehaviour
{
    private int id;
    public Camera fpscam;
    public Animator animator;
    public float[] fireRates;
    public float[] damages;
    float fireTimer;
    private Ammunition ammoInfo;
    public AudioSource[] audiosources;
    public AudioClip[] shootingsounds;
    private void Awake()
    {
        ammoInfo = GetComponent<Ammunition>();
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
        if (id == null)
        {
            id = 0;
        }
        return id;
    }
    private void ShowAmmoLeft()
    {
        ammoInfo.ShowAmmoLeft(id);
    }

    public void SetGunId(int id)
    {
        this.id = id;
        ShowAmmoInMag();
        ShowAmmoLeft();

    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer > fireRates[id])
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
        if (ammoInfo.bulletsInMag[id] != 0 || id == 0)
        {
            ammoInfo.bulletsInMag[id]--;
            audiosources[id].PlayOneShot(shootingsounds[id]);
            RaycastHit hit;
            switch (id)
            {
                case 0: // Miecz
                    if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, 0.75f))
                    {
                        Target target = hit.transform.GetComponent<Target>();
                        if (target != null)
                        {
                            target.TakeDamage(damages[id]);
                        }
                        //Health player = hit.transform.GetComponent<Health>();
                        //if (player != null) player.RpcTakeDamage(damages[id]);
                        CmdShootBullet(this.transform.name, hit.transform.name, damages[id]);
                    }
                    animator.SetTrigger("Attack");
                    break;
                case 3: // Shotgun
                    for (int i = 0; i < 8; i++)
                    {
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
                                target.TakeDamage(damages[id]);
                            }
                            //Health player = hit.transform.GetComponent<Health>();
                            //if (player != null) player.RpcTakeDamage(damages[id]);
                            CmdShootBullet(this.transform.name, hit.transform.name, damages[id]);
                        }
                        else Debug.DrawRay(fpscam.transform.position, direction, Color.red, 3.0f);
                    }
                    break;
                default:
                    if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit))
                    {
                        Target target = hit.transform.GetComponent<Target>();
                        if (target != null)
                        {
                            target.TakeDamage(damages[id]);
                        }
                        //Health player = hit.transform.GetComponent<Health>();
                        //if (player != null) player.RpcTakeDamage(damages[id]);
                        CmdShootBullet(this.transform.name, hit.transform.name, damages[id]);
                    }
                    break;
            }

            ShowAmmoInMag();
        }
    }

    [Command]
    private void CmdShootBullet(string shooter_name, string target_name, float damage)
    {
        Debug.Log(shooter_name + " trafil " + target_name + " i zabral " + damage.ToString() + "dm");
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