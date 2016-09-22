Drop table if exists `ServerList`;
create table `ServerList`
(
	`Id` int unsigned not null auto_increment,
	`Name` varchar(50) not null,
	`Description` varchar(50),
	`Status` int unsigned not null default 0,
	`Load` int unsigned not null default 0,
	`Address` varchar(50) not null,
	`Port` int unsigned not null,
	`TabId` int unsigned not null default 0,
	`Recommended` tinyint(1) unsigned not null default 0,
	`DictionaryDownloadUrl` varchar(100) not null,
	`DataTableDownloadUrl` varchar(100) not null,
	`DownloadMetaDataUrl` varchar(100) not null,
	primary key(`Id`)
)engine=innodb default charset=utf8 auto_increment=1;

Drop table if exists `Users`;
create table `Users`
(
	`Account` varchar(20) not null,
	`Password` varchar(32) not null,
	`UserId` int unsigned not null auto_increment,
	`Token` varchar(32) not null,
	`RegisterTime` timestamp not null default CURRENT_TIMESTAMP,
	`LastLoginTime` timestamp not null default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
	primary key(`Account`),
	key user_id (`UserId`)
)engine=innodb default charset=utf8 auto_increment=1;

create table `SensitiveWord`
(
	`Code` int(11) not null default 0,
	`Word` varchar(255),
	primary key (`Code`)
)engine=innodb default charset=utf8;

INSERT INTO `ServerList` VALUES (1, '内部测试服务器', NULL, 0, 0, 'lobby.genesis.antinc.cn', 9001, 0, 1, 'http://lobby.genesis.antinc.cn:8000/Download/Dictionary', 'http://lobby.genesis.antinc.cn:8000/Download/DataTable','http://lobby.genesis.antinc.cn:8000/Get/DataFile/HashCode');
INSERT INTO `ServerList` VALUES (2, '外网测试服务器', NULL, 0, 0, 'lobby.genesis2.antinc.cn', 9001, 0, 1, 'http://lobby.genesis2.antinc.cn:8000/Download/Dictionary', 'http://lobby.genesis2.antinc.cn:8000/Download/DataTable','http://lobby.genesis2.antinc.cn:8000/Get/DataFile/HashCode');
INSERT INTO `ServerList` VALUES (3, '陈彪的服务器', NULL, 0, 0, '192.168.1.5', 9001, 0, 0, 'http://lobby.genesis.antinc.cn:8000/Download/Dictionary', 'http://lobby.genesis.antinc.cn:8000/Download/DataTable','http://lobby.genesis.antinc.cn:8000/Get/DataFile/HashCode');
INSERT INTO `ServerList` VALUES (4, '本地服务器', NULL, 0, 0, '127.0.0.1', 9001, 0, 0, 'http://lobby.genesis.antinc.cn:8000/Download/Dictionary', 'http://lobby.genesis.antinc.cn:8000/Download/DataTable','http://lobby.genesis.antinc.cn:8000/Get/DataFile/HashCode');