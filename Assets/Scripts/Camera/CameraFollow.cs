using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow CF;
    Camera mainCam;
    void Awake() {
        if (CF != null) {
            CF.ActualizeCameraSettings(startCameraSettings);
            CF.ActualizeStartCameraSettings(startCameraSettings);
            Destroy(gameObject);
            return;
        }
        CF = this;

        if (startCameraSettings.target == null) startCameraSettings.target = PlayerController.playerController.transform;
        ActualizeCameraSettings(startCameraSettings);

        Vector3 startPos = presentCameraSettings.target.position + presentCameraSettings.dynamicOffset + presentCameraSettings.staticOffset;
        startPos.z = -10;
        transform.position = startPos;

        mainCam = GetComponent<Camera>();
        synced = true;
    }
    
    public CameraSettings presentCameraSettings;
    public CameraSettings startCameraSettings;

    /*
    //[HideInInspector]
    public Transform target;

    public float smoothSpeed;
    [HideInInspector]
    public float startSmoothSpeed;
    //[Tooltip("Do animacji")]
    [HideInInspector]
    public Vector3 dynamicOffset;
    //[Tooltip("Tylko w Inspectorze")]
    public Vector3 staticOffset;

    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    */

    [HideInInspector]
    public bool synced;

    //private void Start() {
    //    //tutaj ma sie cos dziac zeby nie bylo tego skoku na stracie sceny, np czarny ekran albo teleport COKOLWIEK :S
    //    // w sumie to nawet nie musi sie tutaj dziac, np moze to byc ekran wczytywania na kazdej scenie
    //}

    //dynamicoffset might be actually useless

    Vector3 GetPos(Vector3 targetPos) {
        targetPos = new Vector3(
                Mathf.Clamp(targetPos.x, presentCameraSettings.minCameraPos.x, presentCameraSettings.maxCameraPos.x),
                Mathf.Clamp(targetPos.y, presentCameraSettings.minCameraPos.y, presentCameraSettings.maxCameraPos.y),
                Mathf.Clamp(targetPos.z, presentCameraSettings.minCameraPos.z, presentCameraSettings.maxCameraPos.z));
        //==============
        return targetPos;
    }
    private void FixedUpdate() {
        if (presentCameraSettings.target != null) {

            Vector3 targetPos = presentCameraSettings.target.position;
            if (presentCameraSettings.bounds) targetPos = GetPos(targetPos);

            Vector3 desiredPosition = targetPos + presentCameraSettings.dynamicOffset + presentCameraSettings.staticOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, presentCameraSettings.moveSpeed * 0.01f);
            float desiredSize = presentCameraSettings.size;
            float smoothedSize = Mathf.Lerp(mainCam.orthographicSize, desiredSize, presentCameraSettings.magnifySpeed * 0.01f);

            if (synced) {
                transform.position = smoothedPosition;
                mainCam.orthographicSize = smoothedSize;
            }
            else {
                transform.position = desiredPosition;
                mainCam.orthographicSize = desiredSize;
            }

            /*if (presentCameraSettings.bounds) {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, presentCameraSettings.minCameraPos.x, presentCameraSettings.maxCameraPos.x),
                    Mathf.Clamp(transform.position.y, presentCameraSettings.minCameraPos.y, presentCameraSettings.maxCameraPos.y),
                    Mathf.Clamp(transform.position.z, presentCameraSettings.minCameraPos.z, presentCameraSettings.maxCameraPos.z));
            }*/
        }
    }

    public void ActualizeCameraSettings(CameraSettings newCameraSettings = null) {

        if (newCameraSettings == null) newCameraSettings = startCameraSettings;
        //

        if (newCameraSettings.target != null) presentCameraSettings.target = newCameraSettings.target;
        presentCameraSettings.moveSpeed = newCameraSettings.moveSpeed;
        presentCameraSettings.magnifySpeed = newCameraSettings.magnifySpeed;
        presentCameraSettings.size = newCameraSettings.size;

        presentCameraSettings.dynamicOffset = newCameraSettings.dynamicOffset;
        presentCameraSettings.staticOffset = newCameraSettings.staticOffset;

        presentCameraSettings.bounds = newCameraSettings.bounds;
        presentCameraSettings.minCameraPos = newCameraSettings.minCameraPos;
        presentCameraSettings.maxCameraPos = newCameraSettings.maxCameraPos;
    }
    public void ActualizeStartCameraSettings(CameraSettings newCameraSettings) {

        if (newCameraSettings.target != null) startCameraSettings.target = newCameraSettings.target;
        startCameraSettings.moveSpeed = newCameraSettings.moveSpeed;
        startCameraSettings.magnifySpeed = newCameraSettings.magnifySpeed;
        startCameraSettings.size = newCameraSettings.size;

        startCameraSettings.dynamicOffset = newCameraSettings.dynamicOffset;
        startCameraSettings.staticOffset = newCameraSettings.staticOffset;

        startCameraSettings.bounds = newCameraSettings.bounds;
        startCameraSettings.minCameraPos = newCameraSettings.minCameraPos;
        startCameraSettings.maxCameraPos = newCameraSettings.maxCameraPos;
    }
    public void OnChangeScene() {
        presentCameraSettings.moveSpeed = 0;
        presentCameraSettings.magnifySpeed = 0;
    }

    public void SetMinCamPos() {
        presentCameraSettings.minCameraPos = gameObject.transform.position;
    }
    public void SetMaxCamPos() {
        presentCameraSettings.maxCameraPos = gameObject.transform.position;
    }
}
