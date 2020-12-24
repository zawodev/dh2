using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.Linq;

[ExecuteInEditMode]
public class Inside : MonoBehaviour {

    [Space]
    public bool runInEditor = true;
    public bool wallOutline;
    public bool roofOutline;
    public bool collide;

    public AnimationCurve AC;
    public AnimationCurve AC2;

    [HideInInspector]
    public List<GameObject> textures;
    [HideInInspector]
    public List<GameObject> points;
    [HideInInspector]
    public List<GameObject> doors;

    [Range(.05f, 1f)]
    public float thickness;
    public Sprite tex;

    [Space]
    public LineRenderer LINE;
    public GameObject texPref;

    public GameObject doorPref;
    public Transform doorParent;

    public GameObject pointPref;
    public Transform pointParent;

    [Space]
    [Range(2f, 23f)]
    public float wallsHeight = 5f;
    [Range(1f, 10f)]
    public float wallsLength = 5f;

    public Vector2 offset;
    Vector3 lastPos;

    private void Update() {
        if (runInEditor && Application.isEditor) Actualize();
    }

    public GameObject window;
    //możesz w wolnym czasie pobawić się w uperfekcyjnienie tegoż skryptu, byłoby awesome <3

    public void Actualize() {

        for (int i = 0; i < points.Count; i++) {

            SetPointPos(points[i].transform.position, i);
            if (tex != null) textures[i].GetComponent<SpriteRenderer>().sprite = tex;

            //==================================

            LineRenderer LR = points[i].GetComponent<LineRenderer>();
            if (wallOutline || roofOutline) {
                LR.enabled = true;
                LR.positionCount = 3;

                //1
                if(wallOutline)LR.SetPosition(0, points[i].transform.position);
                else LR.SetPosition(0, points[i].transform.position + new Vector3(0, wallsHeight, 0));

                //2
                LR.SetPosition(1, points[i].transform.position + new Vector3(0, wallsHeight, 0));

                //3
                if (i < points.Count - 1 && roofOutline) LR.SetPosition(2, points[i + 1].transform.position + new Vector3(0, wallsHeight, 0));
                else LR.SetPosition(2, points[i].transform.position + new Vector3(0, wallsHeight, 0));

                LR.startWidth = thickness;
                LR.endWidth = thickness;
            }
            else {
                LR.enabled = false;
            }
            //==================================

            if (i > 0) {

                float angle = Vector2.SignedAngle(Vector2.right, points[i - 1].transform.position - points[i].transform.position) - 180f;
                textures[i].transform.localEulerAngles = new Vector3(0f, 0f, angle);

                Vector2 h1 = GetMiddlePoint(points[i - 1].transform.position, points[i].transform.position);
                textures[i].transform.position = new Vector3(h1.x + offset.x, h1.y + offset.y + wallsHeight / 2, 0f);

                float wid1 = Vector2.Distance(points[i - 1].transform.position, points[i].transform.position);
                float hei1 = /*Mathf.Clamp(wallsHeight + (1f / ((angle > -180f ? angle : -(360f + angle)) + 90f) * 90f - fix.x) * wallsHeight * fix.y, wallsHeight, 2000f)*/ wallsHeight;
                textures[i].GetComponent<SpriteRenderer>().size = new Vector2(wid1, hei1);
                float x = AC2.Evaluate((Mathf.Abs(angle) + 90) % 360);
                //textures[i].transform.localScale = new Vector3(1, 1 + (Mathf.Pow(Mathf.Abs(maxx - Mathf.Abs((x + b) % (maxx * 2))), a) / Mathf.Pow(maxx, a - 1)) * c, 1);
                textures[i].transform.localScale = new Vector3(1, AC.Evaluate(x), 1);
            }
        }
        LINE.startWidth = thickness;
        LINE.endWidth = thickness;
    }
    public void Randomize() {

        //int x1 = Random.Range(0, wallTextures.Length);
        //int x2 = Random.Range(0, roofTextures.Length);

        //wallTexture.sprite = wallTextures[x1];
        //frontTexture.sprite = wallTextures[x1];

        //roofTexture.sprite = roofTextures[x2];
    }
    public void AddPoint() {

        if (points.Count > 0) lastPos = points[points.Count - 1].transform.position + new Vector3(wallsLength, 0, 0);

        GameObject g1 = Instantiate(pointPref, lastPos, Quaternion.identity, pointParent);
        g1.name = "point" + points.Count.ToString();
        g1.SetActive(true);
        points.Add(g1);

        GameObject g2 = Instantiate(texPref, g1.transform);
        g2.name = "texture" + textures.Count.ToString();
        if (textures.Count > 0) g2.SetActive(true);
        textures.Add(g2);

        g2.GetComponent<SpriteRenderer>().renderingLayerMask = (uint)textures.Count;
        g2.transform.localPosition = new Vector3(offset.x, offset.y, 0f);
    }
    public void RemoveLastPoint() {

        if (points.Count > 0) {

            int x = points.Count;
            int y = textures.Count;

            DestroyImmediate(points[x - 1]);

            points.RemoveAt(x - 1);
            textures.RemoveAt(y - 1);

            LINE.positionCount--;
        }
    }
    public void RemoveAllPoints() {

        lastPos = transform.position;

        int x = points.Count;
        int y = textures.Count;

        for (int i = 0; i < x; i++) {
            DestroyImmediate(points[i]);
        }

        points.RemoveRange(0, x);
        textures.RemoveRange(0, y);

        LINE.positionCount = 0;

        if (collide) { //????????????

        }

        AddPoint();
    }
    public void AddDoor() {
        GameObject g1 = Instantiate(doorPref, lastPos + new Vector3(-5f, 1.65f, 0f), Quaternion.identity, doorParent);
        g1.name = "door" + doors.Count.ToString();
        g1.SetActive(true);
        doors.Add(g1);
    }
    public void RemoveLastDoor() {
        
    }
    public void RemoveAllDoors() {

    }

    //help voids
    void SetPointPos(Vector3 a, int i) {
        LINE.positionCount = i + 1;
        LINE.SetPosition(i, new Vector3(a.x, a.y, 0));
    }
    public Vector2 GetMiddlePoint(Vector2 A1, Vector2 A2) {
        return ((A2 - A1) / 2 + A1);
    }
}

[CustomEditor(typeof(Inside))]
public class InsideEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GUILayout.Space(10);

        Inside inside = (Inside)target;
        if (GUILayout.Button("Aktualize")) {
            inside.Actualize();
        }
        if (GUILayout.Button("Randomize")) {
            inside.Randomize();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Add Point")) {
            inside.AddPoint();
        }
        if (GUILayout.Button("Remove Last Point")) {
            inside.RemoveLastPoint();
        }
        if (GUILayout.Button("Remove All Points")) {
            inside.RemoveAllPoints();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Add Door")) {
            inside.AddDoor();
        }
        if (GUILayout.Button("Romove Last Door")) {
            inside.RemoveLastDoor();
        }
        if (GUILayout.Button("Romove All Doors")) {
            inside.RemoveAllDoors();
        }
    }
}
