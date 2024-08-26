
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private float RandomValue => Random.value;

    public GameObject Spawn(GameObject preFab, Vector3 position, Vector3 scale)
    {
        GameObject newCube = Instantiate(preFab, position, Quaternion.identity);

        newCube.transform.localScale = scale;

        Renderer cubeRenderer = newCube.GetComponent<Renderer>();
        cubeRenderer.material.color = new Color(RandomValue, RandomValue, RandomValue);

        return newCube;
    }
}
