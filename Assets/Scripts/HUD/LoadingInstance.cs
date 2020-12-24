using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingInstance : MonoBehaviour {

    public static LoadingInstance LI;

    private void Awake() {
        if (LI != null) {
            Destroy(gameObject);
            return;
        }
        else LI = this;
    }
}
