======= Inhalt =======



- Projekt-Datei als .exe

- Projekt-Mappe für Visual Studio

- SQL-Dateien zum initialen Aufsetzen der Datenbank
- Projektdokumentation als PDF-Datei
- Programmierer-Dokumentation als XML

- Diagramme und Mockups






======= Systemanforderungen =======



- Microsoft Windows 7

- .NET-Framework

- MySQL/MariaDB
- Datenbank




======= Installation ohne Visual Studio =======

Die Beschreibung der Installation basiert auf der phpMyAdmin-Oberfläche von XAMPP.

1. XAMPP Control Panel starten
2. Apache & MySQL starten
3. In beliebigen Browser wechseln und auf "http://localhost/phpmyadmin/" navigieren
4. Auf den Reiter 'Importieren' wechseln
5. Datenbank importieren: Durchsuchen -> 'ims.sql' aus dem Abgabe-Verzeichnis wählen
6. InventoryManagementSystem.exe starten
7. Datenbank-Einstellungen über die Oberfläche anpassen

Die Anwendung sollte nun eine Verbindung mit der Datenbank herstellen können. Beim Anlegen
der Datenbank wird zusätzlich ein lokaler Benutzer 'ims_user' angelegt, welcher alle
erforderlichen Rechte zum Nutzen der Anwendung besitzt.
Das Passwort des Nutzers 'ims_user' lautet 'changeme'.


======= Installation mit Visual Studio =======

Um das Projekt auch aus Visual Studio zu betreiben sind nur wenige Schritte nötig:

1. Visual Studio starten
2. Projekt öffnen

3. MySQL-Treiber aus dem Ordner 'Treiber' des Abgabe-Verzeichnises einbinden:

3.1 Rechtsklick auf InformationManagementSystem

3.2 Hinzufügen -> Verweis

3.3 Durchsuchen

3.4 Die Datei mysql.data.dll einfügen



Die Anwendung sollte sich nun starten lassen. Damit die Unit-Tests keine bestehenden Daten 
in der Datenbank manipulieren sollten sie auf einer seperaten Datenbank ausgeführt werden.
Dafür muss einfach die 'ims_test.sql' wie oben beschrieben importiert werden. Die erforderlichen
Einstellungen wurden bereits in der Settings.setting durchgeführt. Der Benutzer ist der selbe,
wie der bereits beschriebene 'ims_user'.