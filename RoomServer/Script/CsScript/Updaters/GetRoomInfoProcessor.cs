using System;
using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class GetRoomInfoProcessor : BaseActionProcessor
    {
        private CRGetRoomInfo m_Request;

        public GetRoomInfoProcessor(Room room)
            :base(room)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.GetRoomInfo; }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CRGetRoomInfo;
            m_Session = message.Session;
            if (m_Room.Token != m_Request.Token)
            {
                CustomActionDispatcher.PushError((int)ActionType, message.Session, (int)ErrorType.WrongToken, "Token error");
                return false;
            }
            bool InRoom = false;
            if (m_Room.Players.ContainsKey(m_Request.PlayerId))
            {
                InRoom = true;
            }
            if (!InRoom)
            {
                CustomActionDispatcher.PushError((int)ActionType, message.Session, (int)ErrorType.WrongUserId, "RoomId or UserId error");
                return false;
            }
            return true;
        }

        public override void Process()
        {
            TraceLog.WriteInfo("Start get room info process");
            RCGetRoomInfo response = new RCGetRoomInfo();
            response.RoomInfo = new PBRoomInfo()
            {
                Id = m_Room.Id,
                StartTime = m_Room.StartTime,
                State = (int)m_Room.State
            };
            RoomSessionUser user = new RoomSessionUser(m_Request.PlayerId, m_Request.RoomId);
            user.Online(m_Session);

            foreach (var roomPlayer in m_Room.Players)
            {
                PBRoomPlayerInfo rp = new PBRoomPlayerInfo();
                rp.PlayerInfo = new PBPlayerInfo()
                {
                    Id = roomPlayer.Value.PlayerId,
                    Name = roomPlayer.Value.Name,
                    Level = roomPlayer.Value.Level,
                    VipLevel = roomPlayer.Value.VipLevel,
                    PortraitType = roomPlayer.Value.PortraitType,
                    PositionX = roomPlayer.Value.PositionX,
                    PositionY = roomPlayer.Value.PositionY,
                    Rotation = roomPlayer.Value.Rotation,
                };
                foreach (var hero in roomPlayer.Value.Heros)
                {
                    PBLobbyHeroInfo lh = new PBLobbyHeroInfo()
                    {
                        Type = hero.HeroType,
                    };
                    lh.SkillLevels.AddRange(hero.Skills);

                    PBRoomHeroInfo rh = new PBRoomHeroInfo()
                    {
                        EntityId = hero.EntityId,
                        LobbyHeroInfo = lh,
                        HP = hero.HP,
                        MaxHP = hero.MaxHP,
                        PhysicalAttack = hero.PhysicalAttack,
                        PhysicalDefense = hero.PhysicalDefense,
                        MagicAttack = hero.MagicAttack,
                        MagicDefense = hero.MagicDefense,
                        PhysicalAtkHPAbsorbRate = hero.PhysicalAtkHPAbsorbRate,
                        PhysicalAtkReflectRate = hero.PhysicalAtkReflectRate,
                        MagicAtkHPAbsorbRate = hero.MagicAtkHPAbsorbRate,
                        MagicAtkReflectRate = hero.MagicAtkReflectRate,
                        CriticalHitProb = hero.CriticalHitProb,
                        CriticalHitRate = hero.CriticalHitRate,
                        OppPhysicalDfsReduceRate = hero.OppPhysicalDfsReduceRate,
                        OppMagicDfsReduceRate = hero.OppMagicDfsReduceRate,
                        DamageReductionRate = hero.DamageReductionRate,
                        AntiCriticalHitProb = hero.AntiCriticalHitProb,
                        AdditionalDamage = hero.AdditionalDamage,
                        RecoverHP = hero.RecoverHP,
                        ReducedSkillCoolDownRate = hero.ReducedSkillCoolDownRate,
                        HeroSwitchCoolDownRate = hero.ReduceSwitchHeroCoolDownRate,
                        Camp = roomPlayer.Value.Camp,
                    };
                    rp.RoomHeroInfo.Add(rh);
                }
                response.RoomInfo.RoomPlayerInfo.Add(rp);
            }
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType, ProtoBufUtils.Serialize(response));
            m_Session.SendAsync(buffer, 0, buffer.Length);
        }

        public override void PushResult()
        {
        }
    }
}
