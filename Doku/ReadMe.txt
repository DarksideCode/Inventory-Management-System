======= Inhalt =======

- Projekt-Datei als .exe
- Projekt-Mappe für Visual Studio
- SQL-Datei zum initialen Aufsetzen der Datenbank
- Projektdokumentation als Word-Datei
- Programmierer-Dokumentation als XML
- Diagramme und Mockups


======= Systemanforderungen =======

- Microsoft Windows 7
- .NET-Framework
- MySQL/MariaDB-Datenbank


======= Installation =======

1. Visual Studio starten
2. Projekt öffnen
3. MySQL-Treiber aus dem Ordner ??? einbinden:
3.1	Rechtsklick auf InformationManagementSystem
3.2 Hinzufügen -> Verweis
3.3 Durchsuchen
3.4 Die Datei mysql.data.dll einfügen

4. inventory.sql über phpMyAdmin in die MySQL-Datenbank importieren
4.1 XAMPP Control Panel starten
4.2 Apache & MySQL starten
4.3 Auf die Seite 'http://localhost/phpmyadmin/' navigieren
4.4 Reiter: Datenbank -> Datenbanknamen 'ims' vergeben -> Anlegen
4.5 Auf neu erstellte Datenbank klicken -> Reiter: Importieren
4.6 Durchsuchen
4.7 SQL-Datei 'inventory.sql' auswählen -> OK

Anwendung ist nun zur Benutzung bereit.
