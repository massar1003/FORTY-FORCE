using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    float cameraMoveDist, moveDistDecay = 10;
    Vector2 curDist;

    private void LateUpdate()
    {
        Vector2 targetPosition = player.transform.position;
        Vector2 tarDist = player.MoveDirec;
        const float distLerp = 2f;
        curDist = Vector2.MoveTowards(curDist, tarDist, distLerp);
        targetPosition += curDist * cameraMoveDist;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10);
        curDist *= moveDistDecay * Time.deltaTime;
    }
}
