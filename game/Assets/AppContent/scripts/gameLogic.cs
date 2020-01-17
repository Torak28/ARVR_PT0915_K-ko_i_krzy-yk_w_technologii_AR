using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Linq;


public class gameLogic : MonoBehaviour
{
    public bool ezMode;
    public bool move = true;
	bool winPlayer = false;
	bool winComputer = false;
	bool equal = false;
	public Transform[] modelPlaces;
	public GameObject[] planePlaces;
	public Transform[] modelPrefabs;
	public GameObject cylinderPrefab;

    public GameObject WinX;
    public GameObject WinO;

    public GameObject level;

    public GameObject replayButton;


    int i = 0;
	int a = 0;
	int b = 0;
	public char[] OX;
    public char[] nowaPlansza;
    GameObject[] spawnPoints;
	GameObject currentPoint;
	int index;
	public string AirClick;
	public bool Clicked = false;
	public bool hit;

	Transform X;
	Transform O;

	void Start ()
	{
		OX = new char[9];
        nowaPlansza = new char[9];
        ezMode = true;
        var xddx = level.GetComponent<Renderer>();
        xddx.material.SetColor("_Color", Color.white);
    }
	void Update ()
	{
        if (ezMode == false)
        {
            var xddx = level.GetComponent<Renderer>();
            xddx.material.SetColor("_Color", Color.red);
        }
        if (ezMode == true)
        {
            var xddx = level.GetComponent<Renderer>();
            xddx.material.SetColor("_Color", Color.white);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
		if (move) {
			if ((Input.GetMouseButtonUp (0) || Clicked) && winComputer == false && equal == false) {

				RaycastHit hitInfo = new RaycastHit ();
                if (Clicked == true)
                {
                    Debug.Log("1 if");
                    hit = true;
                }
                   

				if (Clicked == false)
                {
                    Debug.Log("2 if");
                    hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                }
                    

				if (hit) {
                    Debug.Log("3 if");
                    if (Clicked == true)
                    {
                        Debug.Log("4 if");
                        AirClick = GameObject.Find("CodeContainer").GetComponent<ProgressBar>().hitInfo.collider.name;
                    }
						
				
					if (Clicked == false)
                    {
                        Debug.Log("5 if");
                        AirClick = hitInfo.collider.name;
                    }
						

					if(AirClick == "replayButton") 
                    {
						replay();
                        move = true;
                    }

                    if(AirClick == "level")
                    {
                        ezMode = !ezMode;
                        move = true;
                    }

                    Debug.Log("Ruch gracza");
                    Debug.Log ("AirClick to " + AirClick);
                    for (int i = 0; i < 9; i++) {
						if (AirClick == planePlaces [i].name) {
							OX [i] = 'X';
							Debug.Log ("i: " + i);
							GameObject.Find (planePlaces [i].name).SetActive (false);
							X = Instantiate (modelPrefabs [0], planePlaces [i].transform.position, planePlaces [i].transform.rotation) as Transform;
							X.transform.parent = GameObject.Find ("ImageTarget").transform;
							X.name = "x" + i;
							Debug.Log ("XXXXXXXXXXXXXX:" + X);
							Vector3 pos = X.transform.position;    
							X.transform.position = new Vector3 (pos.x, pos.y + 0.4f, pos.z);
						}
					}
                    if(AirClick != replayButton.name && AirClick != level.name)
                    {
                        a++;
                        move = false;
                    } 
                    

                    if (OX [0] == 'X' && OX [1] == 'X' && OX [2] == 'X') {
						winPlayer = true;

						Debug.Log ("You Win! - 1st row " + OX [0] + " " + OX [1] + " " + OX [2]);
					}
					if (OX [3] == 'X' && OX [4] == 'X' && OX [5] == 'X') {
						winPlayer = true;
					
						Debug.Log ("You Win! - 2nd row " + OX [3] + " " + OX [4] + " " + OX [5]);
					}
					if (OX [6] == 'X' && OX [7] == 'X' && OX [8] == 'X') {
						winPlayer = true;

						Debug.Log ("You Win! - 3rd row " + OX [6] + " " + OX [7] + " " + OX [8]);
					}
					if (OX [0] == 'X' && OX [3] == 'X' && OX [6] == 'X') {
						winPlayer = true;

						Debug.Log ("You Win! - 1st column " + OX [0] + " " + OX [3] + " " + OX [6]);
					}
					if (OX [1] == 'X' && OX [4] == 'X' && OX [7] == 'X') {
						winPlayer = true;
					
						Debug.Log ("You Win! - 2nd column " + OX [1] + " " + OX [4] + " " + OX [7]);
					}
					if (OX [2] == 'X' && OX [5] == 'X' && OX [8] == 'X') {
						winPlayer = true;

						Debug.Log ("You Win! - 3rd column " + OX [2] + " " + OX [5] + " " + OX [8]);
					}
					if (OX [0] == 'X' && OX [4] == 'X' && OX [8] == 'X') {
						winPlayer = true;

						Debug.Log ("You Win! - 1st Diagonal " + OX [0] + " " + OX [4] + " " + OX [8]);
					}
					if (OX [2] == 'X' && OX [4] == 'X' && OX [6] == 'X') {
						winPlayer = true;

						Debug.Log ("You Win! - 2nd Diagonal  " + OX [2] + " " + OX [4] + " " + OX [6]);
					}
					if (a == 5 && winPlayer == false) {
						equal = true;
						Debug.Log ("Equal " + a);
					}
				}
				Clicked = false;
			}

			if (move == false && winPlayer == false && equal == false) {
				StartCoroutine (TimeDelay ());
			}
				

			if (winPlayer || winComputer || equal) {
				if (winPlayer) {
					Debug.Log ("PLAYER WINS");
                    WinX.SetActive(true);
                }
				if (winComputer) {
					Debug.Log ("COMPUTER WINS");
                    WinO.SetActive(true);
                }
				if (equal) {
					Debug.Log ("EQUAL");
                    WinX.SetActive(true);
                    WinO.SetActive(true);
                }
                replayButton.SetActive(true);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            RaycastHit hitInfo = new RaycastHit ();
			hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
            Debug.Log(hit);
            if (hit) {
                Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                if (hitInfo.collider.name == "replayButton")
                {
                    Debug.Log("tadam?");
                    replay();
                }
			}
		}
	}

	IEnumerator TimeDelay ()
	{
        Debug.Log("Ruch kompa");
		yield return new WaitForSeconds (0.4f);
		spawnPoints = GameObject.FindGameObjectsWithTag ("Plane");

        if (ezMode)
        {
            index = Random.Range(0, spawnPoints.Length);
            currentPoint = spawnPoints[index];
        }
        else 
        {
            //Ja Oni
            Debug.Log("xdddddddddddddddddddddddddddddddddddddddddd");

            nowaPlansza[6] = OX[0];
            nowaPlansza[3] = OX[1];
            nowaPlansza[0] = OX[2];
            nowaPlansza[7] = OX[3];
            nowaPlansza[4] = OX[4];
            nowaPlansza[1] = OX[5];
            nowaPlansza[8] = OX[6];
            nowaPlansza[5] = OX[7];
            nowaPlansza[2] = OX[8];

            string aktSytuacja = "/";
            foreach (char a in nowaPlansza)
            {
                if (a != 'X' && a != 'O')
                    aktSytuacja = string.Concat(aktSytuacja, '-');
                else
                    aktSytuacja = string.Concat(aktSytuacja, a);
            }
            aktSytuacja = string.Concat(aktSytuacja, "/O");

            Debug.Log("aktSytuacja " + aktSytuacja);
            string uri = "https://stujo-tic-tac-toe-stujo-v1.p.rapidapi.com" + aktSytuacja;
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-rapidapi-host", "stujo-tic-tac-toe-stujo-v1.p.rapidapi.com");
                webRequest.SetRequestHeader("x-rapidapi-key", "e2a6e5f343msh1550157023ea8a4p17ec64jsn98d6a3d86698");
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string xdd = webRequest.downloadHandler.text;
                    List<string> idontknow = xdd.Split(',').ToList<string>();
                    List<string> toMove = idontknow[2].Split(':').ToList<string>();
                    int toMoveValue = System.Int16.Parse(toMove[1]);
                    Debug.Log("toMoveValue: " + toMoveValue);

                    int odp = 0;
                    if (toMoveValue == 6)
                        odp = 0;
                    if (toMoveValue == 3)
                        odp = 1;
                    if (toMoveValue == 0)
                        odp = 2;
                    if (toMoveValue == 7)
                        odp = 3;
                    if (toMoveValue == 4)
                        odp = 4;
                    if (toMoveValue == 1)
                        odp = 5;
                    if (toMoveValue == 8)
                        odp = 6;
                    if (toMoveValue == 5)
                        odp = 7;
                    if (toMoveValue == 2)
                        odp = 8;
                    string toFind = string.Concat("Plane", odp + 1);
                    currentPoint = GameObject.Find(toFind);
                }
            }
        }
		Debug.Log ("PC: " + currentPoint.name);
		move = true;
		for (int i = 0; i < 9; i++) {
			if (currentPoint.name == planePlaces [i].name) {
				OX [i] = 'O';
				Debug.Log ("i: " + i);
				GameObject.Find (planePlaces [i].name).SetActive (false);
				O = Instantiate (modelPrefabs [1], planePlaces [i].transform.position, planePlaces [i].transform.rotation) as Transform;
				O.transform.parent = GameObject.Find ("ImageTarget").transform;
				O.name = "o" + i;
				Debug.Log(O);
				Vector3 pos = O.transform.position;    
				O.transform.position = new Vector3 (pos.x, pos.y + 0.4f, pos.z);
			}
		}
        b++;

		if (OX [0] == 'O' && OX [1] == 'O' && OX [2] == 'O') {
			winComputer = true;
		
			Debug.Log ("Computer Wins! - 1st row " + OX [0] + " " + OX [1] + " " + OX [2]);
		}
		if (OX [3] == 'O' && OX [4] == 'O' && OX [5] == 'O') {
			winComputer = true;
		
			Debug.Log ("Computer Wins! - 2nd row " + OX [3] + " " + OX [4] + " " + OX [5]);
		}
		if (OX [6] == 'O' && OX [7] == 'O' && OX [8] == 'O') {
			winComputer = true;
		
			Debug.Log ("Computer Wins! - 3rd row " + OX [6] + " " + OX [7] + " " + OX [8]);
		}
		if (OX [0] == 'O' && OX [3] == 'O' && OX [6] == 'O') {
			winComputer = true;
		
			Debug.Log ("Computer Wins! - 1st column " + OX [0] + " " + OX [3] + " " + OX [6]);
		}
		if (OX [1] == 'O' && OX [4] == 'O' && OX [7] == 'O') {
			winComputer = true;

			Debug.Log ("Computer Wins! - 2nd column " + OX [1] + " " + OX [4] + " " + OX [7]);
		}
		if (OX [2] == 'O' && OX [5] == 'O' && OX [8] == 'O') {
			winComputer = true;

			Debug.Log ("Computer Wins! - 3rd column " + OX [2] + " " + OX [5] + " " + OX [8]);
		}
		if (OX [0] == 'O' && OX [4] == 'O' && OX [8] == 'O') {
			winComputer = true;
		
			Debug.Log ("Computer Wins! - 1st Diagonal " + OX [0] + " " + OX [4] + " " + OX [8]);
		}
		if (OX [2] == 'O' && OX [4] == 'O' && OX [6] == 'O') {
			winComputer = true;

			Debug.Log ("Computer Wins! - 2nd Diagonal  " + OX [2] + " " + OX [4] + " " + OX [6]);
		}
		if (b == 5 && winComputer == false) {
			equal = true;
			Debug.Log ("Equal " + b);
		}
	}

	public void replay ()
	{
        if (GameObject.FindGameObjectsWithTag("XOdelete") != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("XOdelete").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("XOdelete")[i]);
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (!planePlaces[i].activeSelf)
                planePlaces[i].SetActive(true);
        }

        move = true;
        winPlayer = false;
        winComputer = false;
        equal = false;
        Clicked = false;
        WinX.SetActive(false);
        WinO.SetActive(false);

        a = 0;
        b = 0;

        OX = new char[9];
        nowaPlansza = new char[9];

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}