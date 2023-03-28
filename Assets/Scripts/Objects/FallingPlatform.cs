using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private bool platformIsFalling = false;
    private bool platformIsShaking = false;
    public GameObject platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlatformFall());
        }
    }

    private IEnumerator PlatformFall()
    {
        platformIsShaking = true;
        yield return new WaitForSeconds(0.3f);
        platformIsFalling = true;
    }

    void Update()
    {
        if (platformIsShaking && !platformIsFalling)
        {
            float shake = Mathf.Sin(Time.time * 30f) * 0.0005f;
            Vector3 currentPosition = platform.transform.localPosition;
            currentPosition.x += shake;
            platform.transform.localPosition = currentPosition;
        }

        else if (platformIsFalling)
        {
            platform.transform.localPosition -= transform.up * 0.03f;
        }
    }
}
