using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicController : MonoBehaviour
{

    public List<Animator> AnimatorList;

    public Animator currentAnimator;
    public int currentIndex = 0;

    [SerializeField]
    private int internalCount = 0;

    public bool isSafeToSkip = true;

    public GameObject canSkipSprite;

    public Canvas menuCanvas;

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

        if (Input.GetKeyDown(KeyCode.Space) && isSafeToSkip && !menuCanvas.gameObject.activeInHierarchy)
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
        else
        {
            Debug.Log("END");
            GameObject canvas = menuCanvas.gameObject;
            canvas.SetActive(!canvas.activeInHierarchy);
        }
    }

    public void StartIsClicked()
    {
        Debug.Log("Start was clicked");
        menuCanvas.gameObject.SetActive(false);
        currentAnimator = AnimatorList[currentIndex];
        if (IsOnState(currentAnimator, "WAIT"))
        {
            SetTrigger(currentAnimator, "WAIT");
        }
    }

    public void OnCreditsClicked()
    {
        GameObject menu = menuCanvas.transform.Find("Menu").gameObject;
        menu.SetActive(!menu.activeInHierarchy);

        GameObject credits = menuCanvas.transform.Find("Credits").gameObject;
        credits.SetActive(!credits.activeInHierarchy);
    }

    public void OnReturnClicked()
    {
        GameObject menu = menuCanvas.transform.Find("Menu").gameObject;
        menu.SetActive(!menu.activeInHierarchy);

        GameObject credits = menuCanvas.transform.Find("Credits").gameObject;
        credits.SetActive(!credits.activeInHierarchy);
    }

    public void QuitIsClicked()
    {
        Application.Quit();
    }
}
