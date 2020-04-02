using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //Camera vars
    [SerializeField]
    float sensitivity = 5.0f, cameraFOVReset;
    [SerializeField]
    float xAngleMax, xAngleMin;
    float yRotation, yRotate, xRotate;
    [SerializeField]
    GameObject character;

    //Leaning vars
    [SerializeField]
    GameObject[] leanPosition;
    [SerializeField]
    float leanSpeed;

    //FOV vars
    public float cameraFOV;
    float cameraFOVCurrent;

    //References to other GOs
    PlayerControls playerScript;
    Camera MainCamera;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        playerScript = character.GetComponent<PlayerControls>();
        MainCamera = this.GetComponent<Camera>();
        MainCamera.fieldOfView = cameraFOV;
        cameraFOVCurrent = cameraFOV;
    }

    // Update is called once per frame
    void Update()
    {
        yRotation = transform.rotation.x;

        xRotate += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        yRotate += Input.GetAxis("Mouse Y") * -sensitivity *Time.deltaTime;
        yRotate = Mathf.Clamp(yRotate, xAngleMin, xAngleMax);
        transform.eulerAngles = new Vector3(yRotate, xRotate, 0);
        character.transform.eulerAngles = new Vector3(0, xRotate, 0);

        MainCamera.fieldOfView = cameraFOV;

        if(cameraFOV > 60)
        {
            cameraFOV -= Time.deltaTime * cameraFOVReset;
        }

        if(cameraFOV < 60)
        {
            cameraFOV += Time.deltaTime * cameraFOVReset;
        }

        if (Input.GetMouseButtonDown(0))
        {
          RaycastHit rayHit;
          if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit))
          {
                if (rayHit.collider.gameObject.GetComponent<BarracksButton>())
                {
                    rayHit.collider.gameObject.GetComponent<BarracksButton>().SendMessage("AddUnit");
                }

                if (rayHit.collider.gameObject.GetComponent<GoldOre>() && Vector3.Distance(transform.position, rayHit.collider.gameObject.transform.position) <= 2f)
                {
                    rayHit.collider.gameObject.GetComponent<GoldOre>().SendMessage("GatherGold");
                }
            }
        }

        RaycastHit rayHitAlways;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHitAlways))
        {
            #region Outline
            if (rayHitAlways.collider.gameObject.GetComponent<Outline>() && Vector3.Distance(transform.position, rayHitAlways.collider.gameObject.transform.position) > 2f && Vector3.Distance(transform.position, rayHitAlways.collider.gameObject.transform.position) < 5f)
            {
                rayHitAlways.collider.gameObject.GetComponent<Outline>().enabled = true;
                rayHitAlways.collider.gameObject.GetComponent<Outline>().OutlineColor = Color.red;
            }

            if (rayHitAlways.collider.gameObject.GetComponent<Outline>() && Vector3.Distance(transform.position, rayHitAlways.collider.gameObject.transform.position) <= 2f)
            {
                rayHitAlways.collider.gameObject.GetComponent<Outline>().enabled = true;
                rayHitAlways.collider.gameObject.GetComponent<Outline>().OutlineColor = Color.green;
            }
            #endregion
        }

        #region Leaning code
        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            this.transform.position = Vector3.Lerp(this.transform.position, leanPosition[1].transform.position, leanSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            this.transform.position = Vector3.Lerp(this.transform.position, leanPosition[2].transform.position, leanSpeed * Time.deltaTime);

        if(!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            this.transform.position = Vector3.Lerp(this.transform.position, leanPosition[0].transform.position, leanSpeed * Time.deltaTime);
        #endregion
    }

    public void ChangeFOV(float FOV)
    {
        cameraFOV = FOV;
    }
}
