using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiveCard : MonoBehaviour
{
    public float m_Speed = 0.2f;
    public LeanTweenType leanTweenType;

    public Button btnFiveCardInfo, btnFiveCardBack;

    public bool useRoatate = false;
    private bool isFront = true;

    private void OnEnable()
    {

        btnFiveCardInfo = null ?? transform.Find("Front/Use & Info/Info/btn Info").GetComponent<Button>();
        btnFiveCardBack = null ?? transform.Find("Back/Back/btn Back").GetComponent<Button>();



        // Info button :: Info&Back buttons use the same function
        btnFiveCardInfo?.onClick.RemoveAllListeners();
        btnFiveCardInfo?.onClick.AddListener(MoveCard);
        // Back button
        btnFiveCardBack?.onClick.RemoveAllListeners();
        btnFiveCardBack?.onClick.AddListener(MoveCard);
    }

    public void MoveCard()
    {
        // We only change front face
        GameObject front = transform.Find("Front").gameObject;


        if (LeanTween.isTweening(front))
            return;

        MoveX(front, -350f, isFront ? 0 : 1).setEase(leanTweenType);
        if (useRoatate)
            LeanTween.rotateZ(front, -64, m_Speed).setOnComplete(() => LeanTween.rotateZ(front, 0, m_Speed));

        isFront = !isFront;

    }
    protected LTDescr MoveX(GameObject card, float cardWidth, int index)
    {
        var normalPosition = 0;
        return LeanTween.moveLocalX(card, cardWidth, m_Speed).setOnComplete(() => LeanTween.moveLocalX(card, normalPosition, m_Speed).setOnStart(() => ChangeCardIndex(card, index)));
    }
    protected void ChangeCardIndex(GameObject card, int index) => card.transform.SetSiblingIndex(index);

}
