using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

public class MurderBoardScript : MonoBehaviour
{
    public GameObject[] Suspects = new GameObject[4];
    
    public GameObject[] SuspectAClues = new GameObject[4];
    public GameObject[] SuspectBClues = new GameObject[4];
    public GameObject[] SuspectCClues = new GameObject[4];
    public GameObject[] SuspectDClues = new GameObject[4];

    private List<Material> SuspectACluePictures = new List<Material>();
    private List<Material> SuspectBCluePictures = new List<Material>();
    private List<Material> SuspectCCluePictures = new List<Material>();
    private List<Material> SuspectDCluePictures = new List<Material>();
    private List<Material> SuspectCluePictures = new List<Material>();

    public GameObject[,] SuspectClues = new GameObject[4, 4];

    public GameObject[] Clues = new GameObject[20];

    public int CluesFound = 0;

    public int SuspectsFound = 0;

    public Material[] CluePictures = new Material[20];

    private List<Material> FoundCluePictures = new List<Material>();

    public Material[] SuspectPictures = new Material[4];

    private List<Material> FoundSuspectPictures = new List<Material>();

    public int[] SuspectCluesFound = new int[4];

    private List<int> removeList = new List<int>();

    public List<string> SuspectIndexLookup = new List<string>();

    public List<string> ClueIndexLookup = new List<string>();

    public bool murderBoardTesting;

    private int numClues = 20;

    private int numSuspects = 4;

    private static MurderBoardScript _instance;

    private Vector3 murderBoardPosition;

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = this;
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        foreach (GameObject clue in Clues)
        {
            clue.SetActive(false);
        }

        foreach (GameObject clue in SuspectAClues)
        {
            clue.SetActive(false);
        }

        foreach (GameObject clue in SuspectBClues)
        {
            clue.SetActive(false);
        }

        foreach (GameObject clue in SuspectCClues)
        {
            clue.SetActive(false);
        }

        foreach (GameObject clue in SuspectDClues)
        {
            clue.SetActive(false);
        }

