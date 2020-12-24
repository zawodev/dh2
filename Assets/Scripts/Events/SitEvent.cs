using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitEvent : EventHolder {

    //PlayerController playerController;

    [Header("Sit Event")]
    public bool canPeek;
    [Range(-1, 1)]
    public int lookAt;
    float rotationen;

    public Vector2 peekPose;
    Vector2 previous;

    public override void OurStart() {
        previous = transform.position;
    }
    public override void TriggerEvent() {

        PlayerController.playerController.rb.velocity = new Vector2(0, 0);

        if (canPeek) {
            priorityComeBack = PlayerController.playerController.AnimateSitHid("hid");

            PlayerController.playerController.SetPose(priorityComeBack ? (Vector2)transform.position : peekPose);
        }
        else {
            priorityComeBack = PlayerController.playerController.AnimateSitHid("sit");
            PlayerController.playerController.ToggleShotable(!priorityComeBack);

            previous = PlayerController.playerController.SwapPose(previous);
        }

        PlayerController.playerController.freeze = priorityComeBack;

        if (!priorityComeBack) {
            PlayerController.playerController.RotatePlayer(rotationen);
        }
        else {
            rotationen = PlayerController.playerController.transform.rotation.y;

            switch (lookAt) {
                case -1:
                    PlayerController.playerController.RotatePlayer(0f);
                    break;
                case 1:
                    PlayerController.playerController.RotatePlayer(180f);
                    break;
            }
        }
    }
}
