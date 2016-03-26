using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AsteroidSpamer : MonoBehaviour {

    [SerializeField]
    private GameObject caja_preguntas;

    // Objeto asteroide
    public GameObject asteroid;	

	// Cantidad de asteroides con la que se empieza 
	public int startAsteroids = 1;

	// Cantidad de puntos totales para que aparezca el siguiente asteroide
	private int points = 40;

	// Nivel inicial
	private int level = 1;

	// Contador de puntos
	private int pointCount = 0;

	// Referencia al lado izquierdo
	private int left = 0;

	// Referencia al lado derecho
	private int right = 2;

	// Referencia a la camara principal
	private Camera camara;

	// Referencia a la direccion del mapa en la que apareceran los asteroides 
	private int spamDirection;

	// Instancia que hace referencia al objeto asteroide anterior
	private GameObject asteroidInstance;

	// Vector direccion del asteroide que aparecera 
	private Vector3 asteroidDirection;

	// Vector posicion del asteroide que aparecera 
	private Vector3 spamPosition;

	// Referemcia del tamaño de la camara
	private Vector3 camCoordinates;

    [SerializeField]
    private QuestionData preguntas;


    [SerializeField]
    private Text timer;

    [SerializeField]
    private float timerInSeconds = 30;

    // Use this for initialization
    void Start () {

		camara = Camera.main;

		NotificationCenter.DefaultCenter().AddObserver(this,"AddPoints");
        NotificationCenter.DefaultCenter().AddObserver(this, "LosePoints");

        camCoordinates.x = camara.GetComponent<Camera> ().pixelWidth;
		camCoordinates.y = camara.GetComponent<Camera>().pixelHeight;
		camCoordinates.z = -1f;

		camCoordinates = camara.GetComponent<Camera>().ScreenToWorldPoint (camCoordinates);

		SpamAsteroid (startAsteroids);

	}

	void AddPoints(Notification notification){
		pointCount++;
	}

    void LosePoints(Notification notification)
    {
        if (pointCount - 10 <= 0)
        {
            pointCount = 0;
        }
        else
        {
            pointCount -= 10;
        }
        

    }

    void FixedUpdate() {

		if(points >= 50){
            Application.LoadLevel("WinScene");
        }

        if (pointCount >= (points * level))
        {
            pointCount = 0;
            level++;
            SpamAsteroid(level);
        }
    }

    private int mult = 1;

    // Update is called once per frame
    void Update () {
        if (pointCount == 5 * mult){
            continuar = false;
            caja_preguntas.gameObject.SetActive(true);
            preguntas = ScoreManager.Instance.questions.GetQuestion();
            timer = GameObject.Find("Timer").gameObject.GetComponent<Text>();
            GameObject.Find("pregunta").gameObject.GetComponent<Text>().text = preguntas.question;
            GameObject.Find("InputField").gameObject.GetComponent<InputField>().text = "";
            StartCoroutine(SetTimer());
            mult++;
            Time.timeScale = 0;

        }
	}

    IEnumerator SetTimer()
    {
        float counter = timerInSeconds;
        timer.text = string.Format("{0:00}", Mathf.FloorToInt(timerInSeconds / 60f)) + ":" + string.Format("{0:00}", Mathf.FloorToInt(timerInSeconds % 60f));
        while (counter > 0.0f)
        {
            if(continuar) counter = timerInSeconds;
            timer.text = string.Format("{0:00}", Mathf.FloorToInt(counter / 60f)) + ":" + string.Format("{0:00}", Mathf.FloorToInt(counter % 60f));
            counter -= Time.unscaledDeltaTime;
            yield return null;
        }
    }

    bool continuar = false;

    public void Reactivar()
    {
        continuar = true;
        StopCoroutine(SetTimer());
        if (preguntas.solution.ToString() == GameObject.Find("InputField/Text").gameObject.GetComponent<Text>().text)
        {
            pointCount++;
        }
        else
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "DestroyShip");
            NotificationCenter.DefaultCenter().PostNotification(this,"RemoveLive");
        }

        Time.timeScale = 1f;
        caja_preguntas.gameObject.SetActive(false);
    }

	void SpamAsteroid(int quantity){
		for (int i = 0; i < quantity; i++) {
			spamDirection = Random.Range(left, right);
			
			
			if(spamDirection == left){
				asteroidDirection.x = Random.Range(-1f, 0f);
				spamPosition.x = -1*camCoordinates.x;
			}else{
				asteroidDirection.x = Random.Range(0f, 1f);
				spamPosition.x = camCoordinates.x;
			}
			
			spamPosition.y = Random.Range(-1*camCoordinates.y, camCoordinates.y);
			spamPosition.z = -1f;
			
			asteroidDirection.y = Random.Range(-0.8f, 0.8f);
			asteroidDirection.z = 0f;
			
			asteroidInstance = Instantiate(asteroid);
			
			asteroidInstance.transform.up = asteroidDirection;
			asteroidInstance.transform.position = spamPosition;
		}
	}
}
