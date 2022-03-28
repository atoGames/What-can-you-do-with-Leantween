using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwapCard : MonoBehaviour
{

    protected bool isReadyToSwap = false;
    protected GameObject swapCardPrefv;

    private void OnEnable()
    {
        // Set buttons
        for (var i = 0; i < transform.Find("Cards").childCount; i++)
        {
            var sc = transform.Find("Cards").GetChild(i).gameObject;

            sc.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            var index = i;
            sc.GetComponent<Button>()?.onClick.RemoveAllListeners();
            sc.GetComponent<Button>()?.onClick.AddListener(() => SwapTo(sc, index));
        }
    }

    protected void SwapTo(GameObject go, int index)
    {
        if (!isReadyToSwap)
            swapCardPrefv = go;
        else // Wait for the second click
            MoveX(go, swapCardPrefv, go.transform.GetSiblingIndex()).setOnStart(() => MoveX(swapCardPrefv, go, index));

        isReadyToSwap = !isReadyToSwap;

    }
    protected LTDescr MoveX(GameObject go, GameObject To, int index)
    {
        return LeanTween.moveLocal(go, To.transform.localPosition, 0.2f).setOnComplete(() => ChangeCardIndex(go, index));
    }
    protected void ChangeCardIndex(GameObject card, int index) => card.transform.SetSiblingIndex(index);

}
