drop table if exists mails;
create table mails
(
    `Id` int unsigned not null auto_increment,
    `Message` varchar(30) not null,
    `AttachType` int unsigned not null,
    `AttachCount` int unsigned not null,
    `BeginTime` datetime not null,
    `ExpireTime` datetime not null,
    `ToUser` int not null default -1,
    primary key(`Id`)
)engine=innodb default charset=utf8 auto_increment=1;

drop table if exists configs;
create table configs
(
    `Name` varchar(100) not null,
    `Value` varchar(30) not null,
    `Type` int(2) not null default 0,
    `Description` varchar(100),
    primary key(`Name`)
)engine=innodb default charset=utf8;

drop table if exists dataTableCRC32Codes;
create table dataTableCRC32Codes
(
    `Name` varchar(50) not null,
    `CRC32Code` varchar(50) not null,
    `Type` varchar(50) not null,
    primary key(`Name`)
)engine=innodb default charset=utf8;

drop table if exists localNotifications;
create table localNotifications
(
    `Name` varchar(20) not null,
    `Time` varchar(10) not null,
    `Message` varchar(100) not null,
    `Status` int(1) not null default 1,
    primary key(`Name`),
    unique key(`Time`)
)engine=innodb default charset=utf8;

drop table if exists announcement;
create table announcement
(
    `Id` int(10) not null,
    `Type` int(1) not null default 0,
    `Message` varchar(1024) not null,
    `BeginTime` datetime not null,
    `ExpireTime` datetime not null,
    `IntervalMinutes` int(10) not null default 0,
    primary key(`Id`)
)engine=innodb default charset=utf8;

insert into dataTableVersions(`Name`) values ("BadWords"),("Chance"),("ChanceCost"),("Drop"),("Epigraph"),("Gear"),("GearLevelUp"),("Hero"),("HeroBase"),("HeroConsciousnessBase"),("HeroElevationBase"),("Instance"),("InstanceStory"),("Item"),("Meridian"),("Player"),("RandomShop"),("Soul");

INSERT INTO `configs` VALUES 
('Arena_Rank_Player_Count_Per_Page', '20', 1, ""),
('Arena_Token_For_Loser','1', 1, ""),
('Arena_Token_For_Winner','2', 1, ""),
('ChanceRefreshCost0','10000', 1, ""),
('ChanceRefreshCost1','50', 1, ""),
('Cosmos_Crack_NPC_Level_Range','5', 1, ""),
('Cosmos_Crack_Round_Limit','10', 1, ""),
('Default_Shop_Refresh_Cost','100', 1, ""),
('Epigraph_Required_Level_1','1', 1, ""),
('Epigraph_Required_Level_2','10', 1, ""),
('Epigraph_Required_Level_3','20', 1, ""),
('Epigraph_Required_Level_4','30', 1, ""),
('Epigraph_Required_Level_5','40', 1, ""),
('Epigraph_Required_Level_6','50', 1, ""),
('Epigraph_Required_Level_7','60', 1, ""),
('Foundry_Cool_Down_Seconds','10', 1, ""),
('Gear_Compose_Success_Rate_1','80', 1, ""),
('Gear_Compose_Success_Rate_2','70', 1, ""),
('Gear_Compose_Success_Rate_3','30', 1, ""),
('Gear_Compose_Success_Rate_4','20', 1, ""),
('Gear_Foundry_Invitation_Duration','300', 1, ""),
('Gear_Foundry_Level_Count','3', 1, ""),
('Gear_Foundry_Progress_Count_0','5', 1, ""),
('Gear_Foundry_Progress_Count_1','5', 1, ""),
('Gear_Foundry_Progress_Count_2','5', 1, ""),
('Hero_Piece_Count_For_Star_Level_1','10', 1, ""),
('Hero_Piece_Count_For_Star_Level_2','30', 1, ""),
('Hero_Piece_Count_For_Star_Level_3','50', 1, ""),
('Hero_Piece_Count_For_Star_Level_4','80', 1, ""),
('Hero_Piece_Count_For_Star_Level_5','100', 1, ""),
('Instance_Cost_Energy','6', 1, ""),
('Mail_Expire_Days','7', 1, ""),
('Max_Friend_Count','50', 1, ""),
('Max_Friend_Invitation_Count','20', 1, ""),
('NearbyPlayerCountLimit_High','15', 1, ""),
('NearbyPlayerCountLimit_Low','5', 1, ""),
('NearbyPlayerCountLimit_Middle','10', 1, ""),
('Need_Check_Instance_Prerequisite','0', 1, ""),
('Offline_Arena_Liveness_Reward_Count','3', 1, ""),
('Offline_Arena_Liveness_Reward_Win_Count_0','1', 1, ""),
('Offline_Arena_Liveness_Reward_Win_Count_1','2', 1, ""),
('Offline_Arena_Liveness_Reward_Win_Count_2','3', 1, ""),
('Pieces_Per_Hero','10', 1, ""),
('Player_Friend_List_Max_Length','50', 1, ""),
('Player_Name_Max_Length','8', 1, ""),
('Player_Name_Min_Length','3', 1, ""),
('Spirits_Per_Piece','1', 1, ""),
('Sync_Lobby_Position_Interval','6', 1, ""),
('Vip_Shop_Refresh_Cost','200', 1, ""),
('World_Chat_CoolDown_Seconds','30', 1, ""),
('World_Chat_Max_Length','60', 1, ""),
('Single_Pvp_Waiting_Connect_Time_Limit', '30', 1, ""),
('Chess_Bomb_Period_1', '5', 1, ""),
('Chess_Bomb_Cost_1', '2', 1, ""),
('Chess_Bomb_Period_2', '10', 1, ""),
('Chess_Bomb_Cost_2', '5', 1, ""),
('Chess_Bomb_Period_3', '20', 1, ""),
('Chess_Bomb_Cost_3', '10', 1, ""),
('Chess_Bomb_Period_4', '40', 1, ""),
('Chess_Bomb_Cost_4', '20', 1, ""),
('Chess_Bomb_Period_5', '85', 1, ""),
('Chess_Bomb_Cost_5', '40', 1, ""),
('Chess_Bomb_Period_Count', '5', 1, ""),
('Inventory_Slots_Per_Tab', '200', 1, ""),
('Inventory_Item_Max_Count', '999', 1, ""),
('Max_Email_Display_Count', '200', 1, ""),
('Pvp_Start_Time_Everyday', '4:00:00', 1, ""),
('Pvp_End_Time_Everyday', '16:00:00', 1, ""),
('Pvp_Require_Player_Level', '2', 1, ""),
('Max_Free_Count_For_Coin_Chance', '5', 1, ""),
('Room_Movement_Sync_Interval', '0.5', 2, ""),
('Room_Position_Delta_Threshold', '0.1', 2, ""),
('Room_Rotation_Delta_Threshold', '1', 2, ""),
('Room_Battle_Start_Protection_Time', '3', 2, "") on duplicate key update Value=values(Value), Type=values(Type);
