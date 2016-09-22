create database bilog;

drop table if exists Payment;
create table Payment(
    `Time` timestamp not null default CURRENT_TIMESTAMP,
    `UserId` int(10) unsigned not null,
    `RemoteAdress` varchar(20) not null,
    `CashCount` int(10) unsigned not null,
    `GameTokenCount` int(10) unsigned not null,
    `RewardTokenCount` int(10) unsigned not null,
    `AttachInfo` varchar(100) default ""
)engine=innodb default charset=utf8;

create table PurchaseLog(
    `Time` timestamp not null default CURRENT_TIMESTAMP,
    `User` int(10) not null,
    `CostItem` int(10) unsigned not null,
    `CostCount` int(10) unsigned not null,
    `Gems` int(10) unsigned not null,
    primary key(`Time`)
)engine=innodb default charset=utf8;