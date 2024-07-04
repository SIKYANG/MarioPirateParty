using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
	private Transform player;       // Player�� transform ������Ʈ�� ������ �� �ִ� Reference

	public Vector2 maxXAndY;        // X�� Y ��ǥ�� ī�޶� ������ �ִ� �ִ밪  
	public Vector2 minXAndY;        // X�� Y ��ǥ�� ī�޶� ������ �ִ� �ּҰ�   

	public float xMargin = 1f;      // ī�޶� Player�� X��ǥ�� �̵��ϱ� ���� üũ�ϴ� Player�� Camera�� �Ÿ� ��
	public float yMargin = 1f;      // ī�޶� Player�� Y��ǥ�� �̵��ϱ� ���� üũ�ϴ� Player�� Camera�� �Ÿ� ��

	public float xSmooth = 3f;      // Ÿ���� X������ �̵����Բ� �󸶳� �������ϰ� ī�޶� ���󰡾� �ϴ��� ���� ��.
	public float ySmooth = 3f;      // Ÿ���� Y������ �̵����Բ� �󸶳� �������ϰ� ī�޶� ���󰡾� �ϴ��� ���� ��. 


	void Awake()
	{
		// ���۷���(����)�� ����. 
		player = GameObject.FindGameObjectWithTag("Player").transform;

	}


	bool CheckXMargin()
	{
		// ���� X������ camera�� player ������ �Ÿ��� xMargin ���� Ŭ ��� true ����
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// ���� Y������ camera�� player ������ �Ÿ��� yMargin ���� Ŭ ��� true ����
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	//������ ���� �ð����� ȣ�� 
	void LateUpdate()
	{
		TrackPlayer();
	}


	void TrackPlayer()
	{
		// ����Ʈ�� camera�� Ÿ���̵Ǵ� targetX,targetY ��ǥ�� ���� ī�޶� �ڽ��� X,Y��ǥ�� ���� 
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// ���� player�� xMargin �̻� �̵�������
		if (CheckXMargin())
			// Mathf.Lerp(a,b,c) : ����������(Linear Interpolation)�Լ��μ� a�� start��, b�� finish�� c�� factor�μ� a+(b-a)*c ���� ��ȯ
			// �ð��� �帧�� ���� �ڿ��������� ��ȭ��ų �� �ְ� ���ִ� �Լ���. a,b ������ ���� ����
			// targetX�� ��ǥ���� camera�� ���� position y �� player�� ���� position y ������ Lerp �� �Ǿ��Ѵ�.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// ���� player�� yMargin �̻� �̵�������
		if (CheckYMargin())
			// targetY�� ��ǥ���� camera�� ���� position y �� player�� ���� position y ������ Lerp �� �Ǿ��Ѵ�.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// Mathf.Clamp() : ���� ��(targetX)�� �ּ�(minXAndY.x)�� �ִ�(maxXAndY.x) ������ ������ ����
		// targetX�� targetY ��ǥ���� �ִ밪 ���� ũ�ų� �ּҰ� ���� �۾Ƽ��� �ȵȴ�.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// camera�� position�� �ڱ��ڽ��� positon z ���� ������ Ÿ�� positoin ����� ���� 
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}