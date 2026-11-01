# CrazyUniverse Vol.3

WPF rakendus robotite CPU-simulatsioonide käivitamiseks ja võrdlemiseks.

## Funktsioonid

- Vähemalt kolme roboti valimine
- Kolm koormuse taset: Small, Medium, Large
- Sequential - järjestikune arvutamine
- Parallel - paralleelne arvutamine
- Task.Run ja Task.WhenAll kasutamine
- Stopwatch abil tööaja mõõtmine
- CancellationToken abil arvutuste tühistamine
- Iga roboti tulemuse, vea või tühistamise kuvamine
- Kogu tööaja võrdlemine

## Käivitamine

1. Ava Solution2.slnx Visual Studios
2. Vali CrazyUniverse.WPF käivitusprojektiks
3. Käivita F5 abil
4. Vajuta Vol.3 nuppu
5. Vali vähemalt kolm robotit ja koormuse tase
6. Käivita Sequential või Parallel

## Testimine

1. Sequential - robotite arvutused töötavad järjest
2. Parallel - arvutused töötavad paralleelselt
3. Cancel - arvutused tühistatakse
4. Vähem kui kolm robotit - arvutust ei alustata
5. Tulemused - kuvatakse robotite tulemused ja tööajad
6. Võrdlus - paralleelne täitmine on testimisel kiirem
