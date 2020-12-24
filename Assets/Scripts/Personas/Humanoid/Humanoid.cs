using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour {

    public Animator anim;
    public void Start() {
        anim = GetComponentInChildren<Animator>();
    }
    
}
