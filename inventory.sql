-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Erstellungszeit: 10. Feb 2016 um 12:57
-- Server-Version: 10.1.9-MariaDB
-- PHP-Version: 5.6.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `inventory`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_arbeitsspeicher`
--

CREATE TABLE `ims_arbeitsspeicher` (
  `ID` int(11) NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Speicher` int(11) NOT NULL,
  `Taktrate` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_arbeitsspeicher`:
--   `ID`
--       `ims_hersteller` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_festplatte`
--

CREATE TABLE `ims_festplatte` (
  `ID` int(11) NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Kapazität` int(11) NOT NULL,
  `SSD` int(11) DEFAULT NULL,
  `Zoll` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_festplatte`:
--   `ID`
--       `ims_hersteller` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_grafikkarte`
--

CREATE TABLE `ims_grafikkarte` (
  `ID` int(11) NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Taktrate` float DEFAULT NULL,
  `Modelbezeichnung` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Grafikspeicher` int(11) DEFAULT NULL,
  `ID_Hersteller` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_grafikkarte`:
--   `ID_Hersteller`
--       `ims_hersteller` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_grafikkarte_schnittstelle`
--

CREATE TABLE `ims_grafikkarte_schnittstelle` (
  `ID_Grafikkarte` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_grafikkarte_schnittstelle`:
--   `ID_Grafikkarte`
--       `ims_grafikkarte` -> `ID`
--   `ID_Schnittstelle`
--       `ims_schnittstelle` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_hauptplantine`
--

CREATE TABLE `ims_hauptplantine` (
  `ID` int(11) NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Zoll` float NOT NULL,
  `Sockel` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `ID_Hersteller` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_hauptplantine`:
--   `ID_Hersteller`
--       `ims_hersteller` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_hauptplatine_schnittstelle`
--

CREATE TABLE `ims_hauptplatine_schnittstelle` (
  `ID_Hauptplatine` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_hauptplatine_schnittstelle`:
--   `ID_Hauptplatine`
--       `ims_hauptplantine` -> `ID`
--   `ID_Schnittstelle`
--       `ims_schnittstelle` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_hersteller`
--

CREATE TABLE `ims_hersteller` (
  `ID` int(11) NOT NULL,
  `Firma` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Telefon` int(11) DEFAULT NULL,
  `Email` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Webseite` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PLZ` int(11) DEFAULT NULL,
  `Ort` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Straße` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Hausnummer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_hersteller`:
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_monitor`
--

CREATE TABLE `ims_monitor` (
  `ID` int(11) NOT NULL,
  `Auflösung` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `Zoll` int(11) NOT NULL,
  `Seitenverhältnis` varchar(6) COLLATE utf8_unicode_ci NOT NULL,
  `Mitarbeiter_ID` int(11) DEFAULT NULL,
  `Hersteller_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_monitor`:
--   `Mitarbeiter_ID`
--       `mitarbeiter` -> `ID`
--   `Hersteller_ID`
--       `ims_hersteller` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_monitor_schnittstelle`
--

CREATE TABLE `ims_monitor_schnittstelle` (
  `ID_Monitor` int(11) NOT NULL,
  `ID_Schnittstelle` int(11) NOT NULL,
  `Anzahl` int(11) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_monitor_schnittstelle`:
--   `ID_Monitor`
--       `ims_monitor` -> `ID`
--   `ID_Schnittstelle`
--       `ims_schnittstelle` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_prozessor`
--

CREATE TABLE `ims_prozessor` (
  `ID` int(11) NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Modell` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Kerne` int(11) NOT NULL,
  `Befehlssatz` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Architektur` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Taktrate` float NOT NULL,
  `ID_Hersteller` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_prozessor`:
--   `ID`
--       `ims_hersteller` -> `ID`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ims_schnittstelle`
--

CREATE TABLE `ims_schnittstelle` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `Beschreibung` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Seriell` int(11) DEFAULT NULL,
  `Übertragungsrate` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- RELATIONEN DER TABELLE `ims_schnittstelle`:
--

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `ims_arbeitsspeicher`
--
ALTER TABLE `ims_arbeitsspeicher`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Hersteller_RAM` (`ID_Hersteller`);

--
-- Indizes für die Tabelle `ims_festplatte`
--
ALTER TABLE `ims_festplatte`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `hersteller_Festplatte` (`ID_Hersteller`);

--
-- Indizes für die Tabelle `ims_grafikkarte`
--
ALTER TABLE `ims_grafikkarte`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Modelbezeichnung` (`Modelbezeichnung`),
  ADD KEY `ID_Hersteller` (`ID_Hersteller`);

--
-- Indizes für die Tabelle `ims_grafikkarte_schnittstelle`
--
ALTER TABLE `ims_grafikkarte_schnittstelle`
  ADD PRIMARY KEY (`ID_Grafikkarte`,`ID_Schnittstelle`),
  ADD KEY `schnittstelle` (`ID_Schnittstelle`);

--
-- Indizes für die Tabelle `ims_hauptplantine`
--
ALTER TABLE `ims_hauptplantine`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ID_Hersteller` (`ID_Hersteller`);

--
-- Indizes für die Tabelle `ims_hauptplatine_schnittstelle`
--
ALTER TABLE `ims_hauptplatine_schnittstelle`
  ADD PRIMARY KEY (`ID_Hauptplatine`,`ID_Schnittstelle`),
  ADD KEY `ID_Schnittstelle` (`ID_Schnittstelle`);

--
-- Indizes für die Tabelle `ims_hersteller`
--
ALTER TABLE `ims_hersteller`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Firma` (`Firma`,`Webseite`);

--
-- Indizes für die Tabelle `ims_monitor`
--
ALTER TABLE `ims_monitor`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Mitarbeiter_ID` (`Mitarbeiter_ID`,`Hersteller_ID`),
  ADD KEY `Hersteller_ID` (`Hersteller_ID`);

--
-- Indizes für die Tabelle `ims_monitor_schnittstelle`
--
ALTER TABLE `ims_monitor_schnittstelle`
  ADD PRIMARY KEY (`ID_Monitor`,`ID_Schnittstelle`),
  ADD KEY `ID_Schnittstelle` (`ID_Schnittstelle`);

--
-- Indizes für die Tabelle `ims_prozessor`
--
ALTER TABLE `ims_prozessor`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `hersteller_prozessor` (`ID_Hersteller`);

--
-- Indizes für die Tabelle `ims_schnittstelle`
--
ALTER TABLE `ims_schnittstelle`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Name` (`Name`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `ims_arbeitsspeicher`
--
ALTER TABLE `ims_arbeitsspeicher`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_festplatte`
--
ALTER TABLE `ims_festplatte`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_grafikkarte`
--
ALTER TABLE `ims_grafikkarte`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_hauptplantine`
--
ALTER TABLE `ims_hauptplantine`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_hersteller`
--
ALTER TABLE `ims_hersteller`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_monitor`
--
ALTER TABLE `ims_monitor`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_prozessor`
--
ALTER TABLE `ims_prozessor`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `ims_schnittstelle`
--
ALTER TABLE `ims_schnittstelle`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `ims_arbeitsspeicher`
--
ALTER TABLE `ims_arbeitsspeicher`
  ADD CONSTRAINT `ims_arbeitsspeicher_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_festplatte`
--
ALTER TABLE `ims_festplatte`
  ADD CONSTRAINT `ims_festplatte_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_grafikkarte`
--
ALTER TABLE `ims_grafikkarte`
  ADD CONSTRAINT `hersteller` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_grafikkarte_schnittstelle`
--
ALTER TABLE `ims_grafikkarte_schnittstelle`
  ADD CONSTRAINT `grafik` FOREIGN KEY (`ID_Grafikkarte`) REFERENCES `ims_grafikkarte` (`ID`) ON UPDATE CASCADE,
  ADD CONSTRAINT `schnittstelle` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `ims_schnittstelle` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_hauptplantine`
--
ALTER TABLE `ims_hauptplantine`
  ADD CONSTRAINT `ims_hauptplantine_ibfk_1` FOREIGN KEY (`ID_Hersteller`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_hauptplatine_schnittstelle`
--
ALTER TABLE `ims_hauptplatine_schnittstelle`
  ADD CONSTRAINT `ims_hauptplatine_schnittstelle_ibfk_1` FOREIGN KEY (`ID_Hauptplatine`) REFERENCES `ims_hauptplantine` (`ID`) ON UPDATE CASCADE,
  ADD CONSTRAINT `ims_hauptplatine_schnittstelle_ibfk_2` FOREIGN KEY (`ID_Schnittstelle`) REFERENCES `ims_schnittstelle` (`ID`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `ims_monitor`
--
ALTER TABLE `ims_monitor`
  ADD CONSTRAINT `ims_monitor_ibfk_1` FOREIGN KEY (`Mitarbeiter_ID`) REFERENCES `mitarbeiter` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `ims_monitor_ibfk_2` FOREIGN KEY (`Hersteller_ID`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

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
  ADD CONSTRAINT `ims_prozessor_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `ims_hersteller` (`ID`) ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
