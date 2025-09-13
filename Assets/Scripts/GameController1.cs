using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController1 : MonoBehaviour
{
    public float Gravity = 30;
    public GameObject Bird;
    public GameObject PipePrefab;
    public GameObject WingsLeft;
    public GameObject WingsRight;
    public Text ScoreText;
    public float Jump = 10;
    public float PipeSpeed = 5;
    public float PipeSpawnInterwal = 2;
    private float VerticalSpeed;
    private float PipeSpawnCountdown;
    private GameObject PipesHolder;
    private int PipeCount;
    private int Score;


    void Start()
    {
        Score = 0;
        ScoreText.text = Score.ToString();


        PipeCount = 0;
        Destroy(PipesHolder);
        PipesHolder = new GameObject("PipesHolder");
        PipesHolder.transform.parent = this.transform;

        VerticalSpeed = 0;
        Bird.transform.position = Vector3.up * 5;

        PipeSpawnCountdown = 0;

    }

    void Update()
    {



        VerticalSpeed += -Gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            VerticalSpeed = 0;
            VerticalSpeed += Jump;
        }
        Bird.transform.position += Vector3.up * VerticalSpeed * Time.deltaTime;


        PipeSpawnCountdown -= Time.deltaTime;

        if (PipeSpawnCountdown <= 0)
        {
            PipeSpawnCountdown = PipeSpawnInterwal;

            GameObject pipe = Instantiate(PipePrefab);
            pipe.transform.parent = PipesHolder.transform;
            pipe.transform.name = (++PipeCount).ToString();

            pipe.transform.position += Vector3.right * 30;
            pipe.transform.position += Vector3.up * Mathf.Lerp(2, 7, Random.value);
        }

        PipesHolder.transform.position += Vector3.left * PipeSpeed * Time.deltaTime;

        float speedTo01Range = Mathf.InverseLerp(-10, 10, VerticalSpeed);
        float noseAngle = Mathf.Lerp(-30, 30, speedTo01Range);
        Bird.transform.rotation = Quaternion.Euler(Vector3.left * noseAngle + Vector3.up * 120);

        float flapspeed = (VerticalSpeed > 0) ? 30 : 5;
        float angle = Mathf.Sin(Time.time * flapspeed) * 45;
        WingsLeft.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
        WingsRight.transform.localRotation = Quaternion.Euler(Vector3.back * angle);

        foreach (Transform pipe in PipesHolder.transform)
        {
            if (pipe.position.x < 0)
            {
                int pipeID = int.Parse(pipe.name);
                if (pipeID > Score)
                {
                    Score = pipeID;
                    ScoreText.text = Score.ToString();
                }
            }
            if (pipe.position.x < -35)
            {
                Destroy(pipe.gameObject);
            }


        }

    }

    private void OnTriggerEnter(Collider collider)
    {


        Start();


    }

}