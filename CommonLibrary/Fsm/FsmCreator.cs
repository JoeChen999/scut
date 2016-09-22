namespace Genesis.GameServer.CommonLibrary
{
    /// <summary>
    /// 有限状态机创建器。
    /// </summary>
    public static class FsmCreator
    {
        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>有限状态机。</returns>
        public static IFsm<T> Create<T>(T owner, params FsmState<T>[] states) where T : class
        {
            return new Fsm<T>(owner, states);
        }
    }
}
