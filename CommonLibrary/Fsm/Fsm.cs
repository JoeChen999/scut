using System.Collections.Generic;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.CommonLibrary
{
    /// <summary>
    /// 有限状态机。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    internal class Fsm<T> : IFsm<T> where T : class
    {
        private readonly T m_Owner;
        private readonly IDictionary<string, FsmState<T>> m_States;
        private FsmState<T> m_CurrentState;
        private float m_CurrentStateTime;

        /// <summary>
        /// 初始化有限状态机的新实例。
        /// </summary>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        public Fsm(T owner, params FsmState<T>[] states)
        {
            if (owner == null)
            {
                TraceLog.WriteError("FSM owner is invalid.");
                return;
            }

            if (states == null || states.Length < 1)
            {
                TraceLog.WriteError("FSM states is invalid.");
                return;
            }

            m_Owner = owner;
            m_States = new Dictionary<string, FsmState<T>>();
            foreach (FsmState<T> state in states)
            {
                string name = state.GetType().Name;
                if (m_States.ContainsKey(name))
                {
                    TraceLog.WriteError("FSM state '{0}' is already exist.", name);
                    return;
                }

                m_States.Add(name, state);
                state.OnInit(this);
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = states[0];
            m_CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        public T Owner
        {
            get
            {
                return m_Owner;
            }
        }

        /// <summary>
        /// 获取当前状态。
        /// </summary>
        public FsmState<T> CurrentState
        {
            get
            {
                return m_CurrentState;
            }
        }

        /// <summary>
        /// 获取当前状态持续时间。
        /// </summary>
        public float CurrentStateTime
        {
            get
            {
                return m_CurrentStateTime;
            }
        }

        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapseSeconds;
            m_CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        public void Shutdown()
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentState.OnLeave(this);
            m_CurrentState = null;
            m_CurrentStateTime = 0f;
        }

        /// <summary>
        /// 是否存在状态。
        /// </summary>
        /// <param name="name">要检查的状态名称。</param>
        /// <returns>是否存在状态。</returns>
        public bool HasState(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                TraceLog.WriteError("State name is invalid.");
                return false;
            }

            return m_States.ContainsKey(name);
        }

        /// <summary>
        /// 是否存在状态。
        /// </summary>
        /// <typeparam name="TState">要检查的状态类型。</typeparam>
        /// <returns>是否存在状态。</returns>
        public bool HasState<TState>() where TState : FsmState<T>
        {
            return HasState(typeof(TState).Name);
        }

        /// <summary>
        /// 获取状态。
        /// </summary>
        /// <param name="name">要获取的状态名称。</param>
        /// <returns>要获取的状态。</returns>
        public FsmState<T> GetState(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                TraceLog.WriteError("State name is invalid.");
                return null;
            }

            FsmState<T> state = null;
            if (m_States.TryGetValue(name, out state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 获取状态。
        /// </summary>
        /// <typeparam name="TState">要获取的状态类型。</typeparam>
        /// <returns>要获取的状态。</returns>
        public FsmState<T> GetState<TState>() where TState : FsmState<T>
        {
            return GetState(typeof(TState).Name);
        }

        /// <summary>
        /// 切换当前状态。
        /// </summary>
        /// <param name="name">要切换到的状态的名称。</param>
        internal void ChangeState(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                TraceLog.WriteError("State name is invalid.");
                return;
            }

            FsmState<T> state = GetState(name);
            if (state == null)
            {
                TraceLog.WriteError("Can not change state to '{0}' which is not exist.", name);
                return;
            }

            m_CurrentState.OnLeave(this);
            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 切换当前状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的状态类型。</typeparam>
        internal void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState).Name);
        }
    }
}
