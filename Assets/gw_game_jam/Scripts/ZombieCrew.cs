using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GWGameJam
{
    /// <summary>
    /// ゾンビ乗組員.
    /// 単純にアニメーションを切り替えるだけ.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class ZombieCrew : MonoBehaviour
    {

        private Animator animator;        

        
        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetInteger("AnimId", Random.Range(0, 4));
        }

        private void Update()
        {
            
        }
    }
}

    