using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
	private Transform player;       // Player의 transform 컴포넌트를 참조할 수 있는 Reference

	public Vector2 maxXAndY;        // X와 Y 좌표로 카메라가 가질수 있는 최대값  
	public Vector2 minXAndY;        // X와 Y 좌표로 카메라가 가질수 있는 최소값   

	public float xMargin = 1f;      // 카메라가 Player의 X좌표로 이동하기 전에 체크하는 Player와 Camera의 거리 값
	public float yMargin = 1f;      // 카메라가 Player의 Y좌표로 이동하기 전에 체크하는 Player와 Camera의 거리 값

	public float xSmooth = 3f;      // 타겟이 X축으로 이동과함께 얼마나 스무스하게 카메라가 따라가야 하는지 설정 값.
	public float ySmooth = 3f;      // 타겟이 Y축으로 이동과함께 얼마나 스무스하게 카메라가 따라가야 하는지 설정 값. 


	void Awake()
	{
		// 레퍼런스(참조)를 셋팅. 
		player = GameObject.FindGameObjectWithTag("Player").transform;

	}


	bool CheckXMargin()
	{
		// 만약 X축으로 camera와 player 사이의 거리가 xMargin 보다 클 경우 true 리턴
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// 만약 Y축으로 camera와 player 사이의 거리가 yMargin 보다 클 경우 true 리턴
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	//정해진 일정 시간마다 호출 
	void LateUpdate()
	{
		TrackPlayer();
	}


	void TrackPlayer()
	{
		// 디폴트로 camera의 타겟이되는 targetX,targetY 좌표는 현재 카메라 자신의 X,Y좌표로 셋팅 
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// 만약 player가 xMargin 이상 이동했을때
		if (CheckXMargin())
			// Mathf.Lerp(a,b,c) : 선형보간법(Linear Interpolation)함수로서 a는 start값, b는 finish값 c는 factor로서 a+(b-a)*c 값을 반환
			// 시간의 흐름에 따라 자연스러럽게 변화시킬 수 있게 해주는 함수다. a,b 사이의 값을 리턴
			// targetX의 좌표값은 camera의 현재 position y 와 player의 현재 position y 사이의 Lerp 이 되야한다.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// 만약 player가 yMargin 이상 이동했을때
		if (CheckYMargin())
			// targetY의 좌표값은 camera의 현재 position y 와 player의 현재 position y 사이의 Lerp 이 되야한다.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// Mathf.Clamp() : 현재 값(targetX)을 최소(minXAndY.x)와 최대(maxXAndY.x) 사이의 값으로 고정
		// targetX와 targetY 좌표값은 최대값 보다 크거나 최소값 보다 작아서는 안된다.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// camera의 position을 자기자신의 positon z 값과 셋팅한 타겟 positoin 값들로 설정 
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}