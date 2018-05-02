using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankSkin
{
    public int key;
    public Sprite Armour;
    public Sprite Barrel;
}

public class AppManager : MonoBehaviour
{
    [Header("SpriteRenderer")]
    [SerializeField] SpriteRenderer armourRenderer;
    [SerializeField] SpriteRenderer barrelRenderer;

    [Header("Skins")]
    [SerializeField] List<TankSkin> skins;

    [Header("Tank")]
    [SerializeField] GameObject tank;

    [Header("maps")]
    [SerializeField] List<GameObject> maps;
    

    void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.F1))
        {
            var s = skins.Where(x => x.key == 1).FirstOrDefault();
            armourRenderer.sprite = s.Armour;
            barrelRenderer.sprite = s.Barrel;

        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            var s = skins.Where(x => x.key == 2).FirstOrDefault();
            armourRenderer.sprite = s.Armour;
            barrelRenderer.sprite = s.Barrel;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            var s = skins.Where(x => x.key == 3).FirstOrDefault();
            armourRenderer.sprite = s.Armour;
            barrelRenderer.sprite = s.Barrel;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize++;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            var m = maps[0];
            m.SetActive(true);
            foreach (var x in maps.Where(x => x != m))
                x.SetActive(false);

            tank.transform.position = Vector3.zero;
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            var m = maps[1];
            m.SetActive(true);
            foreach (var x in maps.Where(x => x != m))
                x.SetActive(false);

            tank.transform.position = Vector3.zero;
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            var m = maps[2];
            m.SetActive(true);
            foreach (var x in maps.Where(x => x != m))
                x.SetActive(false);

            tank.transform.position = Vector3.zero;
        }
    }
}
