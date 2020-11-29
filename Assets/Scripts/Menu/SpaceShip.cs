using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(MoveOverSeconds(gameObject, new Vector3(transform.localPosition.x-2.5f, transform.localPosition.y, transform.localPosition.z - 2.5f), 120));
    }
    //void FixedUpdate()
    //{
    //    transform.Translate(transform.forward * Time.deltaTime/18, Space.Self);
    //}

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.localPosition = end;
    }
}
