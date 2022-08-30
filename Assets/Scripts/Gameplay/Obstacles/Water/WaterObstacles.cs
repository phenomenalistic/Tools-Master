using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObstacles : MonoBehaviour
{
	public int length = 1;
	public bool finishObstacless = false;
	public GameObject water, board, trigger, boards, particleSystemsGO, finishObstaclessGO;
	public WaterMaterials.Waters waterType = WaterMaterials.Waters.water;
	public List<ParticleSystem> particleSystems = new List<ParticleSystem> { };
	public List<WaterMaterials> waterMaterials;

	private bool usePlayer = false;

    private void Awake()
    {
		waterSlapPitch = startWSP;

		if (transform.position.x > -1) { usePlayer = true; }
		else { usePlayer = false; }
    }

    public void ShowCompletedBoards(GameObject pointer)
    {
		foreach (Transform child in boards.transform)
		{
			if (child.position.z < pointer.transform.position.z)
            {
				child.gameObject.SetActive(true);
				child.parent = null;
				BoardAppearanceAnimation(child.position);
				StartCoroutine(StartBoardScaleAnimation(child.gameObject));
				StartCoroutine(StartBoardRotateAnimation(child.gameObject));
				if (usePlayer)
				{
					PlaySound();
					Vibration.Vibrate(10, true);
				}
			}
		}

		if (boards.transform.childCount == 0) { trigger.SetActive(false); }
	}

	float waterSlapPitch = 1, wsPitchStap = 0.05f, startWSP = 1.5f;
	public void PlaySound()
    {
		SoundManager.current.waterSlap.pitch = Mathf.Clamp(waterSlapPitch, startWSP, 3);
		SoundManager.current.Play(SoundManager.current.waterSlap);
		waterSlapPitch += wsPitchStap;
	}

	private void BoardAppearanceAnimation(Vector3 position)
    {
		particleSystemsGO.transform.position = position;

		foreach (ParticleSystem ps in particleSystems) { ps.Play(); }
    }

	IEnumerator StartBoardScaleAnimation(GameObject board)
	{
		Vector3 endScale = board.transform.localScale, startScale;
		float pointer = 0, speed = 3f;
		startScale = new Vector3(endScale.x * 1.2f, endScale.y, endScale.z);
		while (pointer < 1)
		{
			pointer += Time.deltaTime * speed;
			board.transform.localScale = Vector3.Lerp(startScale, endScale, pointer);
			yield return null;
		}
		board.transform.localScale = endScale;
	}

	IEnumerator StartBoardRotateAnimation(GameObject board)
    {
		float pointer = 0, amplitude = 17, speed = 4f;
		float ampl = amplitude;

		while (pointer < 1)
        {
			pointer += Time.deltaTime * speed;
			board.transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * 20f) * ampl);
			ampl = Mathf.Lerp(amplitude, 0, pointer);
			yield return null;
		}
		board.transform.eulerAngles = new Vector3(0, 0, 0);
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		if (gameObject.scene.name != null && gameObject.scene.name != gameObject.name) { InitInScene(); }
        InitInAnyCase();
	}

	public void ChangeLength()
	{
		trigger.transform.localScale = new Vector3(1, 2, length);
		float newZposition = Mathf.FloorToInt((float)length / 2f);
		newZposition = length % 2f == 0 ? newZposition - 0.5f : newZposition;
		trigger.transform.localPosition = new Vector3(0, 1.6f, newZposition);
	}


	private void InitInAnyCase()
    {
		ChangeLength();
		PutWater();
		IsFinishObstacles();
	}

	private void InitInScene()
	{
		WaterMaterials wm = new WaterMaterials();
		foreach (WaterMaterials w in waterMaterials)
		{
			if (w.name == waterType) { wm = w; break; }
		}
		RemoveOldBoards();
		PutBoards(wm);
		SetWaterMaterial(wm);
		SetPSColors(wm);
	}

	public void IsFinishObstacles()
	{
		finishObstaclessGO.GetComponent<FinishObstacles>().obstacleTag = ObstacleTag.water;
		if (finishObstacless)
		{
			transform.localScale = new Vector3(3, 3, 3);
			finishObstaclessGO.SetActive(true);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
			finishObstaclessGO.SetActive(false);
		}
	}

	private void PutWater()
    {
		water.transform.localScale = new Vector3(3, 1, length);
		water.transform.localPosition = new Vector3(0, -0.1f, (float)length / 2f - 0.5f);
    }

	private void SetWaterMaterial(WaterMaterials wm)
    {
		water.GetComponent<MeshRenderer>().material = wm.waterMaterial;
    }

	private void SetPSColors(WaterMaterials wm)
    {
		foreach (ParticleSystem ps in particleSystems)
        {
			ParticleSystem.MainModule main = ps.main;
			main.startColor = new ParticleSystem.MinMaxGradient(wm.oneColor, wm.twoColor);
		}
    }

	private void PutBoards(WaterMaterials wm)
    {
		//board.SetActive(false);
		//board.GetComponent<MeshRenderer>().material = wm.boardsMaterial;
		float space = 0.0375f, boardSize = 0.425f;
		float newPos = -0.5f;

		for (int i = 0; i < length; i++)
		{
			newPos += space;

			newPos += (boardSize / 2f);
			GameObject b1 = Instantiate(board, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), boards.transform);
			b1.transform.localPosition = new Vector3(0, 0.45f, newPos);
			b1.GetComponent<MeshRenderer>().material = wm.boardsMaterial;

			newPos += boardSize + (space * 2f);

			GameObject b2 = Instantiate(board, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), boards.transform);
			b2.transform.localPosition = new Vector3(0, 0.45f, newPos);
			b2.GetComponent<MeshRenderer>().material = wm.boardsMaterial;

			newPos += (boardSize / 2f) + space;

		}
		
    }

	

	private void RemoveOldBoards()
	{
		if (boards.transform.childCount > 0)
		{
			foreach (Transform child in boards.transform)
			{
				StartCoroutine(Destroy(child.gameObject));
			}
		}
	}

	IEnumerator Destroy(GameObject go)
	{
		yield return new WaitForEndOfFrame();
		DestroyImmediate(go);
	}


#endif
}

[System.Serializable]
public struct WaterMaterials
{
	public enum Waters
	{
		water, lava
	}

	public Material waterMaterial, boardsMaterial;
	public Color oneColor, twoColor;
	public Waters name;
}
