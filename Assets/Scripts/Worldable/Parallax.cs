using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    [HideInInspector]
    public float length, startPosX, startPosY, tempX, distX, distY;
    GameObject cam;
    [Range(-1, 1)]
    public float parallexEffect;

    public bool createsCopys;
    public bool inlcudeY;
    public bool auto;
    [Range(-1f, 1f)]
    public float speed;

    private void Awake() {
        cam = CameraFollow.CF.gameObject;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate() {
        if (auto) {
            transform.position += new Vector3((parallexEffect - 1) * speed, 0, 0);
            startPosX = transform.position.x;
            if (createsCopys) {
                if (startPosX > length || startPosX < -length) transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
        else {
            tempX = (cam.transform.position.x * (1 - parallexEffect)); // -1 at end + speed variable
            distX = (cam.transform.position.x * parallexEffect);

            if (inlcudeY) {
                distY = (cam.transform.position.y * parallexEffect);
            }

            transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
            if (createsCopys) {
                if (tempX > startPosX + length) startPosX += length;
                else if (tempX < startPosX - length) startPosX -= length;

                //if (inlcudeY) {
                //    if (tempY > startPosY + heigth) startPosY += heigth;
                //    else if (tempY < startPosY - heigth) startPosY -= heigth;
                //}
            }
        }
    }
    bool done;
    public void Actualize() {
        if (!done) {
            cam = CameraFollow.CF.GetComponent<Camera>().gameObject;
            startPosX = transform.position.x;
            startPosY = transform.position.y;
            length = GetComponent<SpriteRenderer>().bounds.size.x;

            done = true;
        }

        distX = (cam.transform.position.x * parallexEffect);
        if (inlcudeY) distY = (cam.transform.position.y * parallexEffect);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
    }
}

