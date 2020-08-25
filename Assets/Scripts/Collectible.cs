using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 草莓被玩家碰撞时，检测的相关类
/// </summary>
public class Collectible : MonoBehaviour
{

    public ParticleSystem collectEffect;

    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 碰撞检测相关
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (pc != null)
        {
            if(pc.myCurrentHealth < pc.myMaxHealth)
            {
                pc.changeHealth(1);

                Instantiate(collectEffect, transform.position, Quaternion.identity);
                Audiomanager.instance.audioPlay(clip);
                Destroy(this.gameObject);
            }
        }
    }
}
