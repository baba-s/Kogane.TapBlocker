# Uni Tap Blocker

タップをブロックするインスタンスを管理するクラス

## 使用例

```cs
using Kogane;
using UnityEngine;

public class Example : MonoBehaviour
{
    private TapBlocker m_blocker;

    private void Start()
    {
        TapBlocker.OnChange += () => Debug.Log( "変更" );
    }

    private void OnGUI()
    {
        if ( GUILayout.Button( "Block" ) )
        {
            m_blocker = TapBlocker.Block( "【インスタンス名】" );
        }

        if ( GUILayout.Button( "Unblock" ) )
        {
            m_blocker.Dispose();
        }

        if ( GUILayout.Button( "Clear" ) )
        {
            TapBlocker.Clear();
            //TapBlocker.Clear( true );
        }

        GUILayout.Label( TapBlocker.IsBlock.ToString() );
        GUILayout.Label( TapBlocker.BlockCount.ToString() );
        
        foreach ( var blocker in TapBlocker.Blockers )
        {
            GUILayout.Label( blocker.Name );
        }
    }
}
```