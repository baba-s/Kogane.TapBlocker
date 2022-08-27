using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// スプライトのクリックをブロックするためのコンポーネント
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TapBlockerComponent : MonoBehaviour
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 初期化される時に呼び出されます
        /// </summary>
        private void Awake()
        {
            TapBlocker.OnChange += OnChange;
            OnChange();
        }

        /// <summary>
        /// 破棄される時に呼び出されます
        /// </summary>
        private void OnDestroy()
        {
            TapBlocker.OnChange -= OnChange;
        }

        /// <summary>
        /// クリックをブロックするかどうかが変更された時に呼び出されます
        /// </summary>
        private void OnChange()
        {
            gameObject.SetActive( TapBlocker.IsBlock );
        }
    }
}