        foreach (GameObject suspect in Suspects)
        {
            suspect.SetActive(false);
        }
        murderBoardPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "detectivesOffice" || murderBoardTesting)
        {
            this.gameObject.transform.position = murderBoardPosition;
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(0, -100, 0);
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        for (int i = 0; i < CluesFound; i++)
        {
            Clues[i].SetActive(true);
        }
        for (int j = numClues - 1; j >= CluesFound; j--)
        {
            Clues[j].SetActive(false);
        }

        for (int i = 0; i < SuspectsFound; i++)
        {
            Suspects[i].SetActive(true);
        }
        for (int j = numSuspects - 1; j >= SuspectsFound; j--)
        {
            Suspects[j].SetActive(false);
        }

        for (int k = 0; k < SuspectsFound; k++)
        {
            for (int i = 0; i < SuspectCluesFound[k]; i++)
            {
                if (k == 0)
                {
                    SuspectAClues[i].SetActive(true);
                }
                else if (k == 1)
                {
                    SuspectBClues[i].SetActive(true);
                }
                else if (k == 2)
                {
                    SuspectCClues[i].SetActive(true);
                }
                else if (k == 3)
                {
                    SuspectDClues[i].SetActive(true);
                }
            }
            for (int j = numSuspects - 1; j >= SuspectCluesFound[k]; j--)
            {
                if (k == 0)
                {
                    SuspectAClues[j].SetActive(false);
                }
                else if (k == 1)
                {
                    SuspectBClues[j].SetActive(false);
                }
                else if (k == 2)
                {
                    SuspectCClues[j].SetActive(false);
                }
                else if (k == 3)
                {
                    SuspectDClues[j].SetActive(false);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            addSuspect(0);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            addSuspect(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            addSuspect(2);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            addSuspect(3);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            addClue(0);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            addClue(1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            addClue(2);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            addClue(3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            moveClueToSuspect(0, 3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            moveClueToSuspect(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            moveClueToSuspect(3, 2);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            moveClueToSuspect(2, 0);
        }
    }

    [YarnCommand("addSuspect")]
    public void yarnAddSuspect(string suspect)
    {
        Debug.Log("Add suspect " + SuspectIndexLookup.IndexOf(suspect));
        addSuspect(SuspectIndexLookup.IndexOf(suspect));
    }

    [YarnCommand("addClue")]
    public void yarnAddClue(string clue)
    {
        Debug.Log("Add clue " + ClueIndexLookup.IndexOf(clue));
        addClue(ClueIndexLookup.IndexOf(clue));
    }

    [YarnCommand("moveClueToSuspect")]
    public void yarnMoveClueToSuspect(string clue, string suspect)
    {
        Debug.Log("move clue " + ClueIndexLookup.IndexOf(clue) + " to suspect " + SuspectIndexLookup.IndexOf(suspect));
        moveClueToSuspect(ClueIndexLookup.IndexOf(clue), SuspectIndexLookup.IndexOf(suspect));
    }


    public void addSuspect(int suspectNum)
    {
        bool allowAddSuspect = true;
        foreach (Material suspect in FoundSuspectPictures)
        {
            if (suspect == SuspectPictures[suspectNum])
            {
                allowAddSuspect = false;
            }
        }
        if (allowAddSuspect)
        {
            FoundSuspectPictures.Add(SuspectPictures[suspectNum]);
            int i = 0;
            foreach (Material suspect in FoundSuspectPictures)
            {
                Suspects[i].GetComponent<Renderer>().material = suspect;
                i++;
            }
            SuspectsFound = FoundSuspectPictures.Count;
        }
    }

    public void removeSuspect(int suspectNum)
    {
        FoundSuspectPictures.Remove(SuspectPictures[suspectNum]);
        int i = 0;
        foreach (Material suspect in FoundSuspectPictures)
        {
            Suspects[i].GetComponent<Renderer>().material = suspect;
            i++;
        }
        SuspectsFound = FoundSuspectPictures.Count;
    }

    
    public void addClue(int clueNum)
    {
        bool allowAddClue = true;
        foreach (Material clue in FoundCluePictures)
        {
            if (clue == CluePictures[clueNum])
            {
                allowAddClue = false;
            }
        }
        foreach (Material clue in SuspectCluePictures)
        {
            if (clue == CluePictures[clueNum])
            {
                allowAddClue = false;
            }
        }
        if (allowAddClue)
        {
            FoundCluePictures.Add(CluePictures[clueNum]);
            int i = 0;
            foreach (Material clue in FoundCluePictures)
            {
                Clues[i].GetComponent<Renderer>().material = clue;
                i++;
            }
            CluesFound = FoundCluePictures.Count;
        }
    }

    public void removeClue(int clueNum)
    {
        FoundCluePictures.Remove(CluePictures[clueNum]);
        int i = 0;
        foreach (Material clue in FoundCluePictures)
        {
            Clues[i].GetComponent<Renderer>().material = clue;
            i++;
        }
        CluesFound = FoundCluePictures.Count;
    }

    
    public void moveClueToSuspect(int clueNum,int suspectNum)
    {
        foreach (Material clue in FoundCluePictures)
	    {
	        if (clue == CluePictures[clueNum])
	        {
                removeList.Add(clueNum);
                Debug.Log("Add clue to remove list");
                switch (FoundSuspectPictures.IndexOf(SuspectPictures[suspectNum]))
                {
                    case 0:
                        Debug.Log("case 0 suspect");
                        SuspectACluePictures.Add(CluePictures[clueNum]);
                        SuspectCluePictures.Add(CluePictures[clueNum]);
                        int i = 0;
                        foreach (Material suspectClue in SuspectACluePictures)
                        {
                            SuspectAClues[i].GetComponent<Renderer>().material = suspectClue;
                            Debug.Log("add material to suspect 0 clue");
                            i++;
                        }
                        SuspectCluesFound[FoundSuspectPictures.IndexOf(SuspectPictures[suspectNum])] = SuspectACluePictures.Count;
                        break;
                    case 1:
                        Debug.Log("case 1 suspect");
                        SuspectBCluePictures.Add(CluePictures[clueNum]);
                        SuspectCluePictures.Add(CluePictures[clueNum]);
                        int j = 0;
                        foreach (Material suspectClue in SuspectBCluePictures)
                        {
                            SuspectBClues[j].GetComponent<Renderer>().material = suspectClue;
                            Debug.Log("add material to suspect 1 clue");
                            j++;
                        }
                        SuspectCluesFound[FoundSuspectPictures.IndexOf(SuspectPictures[suspectNum])] = SuspectBCluePictures.Count;
                        break;
                    case 2:
                        Debug.Log("case 2 suspect");
                        SuspectCCluePictures.Add(CluePictures[clueNum]);
                        SuspectCluePictures.Add(CluePictures[clueNum]);
                        int k = 0;
                        foreach (Material suspectClue in SuspectCCluePictures)
                        {
                            SuspectCClues[k].GetComponent<Renderer>().material = suspectClue;
                            Debug.Log("add material to suspect 2 clue");
                            k++;
                        }
                        SuspectCluesFound[FoundSuspectPictures.IndexOf(SuspectPictures[suspectNum])] = SuspectCCluePictures.Count;
                        break;
                    case 3:
                        Debug.Log("case 3 suspect");
                        SuspectDCluePictures.Add(CluePictures[clueNum]);
                        SuspectCluePictures.Add(CluePictures[clueNum]);
                        int l = 0;
                        foreach (Material suspectClue in SuspectDCluePictures)
                        {
                            SuspectDClues[l].GetComponent<Renderer>().material = suspectClue;
                            Debug.Log("add material to suspect 3 clue");
                            l++;
                        }
                        SuspectCluesFound[FoundSuspectPictures.IndexOf(SuspectPictures[suspectNum])] = SuspectDCluePictures.Count;
                        break;
                }
                
	        }
	    }
        foreach (int clue in removeList)
        {
            removeClue(clue);
        }
    }

}
