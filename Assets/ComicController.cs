using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicController : MonoBehaviour
{

    public List<Animator> AnimatorList;

    public Animator currentAnimator;
    public int currentIndex = 0;

    [SerializeField]
    private int internalCount = 0;

    public bool isSafeToSkip = true;

    public GameObject canSkipSprite;

    void Update()
    {
        isSafeToSkip = IsOnState(AnimatorList[currentIndex], "CYCLE" + internalCount) || IsOnState(AnimatorList[currentIndex], "END") || IsOnState(AnimatorList[currentIndex], "WAIT");
        if (isSafeToSkip)
        {
            canSkipSprite.SetActive(true);
        }
        else
        {
            canSkipSprite.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isSafeToSkip)
        {
            currentAnimator = AnimatorList[currentIndex];
            if (IsOnState(currentAnimator, "END"))
            {                
                AddToIndex();
                internalCount = 0;
                currentAnimator = AnimatorList[currentIndex];
                SetTrigger(currentAnimator, "WAIT");
            }
            else if (IsOnState(currentAnimator, "WAIT"))
            {
                SetTrigger(currentAnimator, "WAIT");
            }
            else if (IsOnState(currentAnimator, "CYCLE" + internalCount))
            {
                SetTrigger(currentAnimator, "CYCLE" + internalCount);
                internalCount++;
            }
        }
    }

    bool IsOnState(GameObject obj, string StateName)
    {
        return obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(StateName);
    }

    bool IsOnState(Animator obj, string StateName)
    {
        return obj.GetCurrentAnimatorStateInfo(0).IsName(StateName);
    }

    void PlayAnimation(GameObject obj, string StateName)
    {
        obj.GetComponent<Animator>().Play(StateName);
    }

    void SetTrigger(Animator obj, string TriggerName)
    {
        obj.SetTrigger(TriggerName);
    }

    void AddToIndex()
    {
        if (currentIndex < AnimatorList.Count - 1) currentIndex++;
    }
}
