using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public Text TextSilver;
    public Text TextContributiveness;
    public Text TextPublicSentiment;
    public Camera MainCamera;
    public GameObject EffectSpark;
    public AudioClip SFXClick;
    public Text TextUpgradePeerage;
    public Text TextUpgradePublicOffice1;
    public Text TextUpgradePublicOffice2;
    public Text TextUpgradeCollectSilver;
    public Text TextUpgradeCollectContributiveness;
    public Text TextUpgradeCollectPublicSentiment;
    public bool UpgradeCheck = true;
    public string TextPeerage;
    public string TextPublicOffice1;
    public string TextPublicOffice2;
    public Text getTextPeerage;
    public Text getTextPublicOffice1;
    public Text getTextPublicOffice2;
    public Text MaxPeerageText;
    public Text MaxPublicOfficeText;

    // Use this for initialization
    void Start()
    {
        TextSilver.text = DataController.Instance.Silver.ToString();
        TextContributiveness.text = DataController.Instance.Contributiveness.ToString();
        TextPublicSentiment.text = DataController.Instance.PublicSentiment.ToString();
        StartCoroutine(StartCollectSilver());
        StartCoroutine(StartCollectContributiveness());
        StartCoroutine(StartCollectPublicSentiment());
    }

    IEnumerator StartCollectSilver()
    {

        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            DataController.Instance.Silver += DataController.Instance.SilverPerSec;
            TextSilver.text = DataController.Instance.Silver.ToString();
        }
    }
    IEnumerator StartCollectContributiveness()
    {

        while (true)
        {
            yield return new WaitForSecondsRealtime(10f);
            DataController.Instance.Contributiveness += DataController.Instance.ContributivenessPerSec;
            TextContributiveness.text = DataController.Instance.Contributiveness.ToString();
        }
    }
    IEnumerator StartCollectPublicSentiment()
    {

        while (true)
        {
            yield return new WaitForSecondsRealtime(10f);
            DataController.Instance.PublicSentiment += DataController.Instance.PublicSentimentPerSec;
            TextPublicSentiment.text = DataController.Instance.PublicSentiment.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //마우스 클릭했을때
        if (Input.GetMouseButtonDown(0))
        {

            DataController.Instance.Silver += DataController.Instance.SilverPerSec;
            TextSilver.text = DataController.Instance.Silver.ToString();

            //카메라에서 레이저를 쏴서 큐브와 충돌하는지 테스트 
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //클릭 이펙트,사운드
                Instantiate(EffectSpark, hit.point, EffectSpark.transform.rotation);
                MainCamera.gameObject.GetComponent<AudioSource>().PlayOneShot(SFXClick);
            }

        }

    }
    
    //품계 획득
    public void UpgradePeerage()
    {

        if (DataController.Instance.PeerageCount > 17)
        {
            StartCoroutine("MaxPeerageTextDelete");
            return;
        }

        //재화 체크
        if (DataController.Instance.Silver < DataController.Instance.PeerageCost1[DataController.Instance.PeerageCount])
        {
            return;
        }
        else if (DataController.Instance.Contributiveness < DataController.Instance.PeerageCost2[DataController.Instance.PeerageCount])
        {
            return;
        }
        else if (DataController.Instance.PublicSentiment < DataController.Instance.PeerageCost3[DataController.Instance.PeerageCount])
        {
            return;
        }
        //재화 체크

        //관직에 따른 품계 제한
        if (DataController.Instance.PeerageCount <= 6)
        {
            if (DataController.Instance.PeerageCount != DataController.Instance.PublicOfficeCount)
            {
                return;
            }
        }
        else if (DataController.Instance.PeerageCount == 18) { }
        else
        {
            if(DataController.Instance.PeerageCount+ DataController.Instance.PeerageUpgragdeCheck != DataController.Instance.PublicOfficeCount)
            {
                return;
            }
            else
            {
                DataController.Instance.PeerageUpgragdeCheck++;
            }
        }
        //관직에 따른 품계 제한

        if (DataController.Instance.PeerageCount <= 17)
        {
            DataController.Instance.PeerageCount++;
            DataController.Instance.PeerageCountCheck = true;
        }
        DataController.Instance.SilverPerSec += DataController.Instance.GetPeerageSilver[DataController.Instance.PeerageCount];
        DataController.Instance.Silver -= DataController.Instance.PeerageCost1[DataController.Instance.PeerageCount - 1];
        TextSilver.text = DataController.Instance.Silver.ToString();
        TextPeerage = DataController.Instance.Peerage[DataController.Instance.PeerageCount].ToString();
        getTextPeerage.text = TextPeerage.ToString();

        String upgradeText = String.Format("품계 획득\n현재: {0}\n 다음: {1}\n: {2} 전\n: {3} 점\n: {4} 점",
            DataController.Instance.Peerage[DataController.Instance.PeerageCount], 
            DataController.Instance.Peerage[DataController.Instance.PeerageCount + 1],
            DataController.Instance.PeerageCostText1[DataController.Instance.PeerageCount],
            DataController.Instance.PeerageCostText2[DataController.Instance.PeerageCount],
            DataController.Instance.PeerageCostText3[DataController.Instance.PeerageCount]);
        TextUpgradePeerage.text = upgradeText;
    }
    //최대 품계 텍스트
    IEnumerator MaxPeerageTextDelete()
    {
        MaxPeerageText.text = "최대 품계에 도달했습니다!";
        yield return new WaitForSeconds(3);
        MaxPeerageText.text = "";
    }

    //문반계열 관직
    public void UpgradePublicOffice1()
    {
        if (DataController.Instance.PublicOfficeCount > 30)
        {
            StartCoroutine("MaxPublicOfficeTextDelete");
            return;
        }

        //재화 체크 시작
        if (DataController.Instance.Silver < DataController.Instance.PublicOffice1Cost1[DataController.Instance.PublicOfficeCount])
        {
            return;
        }
        else if (DataController.Instance.Contributiveness < DataController.Instance.PublicOffice1Cost2[DataController.Instance.PublicOfficeCount])
        {
            return;
        }
        else if (DataController.Instance.PublicSentiment < DataController.Instance.PublicOffice1Cost3[DataController.Instance.PublicOfficeCount])
        {
            return;
        }
        //재화 체크끝


        //관직에 필요한 품계 체크
        if (DataController.Instance.PeerageCount <= 6)
        {
            if (DataController.Instance.PublicOfficeCount + 1 != DataController.Instance.PeerageCount)
            {
                return;
            }
        }
        else if(DataController.Instance.PeerageCount == 18) { }
        else
        {
            if (DataController.Instance.PublicOfficeCountCheck == 2)
            {
                if(!DataController.Instance.PeerageCountCheck)
                {
                    return;
                }
                else
                {
                    DataController.Instance.PublicOfficeCountCheck = 0;
                }
            }
            DataController.Instance.PublicOfficeCountCheck++;
            DataController.Instance.PeerageCountCheck = false;
        }
        //관직에 필요한 품계 체크
   
        if (DataController.Instance.PublicOfficeCheck2)
        {
            return;
        }
        DataController.Instance.PublicOfficeCheck1 = true;
        if (DataController.Instance.PublicOfficeCount <= 30)
        {
            DataController.Instance.PublicOfficeCount++;
        }
        DataController.Instance.SilverPerSec += DataController.Instance.GetPublicOfficeSilver[DataController.Instance.PublicOfficeCount];
        DataController.Instance.ContributivenessPerSec += 1;
        DataController.Instance.Silver -= DataController.Instance.PublicOffice1Cost1[DataController.Instance.PublicOfficeCount - 1];
        TextSilver.text = DataController.Instance.Silver.ToString();
        TextContributiveness.text = DataController.Instance.Contributiveness.ToString();
        TextPublicOffice1 = DataController.Instance.PublicOffice1[DataController.Instance.PublicOfficeCount].ToString();
        getTextPublicOffice1.text = TextPublicOffice1.ToString();

        String upgradeText = String.Format("관직 획득(문반)\n현재: {0}\n다음: {1}\n: {2} 전\n: {3} 점\n: {4} 점",
            DataController.Instance.PublicOffice1[DataController.Instance.PublicOfficeCount],
            DataController.Instance.PublicOffice1[DataController.Instance.PublicOfficeCount + 1],
            DataController.Instance.PublicOffice1CostText1[DataController.Instance.PublicOfficeCount],
            DataController.Instance.PublicOffice1CostText2[DataController.Instance.PublicOfficeCount],
            DataController.Instance.PublicOffice1CostText3[DataController.Instance.PublicOfficeCount]);
        TextUpgradePublicOffice1.text = upgradeText;
    }

    //무반계열 관직
    public void UpgradePublicOffice2()
    {
        if (DataController.Instance.PublicOfficeCount > 30)
        {
            StartCoroutine("MaxPublicOfficeTextDelete");
            return;
        }

        //재화 체크 시작
        if (DataController.Instance.Silver < DataController.Instance.PublicOffice2Cost1[DataController.Instance.PublicOfficeCount])
        {
            return;
        }
        else if (DataController.Instance.Contributiveness < DataController.Instance.PublicOffice2Cost2[DataController.Instance.PublicOfficeCount])
        {
            return;
        }
        else if (DataController.Instance.PublicSentiment < DataController.Instance.PublicOffice2Cost3[DataController.Instance.PublicOfficeCount])
        {
            return;
        }
        //재화 체크 끝

        //관직에 필요한 품계 체크
        if (DataController.Instance.PeerageCount <= 6)
        {
            if (DataController.Instance.PublicOfficeCount + 1 != DataController.Instance.PeerageCount)
            {
                return;
            }
        }
        else if (DataController.Instance.PeerageCount == 18) { }
        else
        {
            if (DataController.Instance.PublicOfficeCountCheck == 2)
            {
                if (!DataController.Instance.PeerageCountCheck)
                {
                    return;
                }
                else
                {
                    DataController.Instance.PublicOfficeCountCheck = 0;
                }
            }
            DataController.Instance.PublicOfficeCountCheck++;
            DataController.Instance.PeerageCountCheck = false;
        }
        //관직에 필요한 품계 체크

        if (DataController.Instance.PublicOfficeCheck1)
        {
            return;
        }
        DataController.Instance.PublicOfficeCheck2 = true;
        if (DataController.Instance.PublicOfficeCount <= 30)
        {
            DataController.Instance.PublicOfficeCount++;
        }
        DataController.Instance.SilverPerSec += DataController.Instance.GetPublicOfficeSilver[DataController.Instance.PublicOfficeCount];
        DataController.Instance.PublicSentimentPerSec += 1;
        DataController.Instance.Silver -= DataController.Instance.PublicOffice2Cost1[DataController.Instance.PublicOfficeCount - 1];
        TextSilver.text = DataController.Instance.Silver.ToString();
        TextPublicSentiment.text = DataController.Instance.PublicSentiment.ToString();
        TextPublicOffice2 = DataController.Instance.PublicOffice2[DataController.Instance.PublicOfficeCount].ToString();
        getTextPublicOffice2.text = TextPublicOffice2.ToString();

        String upgradeText = String.Format("관직 획득(무반)\n현재: {0}\n다음: {1}\n: {2} 전\n: {3} 점\n: {4} 점",
            DataController.Instance.PublicOffice2[DataController.Instance.PublicOfficeCount],
            DataController.Instance.PublicOffice2[DataController.Instance.PublicOfficeCount + 1],
            DataController.Instance.PublicOffice2CostText1[DataController.Instance.PublicOfficeCount],
            DataController.Instance.PublicOffice2CostText2[DataController.Instance.PublicOfficeCount],
            DataController.Instance.PublicOffice2CostText3[DataController.Instance.PublicOfficeCount]);
        TextUpgradePublicOffice2.text = upgradeText;
    }
    //최대 관직 텍스트 표시후 삭제
    IEnumerator MaxPublicOfficeTextDelete()
    {
        MaxPublicOfficeText.text = "최대 관직에 도달했습니다!";
        yield return new WaitForSeconds(3);
        MaxPublicOfficeText.text = "";
    }

    //은화 수집량 증가
    public void UpgradeCollectSilver()
    {

        int Cost = DataController.Instance.CollectSilverLevel * DataController.Instance.CollectSilverLevel * 10;

        if (DataController.Instance.Silver < Cost)
        {
            return;
        }

        DataController.Instance.CollectSilverLevel++;
        DataController.Instance.SilverPerSec += DataController.Instance.CollectSilverLevel;
        DataController.Instance.Silver -= Cost;
        TextSilver.text = DataController.Instance.Silver.ToString();

        Cost = DataController.Instance.CollectSilverLevel * DataController.Instance.CollectSilverLevel * 10;
        String upgradeText = String.Format("은화 획득량 증가\n가격 : {0} 전", Cost);
        TextUpgradeCollectSilver.text = upgradeText;
    }

    //공헌도 구매
    public void UpgradeCollectContributiveness()
    {

        int Cost = DataController.Instance.CollectContributivenessLevel * DataController.Instance.CollectContributivenessLevel * 5;

        if (DataController.Instance.Silver < Cost)
        {
            return;
        }

        DataController.Instance.CollectContributivenessLevel++;
        DataController.Instance.Silver -= Cost;
        DataController.Instance.Contributiveness += 1;
        TextSilver.text = DataController.Instance.Silver.ToString();
        TextContributiveness.text = DataController.Instance.Contributiveness.ToString();

        Cost = DataController.Instance.CollectContributivenessLevel * DataController.Instance.CollectContributivenessLevel * 5;
        String upgradeText = String.Format("공헌도 증가\n가격 : {0} 전", Cost);
        TextUpgradeCollectContributiveness.text = upgradeText;
    }

    //민심 구매
    public void UpgradeCollectPublicSentiment()
    {

        int Cost = DataController.Instance.CollectPublicSentimentLevel * DataController.Instance.CollectPublicSentimentLevel * 5;

        if (DataController.Instance.Silver < Cost)
        {
            return;
        }

        DataController.Instance.CollectPublicSentimentLevel++;
        DataController.Instance.Silver -= Cost;
        DataController.Instance.PublicSentiment += 1;
        TextSilver.text = DataController.Instance.Silver.ToString();
        TextPublicSentiment.text = DataController.Instance.PublicSentiment.ToString();
        
        Cost = DataController.Instance.CollectPublicSentimentLevel * DataController.Instance.CollectPublicSentimentLevel * 5;
        String upgradeText = String.Format("민심 증가\n가격 : {0} 전", Cost);
        TextUpgradeCollectPublicSentiment.text = upgradeText;
    }
}
