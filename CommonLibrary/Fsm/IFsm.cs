namespace Genesis.GameServer.CommonLibrary
{
    /// <summary>
    /// 有限状态机接口。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public interface IFsm<T> where T : class
    {
        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        T Owner
        {
            get;
        }

        /// <summary>
        /// 获取当前状态。
        /// </summary>
        FsmState<T> CurrentState
        {
            get;
        }

        /// <summary>
        /// 获取当前状态持续时间。
        /// </summary>
        float CurrentStateTime
        {
            get;
        }

        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 是否存在状态。
        /// </summary>
        /// <param name="name">要检查的状态名称。</param>
        /// <returns>是否存在状态。</returns>
        bool HasState(string name);

        /// <summary>
        /// 是否存在状态。
        /// </summary>
        /// <typeparam name="TState">要检查的状态类型。</typeparam>
        /// <returns>是否存在状态。</returns>
        bool HasState<TState>() where TState : FsmState<T>;

        /// <summary>
        /// 获取状态。
        /// </summary>
        /// <param name="name">要获取的状态名称。</param>
        /// <returns>要获取的状态。</returns>
        FsmState<T> GetState(string name);

        /// <summary>
        /// 获取状态。
        /// </summary>
        /// <typeparam name="TState">要获取的状态类型。</typeparam>
        /// <returns>要获取的状态。</returns>
        FsmState<T> GetState<TState>() where TState : FsmState<T>;
    }
}
