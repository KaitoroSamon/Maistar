using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region 変数の定義

    // BGMの音量を調整
    [SerializeField, Range(0f, 1.0f)]
    private float _bgmVolume;

    // BGM音量のプロパティ
    public float BGMVolume
    {
        get
        {
            return _bgmVolume;
        }
        set
        {
            if (value < 0f)
            {
                value = 0.0f;
            }
            else if (1f < value)
            {
                value = 1.0f;
            }
            
            _bgmVolume = value;
        }
    }

    // SEの音量を調整
    [SerializeField, Range(0f, 1.0f)]
    private float _seVolume;
    
    // SE音量のプロパティ
    public float SEVolume
    {
        get
        {
            return _seVolume;
        }
        set
        {
            if (value < 0f)
            {
                value = 0.0f;
            }
            else if (1f < value)
            {
                value = 1.0f;
            }
            
            _seVolume = value;
        }
    }
    
    // Voiceの音量を調整
    [SerializeField, Range(0f, 1.0f)]
    private float _voiceVolume;
    
    // Voice音量のプロパティ
    public float VoiceVolume
    {
        get
        {
            return _voiceVolume;
        }
        set
        {
            if (value < 0f)
            {
                value = 0.0f;
            }
            else if (1f < value)
            {
                value = 1.0f;
            }
            
            _voiceVolume = value;
        }
    }

    // 音源再生用のクラス
    [SerializeField] 
    private AudioSources _audioSorces = new AudioSources();
    
    // 音源保持用のリスト
    [SerializeField]
    private AudioClips _audioClips = new AudioClips();

    // シングルトン化
    public static SoundManager instance; 

    #endregion
    
    #region クラスの定義
    
    // 音源再生用のクラス
    [Serializable]
    private class AudioSources : Audio<AudioSource>
    {
        [SerializeField] private SoundManager _soundManager;
    
        /// <summary>
        /// 音源再生所に音源をセットする
        /// </summary>
        /// <param name="audioType">音源の種類</param>
        /// <param name="audioClip">セットする音源</param>
        /// <returns>再生用のAudioSorce</returns>
       public AudioSource SetAudioSource(AudioOfType audioType, AudioClip audioClip)
        {
            AudioSource audioSource = new AudioSource();
                
            // 音源のジャンルによって以下の処理を分岐する
            switch (audioType)
            {
                // 音源のジャンルが「BGM」の時以下の処理を実行する
                case AudioOfType.BGM:
                    
                    // BGM再生用のAudioSorceを追加する
                    audioSource = SearchPlayableAudioSource(audioType, ref BGM);
                    
                    audioSource.clip = audioClip;
                    
                    break;
                // 音源のジャンルが「SYSTEMSE」の時以下の処理を実行する
                case AudioOfType.SYSTEMSE:
                    
                    // SYSTEMSE再生用のAudioSorceを追加する
                    audioSource = SearchPlayableAudioSource(audioType, ref SE.SYSTEM);
                    
                    audioSource.clip = audioClip;
                    
                        break;
                // 音源のジャンルが「ENVIRONMENTSE」の時以下の処理を実行する
                case AudioOfType.ENVIRONMENTSE:
                    
                    // ENVIRONMENTSE再生用のAudioSorceを追加する
                    audioSource = SearchPlayableAudioSource(audioType, ref SE.ENVIRONMENT);
                    
                    audioSource.clip = audioClip;
                    
                    break;
                // 音源のジャンルが「PLAYERSE」の時以下の処理を実行する
                case AudioOfType.PLAYERSE:
                    
                    // PLAYERSE再生用のAudioSorceを追加する
                    audioSource = SearchPlayableAudioSource(audioType, ref SE.PLAYER);
                    
                    audioSource.clip = audioClip;
                    
                    break;
                // 音源のジャンルが「ENEMY」の時以下の処理を実行する
                case AudioOfType.ENEMYSE:
                    
                    // ENEMYSE再生用のAudioSorceを追加する
                    audioSource = SearchPlayableAudioSource(audioType, ref SE.ENEMY);
                    
                    audioSource.clip = audioClip;
                    
                    break;
                // 音源のジャンルが「VOICE」の時以下の処理を実行する
                case AudioOfType.VOICE:
                    
                    // VOICE再生用のAudioSorceを追加する
                    audioSource = SearchPlayableAudioSource(audioType, ref VOICE);
                    
                    audioSource.clip = audioClip;
                    
                    break;
                // 音源のジャンルが「BGM」の時以下の処理を実行する
                default:
                    Debug.Log("エラー");
                    break;
            }
    
            return audioSource;
        }
    
        /// <summary>
        /// 再生準備が整っているAudioSourceを返す関数
        /// </summary>
        /// <param name="audioList">探索予定のAudioSourceのList</param>
        /// <returns>再生準備が整っているAudioSource</returns>
        private AudioSource SearchPlayableAudioSource(AudioOfType audioType,ref List<AudioSource> audioList)
        {
            // 返り値用の変数を定義
            AudioSource audioSource = new AudioSource();
            
            // 引数の配列の要素数の数だけループ処理
            for (int i = 0; i < audioList.Count; i++)
            {
                // 要素の中のAudioSourceが再生中でない場合以下の処理を実行する
                if (!audioList[i].isPlaying)
                {
                    // 返り値に再生準備が整ったAudioSourceを代入する
                    audioSource = audioList[i];
                    
                    // 返り値を返す
                    return audioSource;
                }
            }
    
            audioSource = _soundManager.AddComponent<AudioSource>();
    
            // AudioTypeによって以下の処理を分岐する
            switch (audioType)
            {
                // AudioClipが「BGM」の場合以下の処理を実行する
                case AudioOfType.BGM:
    
                    // BGM再生用配列の要素数が0以外の場合以下の処理を実行する
                    if (BGM.Count != 0)
                    {
                        // ひとつ目の再生用AudioSorceのAudioClipを更新
                        BGM[0].Stop();
                        
                        // 新規追加用のaudioSorceを削除
                        Destroy(audioSource);
                        
                        // ひとつ目の再生用audioSorceを返す
                        return BGM[0];
                    }
                    else
                    {
                        // 音量をBGM用の設定で代入する
                        audioSource.volume = _soundManager.BGMVolume;
                    
                        // 生成時に再生される設定を無効にする
                        audioSource.playOnAwake = false;
                    
                        // 自動ループを無効にする
                        audioSource.loop = true;
                    
                        // BGM再生用の配列に追加する
                        BGM.Add(audioSource); 
                    }
    
                    break;
                // AudioClipが「SE」の場合以下の処理を実行する
                case AudioOfType.SE:
                    
                    // 音量をSE用の設定で代入する
                    audioSource.volume = _soundManager.SEVolume;
                    
                    // 生成時に再生される設定を無効にする
                    audioSource.playOnAwake = false;
                    
                    // 自動ループを無効にする
                    audioSource.loop = false;
                    break;
                // AudioClipが「VOICE」の場合以下の処理を実行する
                case AudioOfType.VOICE:
                    
                    // 音量をVOICE用の設定で代入する
                    audioSource.volume = _soundManager.VoiceVolume;
                    
                    // 生成時に再生される設定を無効にする
                    audioSource.playOnAwake = false;
                    
                    // 自動ループを無効にする
                    audioSource.loop = false;
                    break;
            }
            // 返り値を返す
            return audioSource;
        }
    
        /// <summary>
        /// AudioSourceでの再生用に増設した分のAudioSourceを削除するコルーチン
        /// </summary>
        /// <param name="audioSource">増設されたAudioSource</param>
        /// <param name="audioList">増設されたAudioSourceが内包されているリスト</param>
        /// <returns></returns>
        public IEnumerator AudioSourceDelete(AudioOfType audioType,AudioSource audioSource)
        {
            // 対象Audioが再生されている限り実行する
            while (audioSource.isPlaying)
            {
                // 1フレーム待機する
                yield return null;
            }

          
                // 音源のジャンルによって以下の処理を分岐する
                switch (audioType)
                {
                    // 音源のジャンルが「BGM」の時以下の処理を実行する
                    case AudioOfType.BGM:

                        // リストから自身を除外する
                        BGM.Remove(audioSource);

                        break;
                    // 音源のジャンルが「SYSTEMSE」の時以下の処理を実行する
                    case AudioOfType.SYSTEMSE:

                        // リストから自身を除外する
                        SE.SYSTEM.Remove(audioSource);

                        break;
                    // 音源のジャンルが「ENVIRONMENTSE」の時以下の処理を実行する
                    case AudioOfType.ENVIRONMENTSE:

                        // リストから自身を除外する
                        SE.ENVIRONMENT.Remove(audioSource);

                        break;
                    // 音源のジャンルが「PLAYERSE」の時以下の処理を実行する
                    case AudioOfType.PLAYERSE:

                        // リストから自身を除外する
                        SE.PLAYER.Remove(audioSource);

                        break;
                    // 音源のジャンルが「ENEMY」の時以下の処理を実行する
                    case AudioOfType.ENEMYSE:

                        // リストから自身を除外する
                        SE.ENEMY.Remove(audioSource);

                        break;
                    // 音源のジャンルが「VOICE」の時以下の処理を実行する
                    case AudioOfType.VOICE:

                        // リストから自身を除外する
                        VOICE.Remove(audioSource);

                        break;
                    // 音源のジャンルが「BGM」の時以下の処理を実行する
                    default:
                        Debug.Log("エラー");
                        break;
                }

                // 自身を削除する
                Destroy(audioSource);
                
                yield break;
        }
    }
    
    // 音源保持用のクラス
    [Serializable]
    private class AudioClips : Audio<AudioClip>
    {
        /// <summary>
        /// 音源格納庫から音源を取得する
        /// </summary>
        /// <param name="audioType">音源のジャンル</param>
        /// <param name="index">音源のナンバリング</param>
        /// <returns></returns>
        public AudioClip GetAudioClip(AudioOfType audioType, int index)
        {
            // 引数チェック
            if (index < 0)
            {
                return null;
            }

            // 返り値用の変数を定義
            AudioClip audioClip = null;
    
            // 音源のジャンルによって以下の処理を分岐する
            switch (audioType)
            {
                // 音源のジャンルが「BGM」の時以下の処理を実行する
                case AudioOfType.BGM:
                    // 配列の要素数以下である場合以下の処理を実行する
                    if (BGM.Count > index)
                    {
                        // 返り値用の変数を指定された音源で上書きする
                        audioClip = BGM[index];
                    }
                    // 配列の要素数以上である場合以下の処理を実行する
                    else
                    {
                        // 返り値用の変数に「null」を代入する
                        audioClip = null;
                    }
                    break;
                // 音源のジャンルが「SYSTEMSE」の時以下の処理を実行する
                case AudioOfType.SYSTEMSE:
                    // 配列の要素数以下である場合以下の処理を実行する
                    if (SE.SYSTEM.Count > index)
                    {
                        // 返り値用の変数を指定された音源で上書きする
                        audioClip = SE.SYSTEM[index];
                    }
                    // 配列の要素数以上である場合以下の処理を実行する
                    else
                    {
                        // 返り値用の変数に「null」を代入する
                        audioClip = null;
                    }
                    break;
                // 音源のジャンルが「ENVIRONMENTSE」の時以下の処理を実行する
                case AudioOfType.ENVIRONMENTSE:
                    // 配列の要素数以下である場合以下の処理を実行する
                    if (SE.ENVIRONMENT.Count > index)
                    {
                        // 返り値用の変数を指定された音源で上書きする
                        audioClip = SE.ENVIRONMENT[index];
                    }
                    // 配列の要素数以上である場合以下の処理を実行する
                    else
                    {
                        // 返り値用の変数に「null」を代入する
                        audioClip = null;
                    }
                    break;
                // 音源のジャンルが「PLAYERSE」の時以下の処理を実行する
                case AudioOfType.PLAYERSE:
                    // 配列の要素数以下である場合以下の処理を実行する
                    if (SE.PLAYER.Count > index)
                    {
                        // 返り値用の変数を指定された音源で上書きする
                        audioClip = SE.PLAYER[index];
                    }
                    // 配列の要素数以上である場合以下の処理を実行する
                    else
                    {
                        // 返り値用の変数に「null」を代入する
                        audioClip = null;
                    }
                    break;
                // 音源のジャンルが「ENEMY」の時以下の処理を実行する
                case AudioOfType.ENEMYSE:
                    // 配列の要素数以下である場合以下の処理を実行する
                    if (SE.ENEMY.Count > index)
                    {
                        // 返り値用の変数を指定された音源で上書きする
                        audioClip = SE.ENEMY[index];
                    }
                    // 配列の要素数以上である場合以下の処理を実行する
                    else
                    {
                        // 返り値用の変数に「null」を代入する
                        audioClip = null;
                    }
                    break;
                // 音源のジャンルが「VOICE」の時以下の処理を実行する
                case AudioOfType.VOICE:
                    // 配列の要素数以下である場合以下の処理を実行する
                    if (VOICE.Count > index)
                    {
                        // 返り値用の変数を指定された音源で上書きする
                        audioClip = VOICE[index];
                    }
                    // 配列の要素数以上である場合以下の処理を実行する
                    else
                    {
                        // 返り値用の変数に「null」を代入する
                        audioClip = null;
                    }
                    break;
                // 音源のジャンルが「BGM」の時以下の処理を実行する
                default:
                    Debug.Log("エラー");
                    // 返り値用の変数に「null」を代入する
                    audioClip = null;
                    break;
            }
            
            return audioClip;
        }
    }
    
    // 音源の基底クラス
    [Serializable]
    protected class Audio<T>
    {
        [SerializeField] 
        protected List<T> BGM;

        [SerializeField] 
        protected SEAudio SE;

        [SerializeField] 
        protected List<T> VOICE;

        [Serializable]
        protected class SEAudio
        {
            [SerializeField]
            public List<T> SYSTEM;
            
            [SerializeField]
            public List<T> ENVIRONMENT;
            
            [SerializeField]
            public List<T> PLAYER;
            
            [SerializeField]
            public List<T> ENEMY;
        }
    }
    #endregion
    

    /// <summary>
    /// 音源の種類を列挙した配列
    /// </summary>
    public enum AudioOfType
    {
        BGM,
        SE,
        SYSTEMSE,
        ENVIRONMENTSE,
        PLAYERSE,
        ENEMYSE,
        VOICE
    }

    private void Awake()
    {
        // インスタンスが定義されていない時に以下の処理を実行
        if (instance == null)
        {
            // インスタンスを更新
            instance = this;
            
            // シーン間を持ち越す用に保護
            DontDestroyOnLoad(this.gameObject);
        }
        // インスタンスが定義済みの場合以下の処理を実行する
        else
        {
            // 自身を削除する
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     PlayAudioSorce(AudioOfType.PLAYERSE,0);
        // }
        // else if (Input.GetKeyDown(KeyCode.W))
        // {
        //     PlayAudioSorce(AudioOfType.PLAYERSE,1);
        // }
        // else if (Input.GetKeyDown(KeyCode.E))
        // {
        //     PlayAudioSorce(AudioOfType.PLAYERSE,2);
        // }
        // else if (Input.GetKeyDown(KeyCode.R))
        // {
        //     PlayAudioSorce(AudioOfType.PLAYERSE,3);
        // }
        // else if (Input.GetKeyDown(KeyCode.T))
        // {
        //     PlayAudioSorce(AudioOfType.PLAYERSE,4);
        // }
    }

    #region 関数の定義 

    /// <summary>
    /// 音源の再生用関数
    /// </summary>
    /// <param name="audioTyoe">音源の種類</param>
    /// <param name="num">音源の管理ナンバー</param>
    public void PlayAudioSorce(AudioOfType audioType, int num)
    {
        // 再生用の音源を取得する
        AudioClip audioClip = _audioClips.GetAudioClip(audioType, num);

        if (audioClip == null)
        {
            Debug.Log("オーディオクリップの取得失敗");
            return;
        }
        
        // 再生用の音源をセットした音源再生所を取得する
        AudioSource audioSource = _audioSorces.SetAudioSource(audioType, audioClip);

        if (audioSource == null)
        {
            Debug.Log("オーディオソースの取得失敗");
            return;
        }
        
        // 音源の本再生処理
        audioSource.Play();
        StartCoroutine(_audioSorces.AudioSourceDelete(audioType, audioSource));
    } 

    #endregion
    
}
