
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] cuts;
    public string nextSceneName = "Stage1";

    private int currentCut = 0;

    void Start()
    {
        // 시작할 때 모든 컷 숨기기
        foreach (GameObject cut in cuts)
        {
            if (cut != null)
            {
                cut.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 다음 컷이 남아 있다면 보여주기
            if (currentCut < cuts.Length)
            {
                if (cuts[currentCut] != null)
                {
                    cuts[currentCut].SetActive(true);
                }

                currentCut++;
            }
            else
            {
                // 모든 컷을 본 뒤 클릭하면 다음 씬으로 이동
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
