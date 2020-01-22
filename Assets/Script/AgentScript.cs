using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Navigation 관련 기능을 사용할 때 필요
using UnityEngine.AI; 

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 locate; //목표 지점 벡터
    float time = 0;
    // Use this for initialization
    void Start ()
    {
        var target = GameObject.Find("Destination").GetComponent<AgentDestination>();
        locate = target.destination;
        //해당 개체의 NavMeshAgent를 참조
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        GameObject AgentLoc = GameObject.Find("Agent(move)");
        GameObject startPoint = GameObject.Find("StartPoint");
        if (time <= 0.2f)
        {
            AgentLoc.transform.position = startPoint.transform.position;
        }
        else
        {
            //매 프레임마다 목표지점으로 이동
            agent.SetDestination(locate);
        }
	}
}
