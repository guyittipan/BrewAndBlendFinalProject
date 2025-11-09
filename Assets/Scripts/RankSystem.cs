
using UnityEngine;

public class RankSystem : MonoBehaviour {
    [SerializeField] private int bronze = 200;
    [SerializeField] private int silver = 400;
    [SerializeField] private int gold = 600;
    [SerializeField] private int platinum = 800;

    public string GetRank(int score) {
        if (score >= platinum) return "Platinum";
        if (score >= gold) return "Gold";
        if (score >= silver) return "Silver";
        if (score >= bronze) return "Bronze";
        return "Unranked";
    }
}
