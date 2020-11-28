using UnityEngine;
using Mirror;

public class Weapons : NetworkBehaviour
{
    [Header("Add the weapons")]
    [SerializeField]
    private GameObject[] weaponobj;
    [SerializeField]
    private GameObject[] modelwep;
    [Header("The state of the weapons")]
    [SerializeField]
    private bool[] isActive;

    private void Start()
    {
        weaponobj[0].SetActive(true);
            modelwep[0].SetActive(true);
            isActive[0] = true;
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
            weaponobj[0].SetActive(true);
            modelwep[0].SetActive(true);
            isActive[0] = true;
            CmdSetEnemyWepon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isActive[1])
        {
            ImportantFunction();
            weaponobj[1].SetActive(true);
            modelwep[1].SetActive(true);
            isActive[1] = true;
            CmdSetEnemyWepon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && !isActive[2])
        {
            ImportantFunction();
            weaponobj[2].SetActive(true);
            modelwep[2].SetActive(true);
            isActive[2] = true;
            CmdSetEnemyWepon(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !isActive[3])
        {
            ImportantFunction();
            weaponobj[3].SetActive(true);
            modelwep[3].SetActive(true);
            isActive[3] = true;
            CmdSetEnemyWepon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !isActive[4])
        {
            ImportantFunction();
            weaponobj[4].SetActive(true);
            modelwep[4].SetActive(true);
            isActive[4] = true;
            CmdSetEnemyWepon(4);
        }
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
}