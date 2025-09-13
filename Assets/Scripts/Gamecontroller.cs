using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    public GameObject Bird;
    public float Gravity = 30;
    public float JumpSpeed = 10;

    private float VerticalSpeed;
    private float spawnTimer;
    public float spawnInterval = 2f;
    

    private float centerPointY = 0f;
    private float heightVariation = 3f;
    private float spawnPosZ = 10f;
    public GameObject pipeGatePrefab;
    private List<GameObject> activePipeGates = new List<GameObject>();
    private float pipeSpeed = 10f;
    private float destroyPointZ = -15f;

    // Start is called before the first frame update
    
    // Update is called once per frame
    private void Start()
    {
        pipecount = 0;
        Destroy(pipeGatePrefab);

        VerticalSpeed -= Gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            VerticalSpeed = 0;
            VerticalSpeed += JumpSpeed;
        }
        Bird.transform.position += Vector3.up * VerticalSpeed * Time.deltaTime;

        VerticalSpeed = 0;
        Bird.transform.position = new Vector3(0, 11, -3.5f);

        

    }


    public int pipecount { get; private set; }

    // Merge the two Update methods into one to fix CS0111
    private void Update()
    {
        // Bird movement and jump logic
        VerticalSpeed -= Gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            VerticalSpeed = 0;
            VerticalSpeed += JumpSpeed;
        }
        Bird.transform.position += Vector3.up * VerticalSpeed * Time.deltaTime;

        // Pipe spawning and movement logic
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnPipe();
        }
        MoveAndDestroyPipes();
    }



    private void SpawnPipe()
    {
        float randomY = centerPointY + UnityEngine.Random.Range(-heightVariation, heightVariation);
        //randomY = 0+Random(-3,3)
        Vector3 spawnPosition = new Vector3(0, randomY, spawnPosZ);
        GameObject pipe = Instantiate(pipeGatePrefab, spawnPosition, Quaternion.identity);
        activePipeGates.Add(pipe);
    }

    void MoveAndDestroyPipes()
    {
        for (int i = activePipeGates.Count - 1; i >= 0; i--)
        {
            GameObject pipeGate = activePipeGates[i];
            pipeGate.transform.position += Vector3.back * pipeSpeed * Time.deltaTime;
            if (pipeGate.transform.position.z < destroyPointZ)
            {
                activePipeGates.RemoveAt(i);
                Destroy(pipeGate);
            }
        }

    }

    private void OnTriggerEnter(Collider pipeGatePrefab)
    {
        Start();
    }
 
}


