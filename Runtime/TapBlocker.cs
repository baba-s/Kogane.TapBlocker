using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// タップをブロックするインスタンスを管理するクラス
    /// </summary>
    public sealed class TapBlocker
        : IDisposable,
          ITapBlocker
    {
        //================================================================================
        // 変数
        //================================================================================
        private bool m_isDispose; // 既に破棄されている場合 true

        //================================================================================
        // 変数(static readonly)
        //================================================================================
        private static readonly List<TapBlocker> ms_list = new(); // タップをブロックしているインスタンスのリスト

        //================================================================================
        // プロパティ
        //================================================================================
        /// <summary>
        /// インスタンス名を返します
        /// </summary>
        public string Name { get; }

        //================================================================================
        // プロパティ(static)
        //================================================================================
        /// <summary>
        /// タップをブロックしているインスタンスの数を返します
        /// </summary>
        public static int BlockCount => ms_list.Count;

        /// <summary>
        /// タップをブロックしている場合 true を返します
        /// </summary>
        public static bool IsBlock => 1 <= BlockCount;

        /// <summary>
        /// タップをブロックしているすべてのインスタンスを返します
        /// </summary>
        public static IReadOnlyList<ITapBlocker> Blockers => ms_list;

        //================================================================================
        // イベント(static)
        //================================================================================
        /// <summary>
        /// タップをブロックするかどうかが変更された時に呼び出されます
        /// </summary>
        public static Action OnChange { get; set; }

        /// <summary>
        /// タップをブロックするインスタンスが追加された時に呼び出されます
        /// </summary>
        public static Action<TapBlocker> OnAdd { get; set; }

        /// <summary>
        /// タップをブロックするインスタンスが削除された時に呼び出されます
        /// </summary>
        public static Action<TapBlocker> OnRemove { get; set; }

        /// <summary>
        /// インスタンス名を取得する時に呼び出されます
        /// </summary>
        public static Func<string> OnGetName { get; set; }

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private TapBlocker( string name )
        {
            Name = name;
            ms_list.Add( this );
            OnChange?.Invoke();
            OnAdd?.Invoke( this );
        }

        /// <summary>
        /// タップのブロックを解除します
        /// </summary>
        public void Dispose()
        {
            if ( m_isDispose ) return;
            m_isDispose = true;
            ms_list.Remove( this );
            OnChange?.Invoke();
            OnRemove?.Invoke( this );
        }

        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// タップのブロックを開始します
        /// </summary>
        public static TapBlocker Block( string name )
        {
            var blocker = new TapBlocker( name );
            return blocker;
        }

        /// <summary>
        /// タップのブロックを開始します
        /// </summary>
        public static TapBlocker Block()
        {
            var blocker = new TapBlocker( OnGetName?.Invoke() ?? "" );
            return blocker;
        }

        /// <summary>
        /// すべてのタップのブロックを解除します
        /// </summary>
        public static void Clear()
        {
            Clear( false );
        }

        /// <summary>
        /// すべてのタップのブロックを解除します
        /// </summary>
        public static void Clear( bool callEvent )
        {
            if ( !IsBlock ) return;

            var onChange = OnChange;
            OnChange = null;

            for ( var i = ms_list.Count - 1; i >= 0; i-- )
            {
                var blocker = ms_list[ i ];
                blocker.Dispose();
            }

            OnChange = onChange;

            if ( !callEvent ) return;
            OnChange?.Invoke();
        }

        /// <summary>
        /// 初回のシーンが読み込まれる前に呼び出されます
        /// </summary>
        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
        private static void RuntimeInitializeOnLoadMethod()
        {
            Clear();
        }
    }
}