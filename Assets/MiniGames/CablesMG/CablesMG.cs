using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CablesMG : MiniGame {

    public Cable[] cables;
    public CableEnd[] cablesEnds;

    [HideInInspector]
    public Cable cable;
    [HideInInspector]
    public CableEnd cableEnd;

    public override void MiniGamePrepare() {

        List<int> nums1 = new List<int>{ 0, 1, 2, 3 };
        List<int> nums2 = new List<int>{ 0, 1, 2, 3 };

        for(int i = 0; i < 4; i++) {

            int j1 = Random.Range(0, nums1.Count);
            cables[i].index = nums1[j1];
            nums1.RemoveAt(j1);
            cables[i].Actualize();

            int j2 = Random.Range(0, nums2.Count);
            cablesEnds[i].index = nums2[j2];
            nums2.RemoveAt(j2);
            cablesEnds[i].StartActualizeColor();
        }
    }
    public override void MiniGameFinish() {

    }
    private void Update() {

    }
}