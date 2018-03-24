-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.3.5-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win32
-- HeIdiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for Gaycardsdb
CREATE DATABASE IF NOT EXISTS `GayRate` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `GayRate`;

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.playgroup
CREATE TABLE IF NOT EXISTS `Playgroup` (
  `Id` tinyint(3) unsigned NOT NULL,
  `Name` varchar(50) NOT NULL,
  `PasswordHash` varchar(50) NOT NULL,
  `PasswordSalt` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Playgroup is an entity  that groups together Gays that play games togeher. A Gay can belong to several playgroups at once, but each game is only played within a single playgroup. This allows for keeping track of statistics on a playgroup level.';

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.Gay
CREATE TABLE IF NOT EXISTS `Gay` (
  `Id` smallint(5) unsigned NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Nick` varchar(50) NOT NULL,
  `Created` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Gay entity represents a single actor (MTG player) in the system. Such an actor owns a deck, plays games, belongs to a playgroup, etc.';

-- Dumping structure for table Gaycardsdb.deck
CREATE TABLE IF NOT EXISTS `Deck` (
  `Id` smallint(6) unsigned NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `OwnerId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Owner` (`OwnerId`),
  CONSTRAINT `FK_Owner` FOREIGN KEY (`OwnerId`) REFERENCES `Gay` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='deck is an entity that represents a MTG card deck with which a game is played by each Gay attending that game';

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.game
CREATE TABLE IF NOT EXISTS `Game` (
  `Id` mediumint(8) unsigned NOT NULL,
  `FormatId` tinyint(3) unsigned NULL,
  `StartedAt` datetime NOT NULL,
  `FinishedAt` datetime NOT NULL,
  `Notes` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Format` (`FormatId`),
  CONSTRAINT `FK_Format` FOREIGN KEY (`FormatId`) REFERENCES `GameFormat` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='game is an entity representing a single MTG card game played by Gays (from a specific playgroup) each with a single deck';

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.GameFormat
CREATE TABLE IF NOT EXISTS `GameFormat` (
  `Id` tinyint(3) unsigned NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(250) NOT NULL,
  `PlayersMin` tinyint(3) unsigned NOT NULL DEFAULT 2,
  `PlayersMax` tinyint(3) unsigned DEFAULT NULL,
  `IsSingleton` tinyint(1) unsigned NOT NULL DEFAULT 1,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Defines a MTG game format that each game is played by. It defines how many players can participate, what is the starting life total of each player, how many cards each deck needs to have (at least) and whether it is a singleton format.';

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.gametimespent
CREATE TABLE IF NOT EXISTS `GameTimeSpent` (
  `GameId` mediumint(8) unsigned NOT NULL,
  `GayId` smallint(5) unsigned NOT NULL,
  `TimeSpentInSeconds` int(10) unsigned NOT NULL,
  KEY `FK_Game` (`GameId`),
  KEY `FK_Gay` (`GayId`),
  CONSTRAINT `FK_Game` FOREIGN KEY (`GameId`) REFERENCES `Game` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Gay` FOREIGN KEY (`GayId`) REFERENCES `Gay` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='this table records total game time spent by indivIdual Gay within a single game';

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.Gayingamedetails
CREATE TABLE IF NOT EXISTS `GayInGameDetails` (
  `GameId` mediumint(8) unsigned NOT NULL,
  `GayId` smallint(5) unsigned NOT NULL,
  `DeckId` smallint(6) unsigned NOT NULL,
  `isWinner` tinyint(1) unsigned NOT NULL,
  `isDraw` tinyint(1) unsigned NOT NULL,
  KEY `FK_GiG_Game` (`GameId`),
  KEY `FK_GiG_Gay` (`GayId`),
  KEY `FK_GiG_Deck` (`DeckId`),
  CONSTRAINT `FK_GiG_Deck` FOREIGN KEY (`DeckId`) REFERENCES `Deck` (`Id`),
  CONSTRAINT `FK_GiG_Game` FOREIGN KEY (`GameId`) REFERENCES `Game` (`Id`),
  CONSTRAINT `FK_GiG_Gay` FOREIGN KEY (`GayId`) REFERENCES `Gay` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Relational table that stores information about a Gay player participating in a specific game with a specific deck';

-- Data exporting was unselected.
-- Dumping structure for table Gaycardsdb.Gayinplaygroup
CREATE TABLE IF NOT EXISTS `GayToPlaygroup` (
  `GayId` smallint(5) unsigned NOT NULL,
  `PlaygroupId` tinyint(3) unsigned NOT NULL,
  KEY `FK_GayId` (`GayId`),
  KEY `FK_PlaygroupId` (`PlaygroupId`),
  CONSTRAINT `FK_GayId` FOREIGN KEY (`GayId`) REFERENCES `Gay` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PlaygroupId` FOREIGN KEY (`PlaygroupId`) REFERENCES `playgroup` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Relational table that stores membership of indivIdual Gay actors within specific playgroups (N:N)';

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
