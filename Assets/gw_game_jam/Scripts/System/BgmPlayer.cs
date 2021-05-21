using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Cysharp;
using Cysharp.Threading.Tasks;
using UniRx;

namespace gw_game_jam.Systems
{
    /// <summary>
    /// BGM再生するやつ.
    /// </summary>
    public class BgmPlayer : MonoBehaviour
    {

        /// <summary>
        /// 戦闘BGMをフェードインするときの所要時間.
        /// </summary>
        [SerializeField] private float fadeInBattleBgmTime = 1f;

        /// <summary>
        /// 戦闘BGMをフェードアウトするときの所要時間.
        /// </summary>
        [SerializeField] private float fadeOutBattleBgmTime = 1f;
        
        /// <summary>
        /// 環境音BGMの最大値.
        /// </summary>
        [SerializeField, Range(0f, 1f)] private float environmentBgmMaxVolume = 0.5f;
        
        /// <summary>
        /// 戦闘BGMの最大値.
        /// </summary>
        [SerializeField, Range(0f, 1f)] private float battleBgmMaxVolume = 0.5f;

        [SerializeField] private AudioSource environmentBgmSource;
        [SerializeField] private AudioSource battleBgmBgmSource;
        
        
        private void Start()
        {
            environmentBgmSource.volume = environmentBgmMaxVolume;
            environmentBgmSource.loop = true;
            
            battleBgmBgmSource.volume = 0f;
            battleBgmBgmSource.loop = true;

            // TODO : とりあえず今はここ、後でメインシーン操作用スクリプトから操作するようにする.
            RequestPlayBattleBgm();

            // TODO : とりあえず今はこう、後でメインシーン操作用スクリプトから操作するようにする.
            //Observable.Timer(TimeSpan.FromMilliseconds(10 * 1000)).Subscribe(_ => RequestStopBattleBgm());
        }


        /// <summary>
        /// 引数で渡したstartTimeからどれくらい経過したかを取得.
        /// 渡すのは Time.realtimeSinceStartup これ.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        private float GetElapsedTime(float startTime) =>(Time.realtimeSinceStartup - startTime);


        /// <summary>
        /// 一定時間で戦闘用BGMをフェードインさせる.
        /// </summary>
        private async UniTask PlayBattleBgm()
        {
            battleBgmBgmSource.Play();
            float startTime = Time.realtimeSinceStartup;
            while (battleBgmBgmSource.volume < battleBgmMaxVolume)
            {
                float elapsedTimeRatio = GetElapsedTime(startTime) / fadeInBattleBgmTime;
                battleBgmBgmSource.volume = 1f <= elapsedTimeRatio ? battleBgmMaxVolume : battleBgmMaxVolume * elapsedTimeRatio;
                await UniTask.DelayFrame(1);
            }
        }


        /// <summary>
        /// 一定時間で戦闘用BGMをフェードアウトさせる.
        /// </summary>
        /// <returns></returns>
        private async UniTask StopBattleBgm()
        {
            float startTime = Time.realtimeSinceStartup;
            while (0 < battleBgmBgmSource.volume)
            {
                float elapsedTimeRatio = 1f - (GetElapsedTime(startTime) / fadeOutBattleBgmTime);
                battleBgmBgmSource.volume = elapsedTimeRatio <= 0f? 0f : battleBgmMaxVolume * elapsedTimeRatio;
                await UniTask.DelayFrame(1);
            }
            battleBgmBgmSource.Stop();
        }


        /// <summary>
        /// 戦闘用BGMの再生を依頼.
        /// </summary>
        public async void RequestPlayBattleBgm()
        {
            await PlayBattleBgm();
        }


        /// <summary>
        /// 戦闘用BGMの停止を依頼.
        /// </summary>
        public async void RequestStopBattleBgm()
        {
            await StopBattleBgm();
        }

    }
}


    