using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ParallaxController : MonoBehaviour {

    public static ParallaxController parallaxController;

    public bool changeInEditor;

    [Space]
    public Vector2 offset;

    GameObject pref;
    Parallax parallax;

    public enum LandScape { green_valley, spruce_forest, Length };
    public LandScape currentLandScape;

    private void Awake() {
        parallaxController = this;
    }
    void Start() {
        foreach (Transform parent in transform) {
            foreach (Transform child in parent) {
                parallax = child.GetComponent<Parallax>();
                if (parallax != null) {
                    if (parallax.createsCopys) {
                        pref = parallax.gameObject;
                        pref.GetComponent<Parallax>().enabled = false;

                        GameObject childL = Instantiate(pref, parallax.gameObject.transform);
                        pref = childL;
                        childL.name = "L";

                        GameObject childR = Instantiate(pref, parallax.gameObject.transform);
                        parallax.gameObject.GetComponent<Parallax>().enabled = true;
                        childR.name = "R";

                        childL.transform.localScale = new Vector3(1, 1, 1);
                        childR.transform.localScale = new Vector3(1, 1, 1);

                        childL.transform.localPosition = new Vector3(-(parallax.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / parallax.transform.localScale.x) + offset.x, 0, 0);
                        childR.transform.localPosition = new Vector3((parallax.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / parallax.transform.localScale.x) - offset.x, 0, 0);
                        parallax.offset = offset;
                    }
                    pref = null;
                }
                parallax = null;
            }
        }
    }
    private void OnValidate() {
        if(changeInEditor)ActualizeLandScape(currentLandScape);
    }
    public LandScape GetLandScape() {
        return currentLandScape;
    }
    public void ActualizeLandScape(LandScape newLandScape) {
        currentLandScape = newLandScape;
        foreach (Transform parent in transform) parent.gameObject.SetActive(false);
        transform.GetChild((int)currentLandScape).gameObject.SetActive(true);
        changeInEditor = false;
    }
    /*
    public void Actualize() {
        for (int i = 0; i < transform.childCount; i++) {
            parallax = transform.GetChild(i).GetComponent<Parallax>();
            if (parallax != null) parallax.Actualize();
        }
    }
    */
}
[CustomEditor(typeof(ParallaxController))]
public class ParallaxControllerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        /*
        if (GUILayout.Button("Actualize")) {
            foreach (var script in targets.Cast<ParallaxController>()) {
                script.Actualize();
            }
        }
        */
    }
}
