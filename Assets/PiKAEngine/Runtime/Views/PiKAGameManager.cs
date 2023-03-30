using UnityEngine;
using AnnulusGames.LucidTools.Inspector;
using JuhaKurisu.PiKAEngine.Logics;

namespace JuhaKurisu.PiKAEngine.Views
{
    [HideMonoScript, AddComponentMenu("PiKAEngine/PiKA GameManager")]
    public class PiKAGameManager : MonoBehaviour
    {
        [SerializeField, LabelText("Game Settings"), Required] private GameSettings settings;
    }
}
