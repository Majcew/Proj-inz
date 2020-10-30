using UnityEngine;

/* Komenda wymagająca podpięcie komponentu "PlaterController" w poprawnego działania skryptu*/
[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    #region Variables
    private CharacterController cc;

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
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        //Rozglądanie się na boki
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        //Rozglądanie się góra-dół
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Poruszanie graczem (jeżeli ma kontakt z podłożem), w innym przypadku niech działa przyciąganie ziemskie
        if (cc.isGrounded)
        {
            //Sprawdź, czy gracz chce biegać
            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            }
            else 
            {
                forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
                sidewaysMovement = - Input.GetAxis("Horizontal") * playerWalkingSpeed;
            }
        }
        else { verticalVelocity += Physics.gravity.y * Time.deltaTime; }

        //Skakanie po wciśnięciu przycisku odpowiedzialnego za skok
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(forwardMovement, verticalVelocity, sidewaysMovement);
        //Poruszanie bohaterem
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }
}
