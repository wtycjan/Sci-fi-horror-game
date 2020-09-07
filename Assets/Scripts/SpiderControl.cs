using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControl : MonoBehaviour
{
    private Vector3 deltaPosition, prevPosition;
    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
        Destroy(gameObject, 2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.forward);
        rotateSpider();
    }
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    private void rotateSpider()
    {
        deltaPosition = transform.position - prevPosition;
        if (deltaPosition != Vector3.zero)
        {
            transform.forward = deltaPosition;

        }
        prevPosition = transform.position;
    }
}
