using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    public GameObject explosionParticle;
    public void Explode(Vector3 pos, Vector2 vel) {
        Instantiate(explosionParticle, pos, Quaternion.identity);
        CameraShake.ShakeOnce(.2f, 1f);
    }
}
