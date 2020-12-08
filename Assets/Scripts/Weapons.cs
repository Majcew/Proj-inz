using UnityEngine;
using Mirror;

public class Weapons : NetworkBehaviour
{
    [Header("Add the weapons")]
    [SerializeField]
    private GameObject[] weaponobj;
    [SerializeField]
    private GameObject[] modelwep;
    [Header("Add the shot script")]
    [SerializeField]
    private Shoot shootScriptObj;
    [Header("The state of the weapons")]
    [SerializeField]
    private bool[] isActive;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponobj[0].SetActive(true);
        modelwep[0].SetActive(true);
        isActive[0] = true;
        shootScriptObj.SetGunId(0);

        if (isLocalPlayer){
            //Transform[] weapons = this.GetComponentInChildren<Camera>().GetComponentsInChildren<Transform>();
           
            foreach(GameObject o in modelwep){
                setTags(o, "OwnModelWepon");
            }
        }
        else{
            foreach(GameObject o in weaponobj){
                setTags(o, "OwnModelWepon");
            }
              //CmdSetEnemyWepon();
        }
    }

    void setTags(GameObject obj, string layer_name)
    {
        obj.layer = LayerMask.NameToLayer(layer_name);

        foreach(Transform children in obj.transform)
        {
            setTags(children.gameObject, layer_name);
        }
    }

    [Command]
    void CmdSetEnemyWepon(int wepon_id){
        RpcSetWepon(wepon_id);
    }

    [ClientRpc]
    void RpcSetWepon(int wepon_id){
        ImportantFunction();
        weaponobj[wepon_id].SetActive(true);
        modelwep[wepon_id].SetActive(true);
        isActive[wepon_id] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isActive[0])
        {
            ImportantFunction();
            animator.SetTrigger("Sword");
            shootScriptObj.SetGunId(0);
            CmdSetEnemyWepon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isActive[1])
        {
            ImportantFunction();
            animator.SetTrigger("Weapon");
            shootScriptObj.SetGunId(1);
            CmdSetEnemyWepon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !isActive[2])
        {
            ImportantFunction();
            animator.SetTrigger("Weapon");
            shootScriptObj.SetGunId(2);
            CmdSetEnemyWepon(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !isActive[3])
        {
            ImportantFunction();
            animator.SetTrigger("Weapon");
            shootScriptObj.SetGunId(3);
            CmdSetEnemyWepon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && !isActive[4])
        {
            ImportantFunction();
            animator.SetTrigger("Torch");
            shootScriptObj.SetGunId(4);
            CmdSetEnemyWepon(4);
        }
    }

    [Command]
    protected void CmdTrigger(string trigger)
    {
        // Verification, hit detection, and damage
        RpcSetTrigger(trigger); // Animate clients
    }

    [ClientRpc]
    public void RpcSetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ImportantFunction()
    {
        foreach (GameObject elem in weaponobj)
        {
            elem.SetActive(false);
        }
        foreach (GameObject model in modelwep)
        {
            model.SetActive(false);
        }
        for (int i = 0; i < isActive.Length; i++)
        {
            isActive[i] = false;
        }
    }

    public void ResetWepon()
    {
        ImportantFunction();
        weaponobj[0].SetActive(true);
        modelwep[0].SetActive(true);
        isActive[0] = true;
        CmdSetEnemyWepon(0);
        shootScriptObj.SetGunId(0);
    }
}