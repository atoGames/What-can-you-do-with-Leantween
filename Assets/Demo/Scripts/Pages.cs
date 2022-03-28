using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pages : MonoBehaviour
{
    protected int currentIndex = 0;
    private float m_Speed = 0.2f;

    public Button btnNextPage, btnPreviousPage;
    public LeanTweenType leanTweenType = LeanTweenType.easeSpring;
    private void OnEnable()
    {


        btnNextPage?.onClick.RemoveAllListeners();
        btnNextPage?.onClick.AddListener(() => NextOrPrev(1));

        btnPreviousPage?.onClick.RemoveAllListeners();
        btnPreviousPage?.onClick.AddListener(() => NextOrPrev(-1));

        // Fix spacing between pages
        for (var i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).localPosition = new Vector3(Screen.width * i, 0);

    }
    public void NextOrPrev(int index)
    {
        // If tweening runing return
        if (LeanTween.isTweening(gameObject))
            return;

        currentIndex = Mathf.Clamp(currentIndex + index, 0, gameObject.transform.childCount - 1);
        // Move to next page  
        LeanTween.moveLocalX(gameObject, -Screen.width * currentIndex, m_Speed).setEase(leanTweenType);
    }

}
