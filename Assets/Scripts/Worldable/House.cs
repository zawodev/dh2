using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.Linq;

[ExecuteInEditMode]
public class House : MonoBehaviour {

    public bool runInEditor = true;

    public enum Type { classic, savanna};
    public Type type;

    [Space]
    public Material lineMaterial;
    public BoxCollider2D col;

    public int order;

    [Range(.05f, 1f)]
    public float thickness;

    [Space]
    public SpriteRenderer frontTexture;
    public SpriteRenderer roofTexture;
    public SpriteRenderer wallTexture;

    [Space]
    public GameObject window;
    public GameObject door;

    public GameObject linesObject;
    public Transform cornerPoint;

    Vector3 RightCorner;
    Vector3 DownCorner;
    Vector3 LeftUp;
    Vector3 RightUp;
    Vector3 LeftDown;
    Vector3 RightDown;

    float height;
    float lenght;

    LineRenderer[] liner = new LineRenderer[20];

    public Sprite[] wallTextures;
    public Sprite[] roofTextures;
    public Sprite[] windowTextures;

    public Vector2 offset;
    public Vector2 fix;

    private void Start() {
        //Actualize();
    }
    private void Update() {
        if (runInEditor && Application.isEditor) Actualize();
    }
    public void Actualize() {

        frontTexture.sortingOrder = order * 2 - 1;
        wallTexture.sortingOrder = order * 2 - 1;
        roofTexture.sortingOrder = order * 2 - 1;

        int lines = 9;

        for (int i = 0; i < lines; i++) {
            if (i == linesObject.transform.childCount) {
                GameObject g = new GameObject();
                g.transform.localPosition = new Vector3(0, 0, 0);
                g.transform.SetParent(linesObject.transform);
                g.AddComponent<LineRenderer>();
            }

            liner[i] = linesObject.transform.GetChild(i).GetComponent<LineRenderer>();
            liner[i].material = lineMaterial;

            liner[i].startWidth = thickness;
            liner[i].endWidth = thickness;
            liner[i].numCapVertices = 8;

            liner[i].startColor = Color.black;
            liner[i].endColor = Color.black;
        }

        lenght = frontTexture.bounds.size.x;
        height = frontTexture.bounds.size.y;

        RightCorner = cornerPoint.position + new Vector3(lenght, 0, 0);
        DownCorner = cornerPoint.position + new Vector3(0, -height, 0);
        LeftUp = frontTexture.bounds.center + new Vector3(-lenght / 2, height / 2, 0);
        RightUp = frontTexture.bounds.center + new Vector3(lenght / 2, height / 2, 0);
        LeftDown = frontTexture.bounds.center + new Vector3(-lenght / 2, -height / 2, 0);
        RightDown = frontTexture.bounds.center + new Vector3(lenght / 2, -height / 2, 0);

        float angle = Vector2.SignedAngle(Vector2.right, cornerPoint.position - LeftUp) - 180f;

        wallTexture.transform.localEulerAngles = new Vector3(0f, 0f, angle);
        roofTexture.transform.localEulerAngles = new Vector3(0f, 0f, angle);

        // ROZMIARY SKALOWANIA TEXTURY ROOF AND WALL =================================================================

        Vector2 h1 = GetIntersectionPointCoordinates(cornerPoint.position, LeftDown, DownCorner, LeftUp, out bool can1);
        if (can1) wallTexture.transform.position = new Vector3(h1.x, h1.y, 0f);
        float wid1 = Vector2.Distance(cornerPoint.position, LeftUp);
        float hei1 = Mathf.Clamp(height + offset.x + (1f / (angle + 90f) * 90f - fix.x) * height * fix.y, height, 2000f);
        wallTexture.size = new Vector2(wid1, hei1);

        Vector2 h2 = GetIntersectionPointCoordinates(cornerPoint.position, RightUp, LeftUp, RightCorner, out bool can2);
        if (can2) roofTexture.transform.position = new Vector3(h2.x, h2.y, 0f);
        float wid2 = Vector2.Distance(cornerPoint.position, LeftUp);
        float hei2 = Mathf.Clamp(lenght + offset.y - (1f / (angle + 0f) * 90f + fix.x) * lenght * fix.y, lenght, 2000f);
        roofTexture.size = new Vector2(wid2, hei2);

        //Debug.Log(wid + " " + hei + " " + wid2 + " " + hei2);

        col.size = new Vector2(lenght, 1f);

        DrawLine(0, LeftUp, RightUp, 0, 0, 0, 0);
        DrawLine(1, RightUp, RightDown, 0, 0, 0, 0);
        DrawLine(2, RightDown, LeftDown, 0, 0, 0, 0);
        DrawLine(3, LeftDown, LeftUp, 0, 0, 0, 0);
        DrawLine(4, cornerPoint.position, RightCorner, 0, 0, 0, 0);
        DrawLine(5, cornerPoint.position, DownCorner, 0, 0, 0, 0);
        DrawLine(6, cornerPoint.position, LeftUp);
        DrawLine(7, RightCorner, RightUp, 0, 0, 0, 0);
        DrawLine(8, DownCorner, LeftDown, 0, 0, 0, 0);
    }
    public void Randomize() {

        int x1 = Random.Range(0, wallTextures.Length);
        int x2 = Random.Range(0, roofTextures.Length);

        wallTexture.sprite = wallTextures[x1];
        frontTexture.sprite = wallTextures[x1];

        roofTexture.sprite = roofTextures[x2];
    }
    public void AddWindow() {

    }
    public void AddDoor() {

    }
    void DrawLine(int i, Vector3 a, Vector3 b, float x1 = 0, float x2 = 0, float y1 = 0, float y2 = 0) {

        float xx1 = (x1 != 0 ? thickness / 2 * x1 : 0);
        float xx2 = (x2 != 0 ? thickness / 2 * x2 : 0);
        float yy1 = (y1 != 0 ? thickness / 2 * y1 : 0);
        float yy2 = (y2 != 0 ? thickness / 2 * y2 : 0);

        liner[i].positionCount = 2;
        liner[i].sortingOrder = order * 2;
        liner[i].SetPosition(0, new Vector3(a.x + xx1, a.y + yy1, 0));
        liner[i].SetPosition(1, new Vector3(b.x + xx2, b.y + yy2, 0));
    }
    public Vector2 GetIntersectionPointCoordinates(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2, out bool found) {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);
        if (tmp == 0) {
            found = false;
            return Vector2.zero;
        }
        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;
        found = true;
        return new Vector2(
            B1.x + (B2.x - B1.x) * mu,
            B1.y + (B2.y - B1.y) * mu
        );
    }
}

[CustomEditor(typeof(House))]
public class HouseEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GUILayout.Space(10);

        House house = (House)target;
        if (GUILayout.Button("Aktualize")) {
            house.Actualize();
        }
        if (GUILayout.Button("Randomize")) {
            house.Randomize();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Add Window")) {
            house.AddWindow();
        }
        if (GUILayout.Button("Add Door")) {
            house.AddDoor();
        }

        if (house.type == House.Type.classic) {
            //house.name = EditorGUILayout.TextField("Name", house.name);
            //house.cost = EditorGUILayout.IntField("Cost", house.cost);
            //house.damage = EditorGUILayout.IntField("Damage", house.damage);
        }
        if (house.type == House.Type.savanna) {
            //house.name = EditorGUILayout.TextField("Name", house.name);
            //house.cost = EditorGUILayout.IntField("Cost", house.cost);
            //house.damage = EditorGUILayout.IntField("Damage", house.damage);
            //EditorGUILayout.Space();
            //house.health = EditorGUILayout.IntField("Health", house.health);
            //house.moveRange = EditorGUILayout.IntField("Move Range", house.moveRange);
        }
    }
}
