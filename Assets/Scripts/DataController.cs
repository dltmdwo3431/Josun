using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour {


	// Singleton class start
	static GameObject _container;
	static GameObject Container {
		get {
			return _container;
		}
	}

	static DataController _instance;
	public static DataController Instance {
		get {
			if( ! _instance ) {
				_container = new GameObject();
				_container.name = "DataController";
				_instance = _container.AddComponent( typeof(DataController) ) as DataController;
				DontDestroyOnLoad (_container);
			}

			return _instance;
		}
	}
    // Singleton class end

    public string gameDataProjectFilePath = "/game.json";

    GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
            }
            return _gameData;
        }
    }

    MetaData _metaData;
    public MetaData metaData
    {
        get
        {
            if (_metaData == null)
            {
                LoadMetaData();
            }
            return _metaData;
        }
    }

    public void LoadMetaData()
    {
        TextAsset statJson = Resources.Load("MetaData/Meta") as TextAsset;
        Debug.Log(statJson.text);
        _metaData = JsonUtility.FromJson<MetaData>(statJson.text);
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            Debug.Log("loaded!");
            string dataAsJson = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            Debug.Log("Create new");

            _gameData = new GameData();
            _gameData.CollectSilverLevel = 1;
            _gameData.SilverPerSec = 1;
            _gameData.Silver = 0;
            _gameData.Contributiveness = 0;
            _gameData.PublicSentiment = 0;
        }
    }

    public void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.persistentDataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }

    public int Silver = 0;
    public int Contributiveness = 0;
    public int PublicSentiment = 0;

    public int SilverPerSec = 1;
    public int ContributivenessPerSec = 0;
    public int PublicSentimentPerSec = 0;

    public int CollectSilverLevel = 1;
    public int CollectContributivenessLevel = 1;
    public int CollectPublicSentimentLevel = 1;

    //품계명
    public string[] Peerage = 
        { "노비", "종9품", "정9품", "종8품", "정8품", "종7품", "정7품", "종6품", "정6품",
        "종5품","정5품","종4품","정4품","종3품","정3품","종2품","정2품","종1품","정1품","없음" };

    //문/무반 계열 관직명
    public string[] PublicOffice1 = 
        { "관노", "장사랑", "종사랑", "승사랑", "통사랑", "계공랑", "무공랑", "선무랑", "선교랑","승훈랑","승의랑",
        "봉훈랑","봉직랑","통선랑","통덕랑","조봉대부","조산대부","봉열대부","봉정대부","중훈대부","중직대부","통훈대부","통정대부","가선대부","가정대부","자헌대부","정헌대부",
        "승정대부","승록대부","보국승록대부","대광보국승록대부","왕","없음"};
    public string[] PublicOffice2 = 
        { "관노","전력부위","효력부위","수의부위","승의부위","분순부위","적순부위","병절교위","여절교위","진용교위","돈용교위",
        "창신교위","현신교위","충의교위","과의교위","선략장군","정략장군","소위장군","진위장군","보공장군","건공장군","어모장군","절충장군","가선대부","가정대부","자헌대부","정헌대부",
        "승정대부","승록대부","보국승록대부","대광보국승록대부","왕","없음"};

    //품계 업그레이드에 필요한 재화 수치
    public int[] PeerageCost1 = 
        { 100, 500, 1000, 5000, 10000, 50000, 100000, 500000, 1000000, 5000000, 10000000, 35000000, 75000000, 100000000, 300000000, 500000000, 700000000, 1000000000, 0 };
    public int[] PeerageCost2 =
        { 1, 3, 5, 7, 10, 30, 50, 70, 100, 300, 500, 700, 1000, 3000, 5000, 7000, 10000, 50000, 0 };
    public int[] PeerageCost3 = 
        { 1, 3, 5, 7, 10, 30, 50, 70, 100, 300, 500, 700, 1000, 3000, 5000, 7000, 10000, 50000, 0 };
    public string[] PeerageCostText1 = 
        { "100", "500", "1000", "5000", "10000", "50000", "100,000", "500,000","1,000,000","5,000,000","10,000,000","35,000,000","75,000,000","100,000,000","300,000,000","500,000,000","700,000,000","1,000,000,000", "최대 작위",};
    public string[] PeerageCostText2 =
        { "1", "3", "5", "7", "10", "30", "50", "70","100","300","500","700","1,000","3,000","5,000","7,000","10,000","50,000", "최대 작위",};
    public string[] PeerageCostText3 =
        { "1", "3", "5", "7", "10", "30", "50", "70","100","300","500","700","1,000","3,000","5,000","7,000","10,000","50,000", "최대 작위",};

    //문반계열 업그레이드에 필요한 재화
    public int[] PublicOffice1Cost1 =
        { 100, 300, 500, 700, 1000, 3000, 5000, 7000, 10000, 15000, 35000, 55000, 77000, 100000, 150000, 450000, 700000, 990000, 1250000, 1500000, 1750000, 3000000, 5000000, 7000000, 10000000, 13000000, 16000000, 20000000, 25000000, 30000000, 50000000, 100000000, 0 };
    public int[] PublicOffice1Cost2 = 
        { 1, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 2000, 3000, 5000, 6000, 7000, 8000, 9000, 10000, 20000, 30000, 40000, 50000, 0 };
    public int[] PublicOffice1Cost3 = 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1300, 1600, 2000, 500000, 0 };
    public string[] PublicOffice1CostText1 =
        { "100", "300", "500", "700", "1,000", "3,000", "5,000", "7,000", "10,000", "15,000", "35,000", "55,000", "77,000", "100,000", "150,000", "450,000", "700,000", "990,000", "1,250,000", "1,500,000", "1,750,000", "3,000,000", "5,000,000", "7,000,000", "10,000,000", "13,000,000", "16,000,000", "20,000,000", "25,000,000", "30,000,000", "50,000,000", "100,000,000", "최대 관직" };
    public string[] PublicOffice1CostText2 = 
        { "1", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "200", "300", "400", "500", "600", "700", "800", "900", "1,000", "2,000", "3,000", "5,000", "6,000", "7,000", "8,000", "9,000", "1,0000", "20,000", "30,000", "40,000", "50,000", "최대 관직" };
    public string[] PublicOffice1CostText3 = 
        { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "200", "300", "400", "500", "600", "700", "800", "900", "1,000", "1,300", "1,600", "2,000", "500,000", "최대 관직" };

    //무반계열 업그레이드에 필요한 재화
    public int[] PublicOffice2Cost1 = 
        { 100, 300, 500, 700, 1000, 3000, 5000, 7000, 10000, 15000, 35000, 55000, 77000, 100000, 150000, 450000, 700000, 990000, 1250000, 1500000, 1750000, 3000000, 5000000, 7000000, 10000000, 13000000, 16000000, 20000000, 25000000, 30000000, 50000000, 100000000, 0 };
    public int[] PublicOffice2Cost2 = 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1300, 1600, 2000, 500000, 0 };
    public int[] PublicOffice2Cost3 = 
        { 1, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 2000, 3000, 5000, 6000, 7000, 8000, 9000, 10000, 20000, 30000, 40000, 500000, 0 };
    public string[] PublicOffice2CostText1 = 
        { "100", "300", "500", "700", "1,000", "3,000", "5,000", "7,000", "10,000", "15,000", "35,000", "55,000", "77,000", "100,000", "150,000", "450,000", "700,000", "990,000", "1,250,000", "1,500,000", "1,750,000", "3,000,000", "5,000,000", "7,000,000", "10,000,000", "13,000,000", "16,000,000", "20,000,000", "25,000,000", "30,000,000", "50,000,000", "100,000,000", "최대 관직" };
    public string[] PublicOffice2CostText2 =
        { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "200", "300", "400", "500", "600", "700", "800", "900", "1,000", "1,300", "1,600", "2,000", "500,000", "최대 관직" };
    public string[] PublicOffice2CostText3 =
        { "1", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "200", "300", "400", "500", "600", "700", "800", "900", "1,000", "2,000", "3,000", "5,000", "6,000", "7,000", "8,000", "9,000", "1,0000", "20,000", "30,000", "40,000", "50,000", "최대 관직" };

    //품계,관직 도달시 플러스되는 초당 금화
    public int[] GetPeerageSilver = 
        { 1, 3, 5, 10, 15, 30, 100, 300, 500, 1000, 3000, 5000, 10000, 30000, 50000, 100000, 300000, 500000, 1000000 };
    public int[] GetPublicOfficeSilver = 
        { 1, 3, 5, 10, 15, 30, 50, 100, 150, 300, 500, 700, 1000, 1500, 2500, 3500, 5000, 7000, 10000, 15000, 30000, 50000, 70000, 100000, 150000, 300000, 500000, 700000, 1000000, 1500000, 3000000, 5000000 };


    public int PeerageCount = 0;
    public int PublicOfficeCount = 0;
    public bool PublicOfficeCheck1 = false;
    public bool PublicOfficeCheck2 = false;

    public int PublicOfficeCountCheck = 0;
    public bool PeerageCountCheck = false;

    public int PeerageUpgragdeCheck = 1;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
