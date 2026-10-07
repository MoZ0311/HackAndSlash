using UnityEngine;
using UnityEngine.UIElements;

namespace CustomUI
{
    // UXMLやUI Builderで認識できるように宣言
    [UxmlElement]
    public partial class RadialProgressIndicator : VisualElement
    {
        // --------------------------------------------------
        // UXML/UI BuilderやC#から調整可能なプロパティ
        // --------------------------------------------------

        // 進捗率（0.0 〜 1.0）
        private float progress = 0.5f;
        [UxmlAttribute]
        public float Progress
        {
            get => progress;
            set
            {
                // 値を 0.0 ～ 1.0 の範囲に制限
                progress = Mathf.Clamp01(value);
                // 値が変わったら再描画をリクエスト
                MarkDirtyRepaint();
            }
        }

        // ゲージの半径 (px)
        [UxmlAttribute] public float GaugeRadius { get; set; } = 40f;

        // ゲージの太さ (px)
        [UxmlAttribute] public float GaugeThickness { get; set; } = 8f;

        // 中心のドットの半径 (px)
        [UxmlAttribute] public float CenterDotRadius { get; set; } = 4f;

        // ゲージの色
        [UxmlAttribute] public Color GaugeColor { get; set; } = Color.white;

        // ゲージ背景（下地）の色
        [UxmlAttribute] public Color GaugeBackgroundColor { get; set; } = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        // 中心のドットの色
        [UxmlAttribute] public Color CenterDotColor { get; set; } = Color.white;

        // コンストラクタ（初期化処理）
        public RadialProgressIndicator()
        {
            // 描画メソッドを登録
            generateVisualContent += OnGenerateVisualContent;
        }

        // --------------------------------------------------
        // 描画ロジック（Painter2Dを使用）
        // --------------------------------------------------
        private void OnGenerateVisualContent(MeshGenerationContext context)
        {
            // 描画用ヘルパーオブジェクトを取得
            Painter2D painter = context.painter2D;

            // 要素の中心座標を計算
            Vector2 center = new(contentRect.width / 2, contentRect.height / 2);

            painter.fillColor = CenterDotColor;
            painter.BeginPath();
            // (x, y, 半径, 開始角度, 終了角度)
            painter.Arc(center, CenterDotRadius, 0f, 360f);
            painter.Fill();

            // ゲージ背景を描画
            if (GaugeThickness > 0 && GaugeRadius > 0)
            {
                painter.strokeColor = GaugeBackgroundColor;
                painter.lineWidth = GaugeThickness;

                painter.BeginPath();
                // 下地として360度の円線を描画
                painter.Arc(center, GaugeRadius, 0f, 360f);
                painter.Stroke();

                // 進捗部分を描画
                if (progress > 0)
                {
                    painter.strokeColor = GaugeColor;

                    // UI ToolkitのArc角度指定：真上は -90度 (時計回り)
                    float startAngle = -90f;
                    float endAngle = startAngle + (progress * 360f);

                    painter.BeginPath();

                    // 時計回りに進捗分の円弧を描画
                    painter.Arc(center, GaugeRadius, startAngle, endAngle);
                    painter.Stroke();
                }
            }
        }
    }
}