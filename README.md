# All The Way Down 

## Abgabe Struktur
Da wir recht viele geladene Assets haben finden Sie folgende Ordner in der Abgabe, beim Aufbau des Scripts Ordners haben wir uns an die Struktur von Ihrem Musterprojekt gehalten:
- Assets/Scripts
- Packages/
- ProjectSettings/

Außerdem finden Sie unser gesamtes git repo auf: https://github.com/benjaminellmer/MOG_Projekt (nach der Bewertung werde ich dieses wieder auf privat stellen)  

## Teammitglieder und Aufgaben
- Benjamin Ellmer: Steuerung (desktop, accelerometer, touch, gyro), Stage designing, Control Menu, ...  
- Pfisterer Paul: Stage designing, Stage Logik, Game Menu, ...

## Änderungen seit der Präsentation
Seit der Präsentation haben wir nichts mehr geändert ausser ein paar kleinen Refactorings und dem ausgrauen der Gyroscop Einstellung.

## Testen auf dem PC
Sie finden unter Scenes -> Game -> Player die Einstellung debugOnPc, wenn diese aktiviert ist, funktionieren die Desktopsteuerungen, dafür aber die Mobilesteuerungen nicht mehr.

## Zusätzliche Annmerkungen
Da wir die Controls mit dem Gyroscop auf Android nicht mehr zum laufen gebracht haben, haben wir die gyroscop settings ausgegraut.  
Auf dem IPhone sollte das Gyroscop funktionieren, solange man die Position des Handys nicht bewegt (neu kalibrieren).  
Falls Sie es trotzdem auf dem IPhone ausprobieren möchten einfach interactable der toggles wieder setzen :).  
Da man keinen wirklichen Unterschied zwischen Accelerometer und Gyroscop merkt haben wir uns dafür entschieden das Accelerometer als Hauptsteuerung zu verwenden.  
