using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.CommonLibrary
{
    /// <summary>
    /// 有限状态机状态基类。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// 状态初始化时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnInit(IFsm<T> fsm)
        {

        }

        /// <summary>
        /// 进入状态时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnEnter(IFsm<T> fsm)
        {

        }

        /// <summary>
        /// 离开状态时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnLeave(IFsm<T> fsm)
        {

        }

        /// <summary>
        /// 状态轮询时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal virtual void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 切换当前状态。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="name">要切换到的状态的名称。</param>
        protected void ChangeState(IFsm<T> fsm, string name)
        {
            Fsm<T> fsmImplement = fsm as Fsm<T>;
            if (fsm == null)
            {
                TraceLog.WriteError("FSM is invalid.");
                return;
            }

            fsmImplement.ChangeState(name);
        }

        /// <summary>
        /// 切换当前状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的状态类型。</typeparam>
        /// <param name="fsm">有限状态机引用。</param>
        protected void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T>
        {
            Fsm<T> fsmImplement = fsm as Fsm<T>;
            if (fsm == null)
            {
                TraceLog.WriteError("FSM is invalid.");
                return;
            }

            fsmImplement.ChangeState<TState>();
        }
    }
}
