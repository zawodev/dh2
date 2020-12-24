using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfit : MonoBehaviour {

    #region STYLE
    public SpriteRenderer head;
    public SpriteRenderer eyes;
    public SpriteRenderer face;
    public SpriteRenderer body;
    [Space]
    public SpriteRenderer arm1;
    public SpriteRenderer arm2;
    public SpriteRenderer leg1;
    public SpriteRenderer leg2;
    [Space]
    public SpriteRenderer shirt1;
    public SpriteRenderer shirt2;
    public SpriteRenderer trouser1;
    public SpriteRenderer trouser2;
    [Space]
    public List<Sprite> clothe = new List<Sprite>();

    [Header("COLOR")]
    public Color furCol;
    public Color dark_furCol;
    public Color shirtCol;
    public Color trouserCol;
    public Color bootsCol;
    #endregion

    private void Start() {
        Actualize();
    }
    public void Actualize() {
        //body.sprite = coss;
        ActualizeColor();
    }
    public void ActualizeColor() {
        //body.color = coss;
    }

    //public void NawigujPoDostepntchSpritachDoMenu() {
    
    //}
}
