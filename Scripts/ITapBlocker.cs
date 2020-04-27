namespace UniTapBlocker
{
	/// <summary>
	/// タップをブロックするインスタンスのインターフェイス
	/// </summary>
	public interface ITapBlocker
	{
		/// <summary>
		/// インスタンス名を返します
		/// </summary>
		string Name { get; }
	}
}