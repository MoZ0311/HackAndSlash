using UnityEngine;

/// <summary>
/// 弾丸の拡散を計算する静的クラス
/// </summary>
public static class SpreadUtility
{
    /// <summary>
    /// 均一な拡散を計算する関数
    /// </summary>
    /// <param name="bulletCount">発射する弾の数</param>
    /// <param name="spreadAngle">拡散する角度</param>
    /// <param name="baseRotation">基準となる回転(正面)</param>
    /// <returns>それぞれの弾の向きの配列</returns>
    public static Quaternion[] CalculateUniformSpread(int bulletCount, float spreadAngle, Quaternion baseRotation)
    {
        // 0発以下なら空の配列を返す
        if (bulletCount <= 0)
        {
            return System.Array.Empty<Quaternion>();
        }

        Quaternion[] rotations = new Quaternion[bulletCount];

        // 1発なら正面にそのまま発射
        if (bulletCount == 1)
        {
            rotations[0] = baseRotation;
            return rotations;
        }

        // 拡散角の左端
        float startAngle = -spreadAngle / 2;

        // 弾1発あたりの角度間隔
        float angleStep = spreadAngle / (bulletCount - 1);
        for (int i = 0; i < bulletCount; ++i)
        {
            float angle = startAngle + angleStep * i;

            // 基準方向を中心としてY軸方向へ回転
            rotations[i] = baseRotation * Quaternion.Euler(0f, angle, 0f);
        }
        return rotations;
    }

    /// <summary>
    /// ランダムな拡散を計算する関数
    /// </summary>
    /// <param name="spreadAngle">ブレの範囲</param>
    /// <param name="baseRotation">基準となる回転(正面)</param>
    /// <returns>ブレた弾の向き</returns>
    public static Quaternion CalculateRandomSpread(float spreadAngle, Quaternion baseRotation)
    {
        // 指定された範囲内でランダムなブレを作成
        float randomAngle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
        return baseRotation * Quaternion.Euler(0f, randomAngle, 0f);
    }
}
