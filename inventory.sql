-- phpMyAdmin SQL Dump
-- version 4.1.12
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Erstellungszeit: 26. Apr 2016 um 12:06
-- Server Version: 5.6.16
-- PHP-Version: 5.5.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Datenbank: `ims`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_arbeitsspeicher`
--

CREATE TABLE IF NOT EXISTS `ims_arbeitsspeicher` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Speicher` int(11) NOT NULL,
  `Taktrate` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Hersteller_RAM` (`ID_Hersteller`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=16 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_festplatte`
--

CREATE TABLE IF NOT EXISTS `ims_festplatte` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Kapazität` int(11) NOT NULL,
  `SSD` tinyint(1) NOT NULL,
  `Zoll` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `hersteller_Festplatte` (`ID_Hersteller`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=16 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_festplatte_schnittstelle`
--

CREATE TABLE IF NOT EXISTS `ims_festplatte_schnittstelle` (
  `ID_Festplatte` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Festplatte`,`ID_Schnittstelle`),
  KEY `ID_Schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_grafikkarte`
--

CREATE TABLE IF NOT EXISTS `ims_grafikkarte` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Taktrate` float NOT NULL,
  `Modelbezeichnung` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Grafikspeicher` int(11) NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_Hersteller` (`ID_Hersteller`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=15 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_grafikkarte_schnittstelle`
--

CREATE TABLE IF NOT EXISTS `ims_grafikkarte_schnittstelle` (
  `ID_Grafikkarte` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Grafikkarte`,`ID_Schnittstelle`),
  KEY `schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_hauptplatine`
--

CREATE TABLE IF NOT EXISTS `ims_hauptplatine` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Zoll` float NOT NULL,
  `Sockel` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_Hersteller` (`ID_Hersteller`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=16 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_hauptplatine_schnittstelle`
--

CREATE TABLE IF NOT EXISTS `ims_hauptplatine_schnittstelle` (
  `ID_Hauptplatine` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Hauptplatine`,`ID_Schnittstelle`),
  KEY `ID_Schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_hersteller`
--

CREATE TABLE IF NOT EXISTS `ims_hersteller` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Firma` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Telefon` int(11) DEFAULT NULL,
  `Email` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Webseite` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PLZ` int(11) DEFAULT NULL,
  `Ort` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Straße` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Hausnummer` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Firma` (`Firma`,`Webseite`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=18 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_monitor`
--

CREATE TABLE IF NOT EXISTS `ims_monitor` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Auflösung` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `Zoll` int(11) NOT NULL,
  `Seitenverhältnis` varchar(6) COLLATE utf8_unicode_ci NOT NULL,
  `ID_Hersteller` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Hersteller_ID` (`ID_Hersteller`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=15 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_monitor_schnittstelle`
--

CREATE TABLE IF NOT EXISTS `ims_monitor_schnittstelle` (
  `ID_Monitor` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_Monitor`,`ID_Schnittstelle`),
  KEY `ID_Schnittstelle` (`ID_Schnittstelle`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_prozessor`
--

CREATE TABLE IF NOT EXISTS `ims_prozessor` (
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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=16 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_schnittstelle`
--

CREATE TABLE IF NOT EXISTS `ims_schnittstelle` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Seriell` tinyint(1) NOT NULL,
  `Übertragungsrate` float NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=14 ;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `ims_arbeitsspeicher`
--
ALTER TABLE `ims_arbeitsspeicher`
  ADD CONSTRAINT `ims_arbeitsspeicher_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_festplatte`
--
ALTER TABLE `ims_festplatte`
  ADD CONSTRAINT `ims_festplatte_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_festplatte_schnittstelle`
--
ALTER TABLE `ims_festplatte_schnittstelle`
  ADD CONSTRAINT `ims_festplatte_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Festplatte`) REFERENCES `ims_festplatte` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ims_festplatte_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `ims_schnittstelle` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_grafikkarte`
--
ALTER TABLE `ims_grafikkarte`
  ADD CONSTRAINT `hersteller` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_grafikkarte_schnittstelle`
--
ALTER TABLE `ims_grafikkarte_schnittstelle`
  ADD CONSTRAINT `grafik` FOREIGN KEY (`ID_Grafikkarte`) REFERENCES `ims_grafikkarte` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `schnittstelle` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `ims_schnittstelle` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_hauptplatine`
--
ALTER TABLE `ims_hauptplatine`
  ADD CONSTRAINT `ims_hauptplatine_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_hauptplatine_schnittstelle`
--
ALTER TABLE `ims_hauptplatine_schnittstelle`
  ADD CONSTRAINT `ims_hauptplatine_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Hauptplatine`) REFERENCES `ims_hauptplatine` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ims_hauptplatine_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `ims_schnittstelle` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_monitor`
--
ALTER TABLE `ims_monitor`
  ADD CONSTRAINT `ims_monitor_ibfk_2` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_monitor_schnittstelle`
--
ALTER TABLE `ims_monitor_schnittstelle`
  ADD CONSTRAINT `ims_monitor_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Monitor`) REFERENCES `ims_monitor` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ims_monitor_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `ims_schnittstelle` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_prozessor`
--
ALTER TABLE `ims_prozessor`
  ADD CONSTRAINT `ims_prozessor_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
