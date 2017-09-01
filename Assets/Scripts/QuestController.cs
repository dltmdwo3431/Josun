using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestController : MonoBehaviour
{

    public Text TextSilver;
    public Text TextContributiveness;
    public Text TextPublicSentiment;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void YesButtonClick()
    {
        DataController.Instance.Contributiveness += 5;
        DataController.Instance.PublicSentiment -= 5;
        TextContributiveness.text = DataController.Instance.Contributiveness.ToString();
        TextPublicSentiment.text = DataController.Instance.PublicSentiment.ToString();
    }

    public void NoButtonClick()
    {
        DataController.Instance.Contributiveness -= 5;
        DataController.Instance.PublicSentiment += 5;
        TextContributiveness.text = DataController.Instance.Contributiveness.ToString();
        TextPublicSentiment.text = DataController.Instance.PublicSentiment.ToString();
    }
}
