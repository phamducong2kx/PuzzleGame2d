using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement Instance;
    public AsyncOperationHandle<IList<GameObject>> preloadHandle;
    public Dictionary<string, AudioSource> dicAudio = new Dictionary<string, AudioSource>();
    public List<AudioSource> listAudioSource = new List<AudioSource>();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {

    }
    private void OnDestroy()
    {
        Addressables.Release(preloadHandle);
    }
    //hàm này sẽ dc gọi ở levelSelect;
    public void ClearAudioSource()
    {
        if (listAudioSource.Count == 0) return;
        foreach (var x in listAudioSource)
        {
            // mỗi x là 1 audio source, cho nó về pool
            // if (x.gameObject.activeSelf == false)
            StopSound(x);
        }
        //clear list
        listAudioSource.Clear();
    }

    //hàm đuywa tất cả âm thanh lên tam
    public async Task PushSoundOnRam(string labelOrAddress)
    {
        //goi ham load aset
        preloadHandle = Addressables.LoadAssetsAsync<GameObject>(labelOrAddress, null);
        await preloadHandle.Task;
        if (preloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            //duyeetj danh sách các prefab đã đưa lên ram
            foreach (var x in preloadHandle.Result)
            {
                if (!dicAudio.ContainsKey(x.name))
                {
                    var audioSource = x.GetComponent<AudioSource>();
                    dicAudio[x.name] = audioSource;
                }
            }
            Debug.Log("Đưa tất cả cac ssoudn lên ram thành công");
        }
        else
        {
            Debug.Log("Đưa  ssoudn lên ram không thành công");
        }

    }

    //hàm tạo đối tượng
    public AudioSource SpawnObject(string key)
    {
        if (dicAudio.ContainsKey(key))
        {

            var audio = dicAudio[key];
            //spawn prefab nay 
            var obj = ObjectPooler.Instance.Spawn(audio.gameObject, new Vector3(0, 0, 0), audio.transform.rotation);
            var audioSource = obj.GetComponent<AudioSource>();
            listAudioSource.Add(audioSource);
            return audioSource;
        }
        else return null;


    }
    //hàm chơi nhạc
    public void PlaySound(string key, float volume, float delay)
    {
        //taoj gam,e object teen là audioSource
        var audioSource = SpawnObject(key);
        var prefab = ObjectPooler.Instance.GetPrefabObject(audioSource.gameObject);
        if (audioSource != null)
        {
            audioSource.volume = volume;
            audioSource.Play();

            StartCoroutine(DespawnSound(prefab, audioSource.gameObject, delay));
        }

    }
    //hamf đưa lại về pool
    public IEnumerator DespawnSound(GameObject prefab, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectPooler.Instance.Despawn(prefab, obj);
    }

    //stop ama thanh lai
    public void StopSound(AudioSource audioSource)
    {
        if (audioSource == null) return;
        //vowis audiosource này sẽ stop nó
        audioSource.Stop();
        //Đưa nó về Pool luôn khỏi nói nhiều
        //đàu tiên là tìm prefab của nó
        var prefab = ObjectPooler.Instance.GetPrefabObject(audioSource.gameObject);
        //đưa về pool
        ObjectPooler.Instance.Despawn(prefab, audioSource.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
