# CrazyUniverse

WPF rakendus erinevate robotite juhtimiseks

## Projekti struktuur

- CrazyUniverse.Core robotite klassid ja loogika
- CrazyUniverse.WPF kasutajaliides

## Klassid

- Robot abstraktne baasklass
- CleanerBot koristab
- ExplorerBot uurib
- RepairBot parandab
- GuardBot valvab

## Liidesed

- IChargeable aku laadimine
- IRepair robotite parandamine
- IScan skannimine

ExplorerBot kasutab kahte liidest: IChargeable ja IScan

## Funktsioonid

- Robotite lisamine ja kustutamine
- Roboti valimine ja info kuvamine
- CrazyAction igal robotil erinev tegevus
- Aku laadimine ja parandamine
- Tegevuste logi

## Aku ja CrazyAction

Igal robotil on aku vahemikus 0-100%

CrazyAction kasutab akut ja tagastab erineva sõnumi sõltuvalt robotist

Kui akut pole piisavalt, tegevust ei tehta

Charge lisab kuni 20% akut. Repair lisab teisele robotile kuni 30% akut

## Kasutajaliides

Robotite hoidmiseks kasutatakse ObservableCollection kollektsiooni

WPF-is on kasutatud WrapPanel elementi, et tegevuste nupud oleksid ühes reas

## Käivitamine

1. Ava Solution2.slnx Visual Studios
2. Vali CrazyUniverse.WPF käivitusprojektiks
3. Käivita F5 abil

## Testimine

1. Roboti lisamine - robot ilmub nimekirja
2. Roboti kustutamine - robot eemaldatakse
3. CrazyAction - aku väheneb
4. Charge - aku suureneb
5. Repair - valitud roboti aku suureneb
6. Tühi nimi - kuvatakse veateade

## AI kasutamine

Ma kasutasin ChatGPT koodi koostamisel ja vigade parandamisel (mittetöötava laadimise ja parandamise loogika). Testisin rakendust Visual Studios ning muutsin kasutajaliidest ja robotite loogikat

## GitHub koostöö

Kaasüliõpilane: hiljem

Issue: hiljem

Pull Request: hiljem
