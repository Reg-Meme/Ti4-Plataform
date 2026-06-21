using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextShow : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI TxT;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip TWCLip;

    [SerializeField] [TextArea(3, 10)] private string FinalTxT;
    [SerializeField] private float Dur = 0.05f;

    int LastChar = 0;
    int CharAt = 0; 
     void Awake()
    {
        
        IniciarDigitacao();
    }

    public void IniciarDigitacao()
    {
        TxT.text = "";
        LastChar = 0;
        CharAt = 0;

        float duracaoTotal = FinalTxT.Length * Dur;

        DOTween.To(() => CharAt, x => CharAt = x, FinalTxT.Length, duracaoTotal).SetEase(Ease.Linear).OnUpdate(AtualizarTextoEDigitacao).SetUpdate(true);
    }

    private void AtualizarTextoEDigitacao()
    {
        TxT.text = FinalTxT.Substring(0, CharAt);
        if (CharAt > LastChar)
        {
            char LastChar = FinalTxT[CharAt - 1];
        
            if (!char.IsWhiteSpace(LastChar) && audioSource != null && TWCLip != null)
            {
                audioSource.PlayOneShot(TWCLip);
            }
            this.LastChar = CharAt;
        }
    }
}
