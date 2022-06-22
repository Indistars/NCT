using System.Collections;
using UnityEngine;

namespace Assets.AssetStore.Dypsloom.RhythmTimeline.Scripts
{
    public class RhythmCameraController : MonoBehaviour
    {
        [Tooltip("리듬게임이 진행될때 움직이는 카메라의 애니메이션")]
        Animator anim;
        [SerializeField] SpriteRenderer sprite;

        Coroutine imgCoroutine;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySong()
        {
            anim.SetTrigger("Start");
            imgCoroutine =  StartCoroutine(FoodImgShow());
        }

        public void EndSong()
        {
            StopCoroutine(imgCoroutine);
        }

        IEnumerator FoodImgShow()
        {
            sprite.color = new Color(1, 1, 1, 0);

            while (true)
            {
                sprite.color += new Color(1, 1, 1, 0.1f);
                yield return new WaitForSeconds(5f);
                if(sprite.color == Color.white)
                    yield break;
            }
        }
    }
}