using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class 伤害数字显示 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textprefab;
    public float displayduration = 0.2f;
    public Transform spawposition;
    public 伤害数字显示(GameObject x, float y, Transform z)
    {
        textprefab = x;
        displayduration = y;
        spawposition = z;
    }
    public void deaidamage(float x)
    {
        GameObject gameObject0 = Instantiate(textprefab, spawposition.position+new Vector3(0,1.5f,1), Quaternion.identity);
        gameObject0.transform.SetParent(textprefab.transform.parent.transform, worldPositionStays: true);
        //Debug.Log(gameObject0);
        TMPro.TextMeshProUGUI damagetext = gameObject0.GetComponent<TMPro.TextMeshProUGUI>();
        //Debug.Log(textprefab.GetComponent<TextMesh>());
        damagetext.text = x.ToString();
        gameObject0.transform.localScale = Vector3.one;
        Vector3 forwarddir = Camera.main.transform.forward;
        gameObject0.transform.rotation = Quaternion.LookRotation(forwarddir);
        gameObject0.transform.DOScale(0f, displayduration).SetEase(Ease.OutBack).OnComplete(() => Destroy(gameObject0));
        
        
    }
}
