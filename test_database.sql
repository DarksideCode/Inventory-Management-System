SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;

CREATE DATABASE IF NOT EXISTS `ims_test` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `ims_test`;

CREATE TABLE IF NOT EXISTS `test_arbeitsspeicher` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Speicher` int(11) NOT NULL,
  `Taktrate` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Hersteller_RAM` (`ID_Hersteller`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `test_festplatte` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Kapazität` int(11) NOT NULL,
  `SSD` tinyint(1) NOT NULL,
  `Zoll` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `hersteller_Festplatte` (`ID_Hersteller`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `test_festplatte_schnittstelle` (
  `ID_Festplatte` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Festplatte`,`ID_Schnittstelle`),
  KEY `ID_Schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE IF NOT EXISTS `test_grafikkarte` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Taktrate` float NOT NULL,
  `Modelbezeichnung` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Grafikspeicher` int(11) NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_Hersteller` (`ID_Hersteller`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `test_grafikkarte_schnittstelle` (
  `ID_Grafikkarte` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Grafikkarte`,`ID_Schnittstelle`),
  KEY `schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE IF NOT EXISTS `test_hauptplatine` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Zoll` float NOT NULL,
  `Sockel` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_Hersteller` (`ID_Hersteller`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `test_hauptplatine_schnittstelle` (
  `ID_Hauptplatine` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Hauptplatine`,`ID_Schnittstelle`),
  KEY `ID_Schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE IF NOT EXISTS `test_hersteller` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Firma` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Telefon` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Webseite` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PLZ` int(11) DEFAULT NULL,
  `Ort` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Straße` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Hausnummer` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Firma` (`Firma`,`Webseite`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=3 ;

CREATE TABLE IF NOT EXISTS `test_monitor` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Auflösung` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `Zoll` float NOT NULL,
  `Seitenverhältnis` varchar(6) COLLATE utf8_unicode_ci NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Hersteller_ID` (`ID_Hersteller`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=3 ;

CREATE TABLE IF NOT EXISTS `test_monitor_schnittstelle` (
  `ID_Monitor` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Monitor`,`ID_Schnittstelle`),
  KEY `ID_Schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE IF NOT EXISTS `test_prozessor` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Modell` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Kerne` int(11) NOT NULL,
  `Befehlssatz` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Architektur` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Taktrate` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `hersteller_prozessor` (`ID_Hersteller`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `test_schnittstelle` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Seriell` tinyint(1) NOT NULL,
  `Übertragungsrate` float NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=3 ;


ALTER TABLE `test_arbeitsspeicher`
  ADD CONSTRAINT `test_arbeitsspeicher_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `test_hersteller` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_festplatte`
  ADD CONSTRAINT `test_festplatte_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `test_hersteller` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_festplatte_schnittstelle`
  ADD CONSTRAINT `test_festplatte_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Festplatte`) REFERENCES `test_festplatte` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `test_festplatte_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `test_schnittstelle` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_grafikkarte`
  ADD CONSTRAINT `hersteller` FOREIGN KEY (`ID_Hersteller`) REFERENCES `test_hersteller` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_grafikkarte_schnittstelle`
  ADD CONSTRAINT `grafik` FOREIGN KEY (`ID_Grafikkarte`) REFERENCES `test_grafikkarte` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `schnittstelle` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `test_schnittstelle` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_hauptplatine`
  ADD CONSTRAINT `test_hauptplatine_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `test_hersteller` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_hauptplatine_schnittstelle`
  ADD CONSTRAINT `test_hauptplatine_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Hauptplatine`) REFERENCES `test_hauptplatine` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `test_hauptplatine_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `test_schnittstelle` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_monitor`
  ADD CONSTRAINT `test_monitor_ibfk_2` FOREIGN KEY (`ID_Hersteller`) REFERENCES `test_hersteller` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_monitor_schnittstelle`
  ADD CONSTRAINT `test_monitor_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Monitor`) REFERENCES `test_monitor` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `test_monitor_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `test_schnittstelle` (`ID`) ON UPDATE CASCADE;

ALTER TABLE `test_prozessor`
  ADD CONSTRAINT `test_prozessor_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `test_hersteller` (`ID`) ON UPDATE CASCADE;
  
  
GRANT USAGE ON *.* TO 'ims_user'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON `ims\_test`.* TO 'ims_user'@'localhost';
COMMIT;