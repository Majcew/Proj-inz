using UnityEngine;
using Mirror;

/* Komenda wymagająca podpięcie komponentu "PlayerController" w poprawnego działania skryptu*/
[RequireComponent(typeof(CharacterController))]
public class Player_Movement : NetworkBehaviour
{
    #region Variables
    private CharacterController cc;
    private Animator animator;

    /* Zmienne odpowiedzialne za prędkość gracza podczas chodzenia, biegania, "siła" skoku*/
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 20f;
    public float verticalRotationLimit = 80f;

    private float forwardMovement;
    private float sidewaysMovement;
    private float verticalVelocity;
    private float verticalRotation = 0;
    #endregion

    void Start()
    {
        Debug.Log("isLocalPlayer " + isLocalPlayer);
        if (isLocalPlayer)
        {
            cc = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    void LateUpdate()
    {
        if (!isLocalPlayer) { 
            return; 
        }
        //Rozglądanie się na góra dół (nie działa)
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        //Rozglądanie się na boki
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        switch (Input.GetAxis("Vertical"))
        {
            case float n when (n < 0 && n >= -1):
                animator.SetBool("Walk_backward", true);
                animator.SetBool("Walk_forward", false);
                break;
            case float n when (n > 0 && n <= 1):
                animator.SetBool("Walk_forward", true);
                animator.SetBool("Walk_backward", false);
                break;
            default:
                animator.SetBool("Walk_forward", false);
                animator.SetBool("Walk_backward", false);
                break;
        }

        /*switch (Input.GetAxis("Horizontal"))
        {
            case float n when (n < 0 && n >= -1):
                animator.SetBool("Run_left", true);
                animator.SetBool("Run_right", false);
                break;
            case float n when (n > 0 && n <= 1):
                animator.SetBool("Run_right", true);
                animator.SetBool("Run_left", false);
                break;
            default:
                animator.SetBool("Run_left", false);
                animator.SetBool("Run_right", false);
                break;
        }*/

        //Poruszanie graczem (jeżeli ma kontakt z podłożem), w innym przypadku niech działa przyciąganie ziemskie
        if (cc.isGrounded)
        {
            //Sprawdź, czy gracz chce biegać
            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
                animator.SetBool("Running", true);
            }
            else 
            {
                forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
                animator.SetBool("Running", false);
            }
        }
        else { verticalVelocity += Physics.gravity.y * Time.deltaTime; }

        //Skakanie po wciśnięciu przycisku odpowiedzialnego za skok
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);
        //Poruszanie bohaterem
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }

    public void TestFunc()
    {
        verticalRotation += 1;
        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);
        //Poruszanie bohaterem
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }
}
