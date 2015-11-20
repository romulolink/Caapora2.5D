﻿using UnityEngine;
using System.Collections;
using IsoTools;

public class SpreadFrame : MonoBehaviour {

    private float spreadTime;
    private GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");


        spreadTime = 10f;  
        StartCoroutine(multiplyFrame());
    }
	
	// Update is called once per frame
	void Update () {
	

	}


    

    // Romulo Lima
    // Multiplica um elemento para seus vizinhos
    public IEnumerator multiplyFrame()
    {

        
        //pega a posicao do fogo
        IsoObject current_frame = GetComponent<IsoObject>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // exclui a própria posição    
                if (x == 0 && y == 0)
                    continue;
                

                StartCoroutine(createNewFlame(current_frame, y, x));

               
                 yield return new WaitForSeconds(spreadTime);


            }
        }

               
    }


    // Romulo Lima
    // Multiplica um elemento para seus vizinhos
    public IEnumerator multiplyFrameInLine()
    {

        //pega a posicao do fogo
        IsoObject current_frame = GetComponent<IsoObject>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // exclui a própria posição    
                if (x == 0 && y == 0)
                    continue;


                StartCoroutine(createNewFlame(current_frame, x, 1));


                yield return new WaitForSeconds(spreadTime);


            }
        }


    }



    IEnumerator createNewFlame(IsoObject current_frame,int x, int y)
    {


       // var frame = Instantiate(Resources.Load("Prefabs/chamas")) as GameObject;
       
        var frame = ObjectPool.instance.GetAllObjectsForType("chamasSemSpread", true);

        // Adiciona em tempo de execucao para ganhar performance
        frame.GetComponent<IsoBoxCollider>().enabled = true;
        frame.GetComponent<IsoRigidbody>().enabled = true;

        frame.GetComponent<IsoRigidbody>().mass = 0.01f;

        frame.GetComponent<IsoObject>().position =
            new Vector3((current_frame.positionX + x), (current_frame.positionY + y), 0);

        yield return null;


    }

 

}
