using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject spawnpoint1;
    [SerializeField] private GameObject spawnpoint2;
    [SerializeField] private GameObject spawnpoint4;
    [SerializeField] private GameObject spawnpoint3;

    [Header("Car prefabs")]
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject carPrefab1;
    [SerializeField] private GameObject carPrefab2;

    private List<GameObject> spawnpoints = new List<GameObject>();
    private List<GameObject> carprefabs = new List<GameObject>();


    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float nextWaveTime;
    [SerializeField] private float czasGry;
    [SerializeField] private float speedIncremetor;
    [SerializeField] private float timeDecreaser;
    [SerializeField] private float pollution = 0f;


    
    [SerializeField] private GameObject helpCanv;
    [SerializeField] private GameObject winCanv;
    [SerializeField] private GameObject losCanv;
    [SerializeField] private GameObject initCanv;


    
    [SerializeField] private string nazwaLewelaUnikatowa;
    [Header("GameOver Canvas")]
    [SerializeField] private GameObject BiggerText;
    [SerializeField] private GameObject SmallerText;

    [Header("Win Canvas")]
    [SerializeField] private GameObject BiggerTextWin;
    [SerializeField] private GameObject SmallerTextWin;

    [Header("Canvas")]
    [SerializeField] private GameObject timeleftTXT;
    [SerializeField] private GameObject addedTime;
    [SerializeField] private Image pollutionBar;

    public bool gameIsActive = true;
    private float timeToRespawnNewOne;



    private string s;/// Tu do canvasa stringi
    private string m;
    Button resumet;


    void Start()
    { Time.timeScale = 1;
        //lista
        spawnpoints.Add(spawnpoint1);
        spawnpoints.Add(spawnpoint2);
        spawnpoints.Add(spawnpoint3);
        spawnpoints.Add(spawnpoint4);

        carprefabs.Add(carPrefab);
        carprefabs.Add(carPrefab1);
        carprefabs.Add(carPrefab2);


        gameIsActive = true;
       

        //huja warte sety

        helpCanv.SetActive(false);
        winCanv.SetActive(false);
        losCanv.SetActive(false);
        initCanv.SetActive(false);
        addedTime.GetComponent<CanvasRenderer>().SetAlpha(0);
        pollutionBar.fillAmount = 0f;


        timeToRespawnNewOne = nextWaveTime;


        
        if (!PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {
            PlayerPrefs.SetInt(nazwaLewelaUnikatowa, 1);
            openInitial();
            resumet = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();
        }
        else { populateCars(); };

      
        



        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InitialHelp.finished == true && PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {
            resumet.onClick.AddListener(closeInitial);
            InitialHelp.finished = false;
        }//Obowiązkowy dla helpa


        if (gameIsActive)
        {
            czasGry -= Time.deltaTime;
        }// czas dla licznika

        float minutes = Mathf.Floor(czasGry / 60);
        float seconds = Mathf.RoundToInt(czasGry % 60);

        if (minutes < 10)
        {
            m = "0" + minutes.ToString();
        }
        else
        {
            m = minutes.ToString();
        }

        if (seconds < 10)
        {
            s = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            s = Mathf.RoundToInt(seconds).ToString();
        }


        timeleftTXT.GetComponent<Text>().text = m + ":" + s;

        //koniec czasu dla licznika






        if (czasGry < 0)
        {
            Victory();
            gameIsActive = false;
            Time.timeScale = 0;

        } // upływ czasu


        if (pollution >= 1) Defeat(false);

        if (gameIsActive)
        {

            /// Respienie nowego itemka
            timeToRespawnNewOne = timeToRespawnNewOne - Time.deltaTime;
            if (timeToRespawnNewOne < 0)
            {
                Debug.Log("from updejt");
                updateSpeedAndIntervals();
                populateCars();
                timeToRespawnNewOne = nextWaveTime;
            }
        }

    }

    //Korygowanie wartości w trakcie rozgrywki
    private void updateSpeedAndIntervals()
    {
        if(minSpeed + speedIncremetor <= maxSpeed) minSpeed = minSpeed + speedIncremetor; // minimalna predkość kazdego auta
        if(nextWaveTime > 0 && nextWaveTime - timeDecreaser > 0) nextWaveTime = nextWaveTime - timeDecreaser; // odstepy miedzy respami
      

    }

    //Jeśli wysadzimy kogoś z eco samochodu, (wywołane z CarDestroyerCycleZone)
    public void addTime()
    {
        this.czasGry = this.czasGry + 5.0f;
        addedTime.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        addedTime.GetComponent<Text>().CrossFadeAlpha(0f, 1f, false);
    }


    public void Victory()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);

        gameIsActive = false;
        Time.timeScale = 0;

        winCanv.SetActive(true);
        winCanv.GetComponent<Animator>().SetBool("canAnimate", true);

        timeleftTXT.GetComponent<Text>().text = "00:00";
        SmallerTextWin.GetComponent<Text>().text = "Dobrze jest jeździć eko!";
        BiggerTextWin.GetComponent<Text>().text = "Zwycięstwo!";
       
    }

 
    public void Defeat(bool fromCarambol)
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);

        gameIsActive = false;
        Time.timeScale = 0;

        losCanv.SetActive(true);
        losCanv.GetComponent<Animator>().SetBool("canAnimate", true);

        timeleftTXT.GetComponent<Text>().text = "00:00";
        SmallerText.GetComponent<Text>().text = "Zabrakło " + m + ":" + s;
        if(fromCarambol) BiggerText.GetComponent<Text>().text = "Pojazdy się zderzyły!";
        else BiggerText.GetComponent<Text>().text = "Powietrze zanieczyszczone!";


    }


    private void populateCars()
    {

        if (gameIsActive)
        {
            int numberOfCars = Random.Range(1, spawnpoints.Count + 1); // ilość aut do zrespienia
            List<int> used = new List<int>(); // żeby się nie zdublowały

            for (int i = 0; i < numberOfCars; i++) // głowny loop dla kazdego z aut
            {

                //
                int randomSpawnpointIndex = Random.Range(0, spawnpoints.Count);
                if (used.Contains(randomSpawnpointIndex))
                {
                    randomSpawnpointIndex = 0;
                    while (used.Contains(randomSpawnpointIndex))
                    {
                        randomSpawnpointIndex++;
                    }
                }//Szukanie wolnego indexu dla samochodu
                used.Add(randomSpawnpointIndex);// zapisanie go zeby sie niezdublował w głownym loopie


                if (spawnpoints[randomSpawnpointIndex].transform.position.x < 0) //Left direction
                {
                    GameObject car = Instantiate(carPrefab, spawnpoints[randomSpawnpointIndex].transform.position, spawnpoints[randomSpawnpointIndex].transform.rotation);
                    SingleCarController singleCarController = car.GetComponent<SingleCarController>();/// pozmieniam
                    car.GetComponent<SingleCarController>().setDir(true);
                    car.GetComponent<SingleCarController>().setController(gameObject);
                    car.GetComponent<SingleCarController>().setSpeed(Random.Range(minSpeed, maxSpeed));
                    car.GetComponent<SpriteRenderer>().flipX = false;
                    car.gameObject.tag = "VehicleLeft";


                }
                else // right dir
                {
                    GameObject car = Instantiate(carPrefab, spawnpoints[randomSpawnpointIndex].transform.position, spawnpoints[randomSpawnpointIndex].transform.rotation);
                    car.GetComponent<SingleCarController>().setSpeed(Random.Range(minSpeed, maxSpeed));
                    car.GetComponent<SingleCarController>().setController(gameObject);
                    car.gameObject.tag = "VehicleRight";
                }


                //Jeśli pojawi się więcej tych autek to ten kod będzie działac dla arraya prefabów, każdy prefab w zasadzie identyczny tylko chodzi o to żeby miał różne spriteRenderery i boxCollidery

                /*
                int typeOfCar = Random.Range(0, carprefabs.Count);      

                if (spawnpoints[randomSpawnpointIndex].transform.position.x < 0) //Left direction
                {
                    GameObject car = Instantiate(carprefabs[typeOfCar], spawnpoints[randomSpawnpointIndex].transform.position, spawnpoints[randomSpawnpointIndex].transform.rotation);
                    car.GetComponent<SingleCarController>().setDir(true);
                    car.GetComponent<SingleCarController>().setController(gameObject);
                    car.GetComponent<SingleCarController>().setSpeed(Random.Range(minSpeed, maxSpeed));
                    car.GetComponent<SpriteRenderer>().flipX = false;


                }
                else // right dir
                {
                    GameObject car = Instantiate(carprefabs[typeOfCar], spawnpoints[randomSpawnpointIndex].transform.position, spawnpoints[randomSpawnpointIndex].transform.rotation);
                    car.GetComponent<SingleCarController>().setSpeed(Random.Range(minSpeed, maxSpeed));
                    car.GetComponent<SingleCarController>().setController(gameObject);
                }
                 
                 
             
             */
              





            }
        }


    }


    public void increasePollution(float i)
    {
        if (gameIsActive)
        {
            this.pollution = this.pollution + i;
            pollutionBar.fillAmount = pollution;
        }
    }


    //Standardowe funkcje dla helpa
    public void openHelp()
    {

        if (gameIsActive)
        {

            helpCanv.SetActive(true);
            helpCanv.GetComponent<Animator>().SetBool("canAnimate", true);
            SetterPause.CanStart = true;
            Time.timeScale = 0;
            gameIsActive = false;
            
        }

    }

    public void closeHelp()
    {
        SetterPause.wantToClose = true;
        SetterPause.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        gameIsActive = true;
        helpCanv.GetComponent<Animator>().SetBool("canHide", true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        helpCanv.SetActive(false);

    }


    public void openInitial()
    {
        initCanv.SetActive(true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        InitialHelp.CanStart = true;
        Time.timeScale = 0;

        gameIsActive = false;

        Debug.Log("Opened");
    }


    public void closeInitial()
    {
        InitialHelp.wantToClose = true;
        InitialHelp.message.text = "";
        Time.timeScale = 1;
        // opcjonalne 
       
        gameIsActive = true;
 
        initCanv.GetComponent<Animator>().SetBool("canHide", true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        initCanv.SetActive(false);
      

    }

    

}
