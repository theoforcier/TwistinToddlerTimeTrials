using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GhostPlayer : MonoBehaviour
{

    public Ghost ghost;
    public WinMenu winMenu;
    private float timeValue;
    private int index1;
    private int index2;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ghost = AssetDatabase.LoadAssetAtPath<Ghost>("Assets/ScriptableObjects/GhostTrials/" + winMenu.prefName + ".asset");
        timeValue = 0;

        if (!ghost)
        {
            this.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        timeValue += Time.deltaTime;

        if (ghost)
        {
            GetIndex();
            SetTransform();
        }
    }

    private void GetIndex()
    {
        for (int i = 0; i < ghost.timeStamp.Count - 2; i++)
        {
            if (ghost.timeStamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghost.timeStamp[i] < timeValue & timeValue < ghost.timeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }

        index1 = ghost.timeStamp.Count - 1;
        index2 = ghost.timeStamp.Count - 1;
    }

    private void SetTransform()
    {
        if (index1 == index2)
        {
            this.transform.localPosition = ghost.position[index1];

        }
        else
        {
            // flip sprite if going backwards
            if (ghost.position[index2].x < ghost.position[index1].x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            float interpolationFactor = (timeValue - ghost.timeStamp[index1]) / (ghost.timeStamp[index2] - ghost.timeStamp[index1]);
            this.transform.localPosition = Vector2.Lerp(ghost.position[index1], ghost.position[index2], interpolationFactor);
        }

        animator.SetFloat("Speed", ghost.speed[index2]);
        animator.SetBool("isJumping", ghost.isJumping[index2]);
        animator.SetBool("onWall", ghost.onWall[index2]);
        GetComponent<SpriteRenderer>().flipX = ghost.spriteFliped[index2];
    }
}
