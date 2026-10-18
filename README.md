# CrazyUniverse

WPF rakendus erinevate robotite juhtimiseks

## Projekti struktuur

- CrazyUniverse.Core robotite klassid, hoidlad ja loogika
- CrazyUniverse.WPF kasutajaliides, ViewModel ja käsud

## Klassid

- Robot abstraktne baasklass
- CleanerBot koristab
- ExplorerBot uurib
- RepairBot parandab
- GuardBot valvab
- MedicBot partneri lisatud robot
- ChefBot partneri lisatud robot
- DeliveryBot partneri lisatud robot

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
- Tegevuste ja reaktsioonide logi
- Robotitüübi hoidla valimine
- Otsimine nime järgi
- Sortimine nime või aku järgi
- Robotite arvu kokkuvõte tüübi järgi

## Aku ja CrazyAction

Igal robotil on aku vahemikus 0-100%

CrazyAction kasutab akut ja tagastab erineva sõnumi sõltuvalt robotist

Kui akut pole piisavalt, tegevust ei tehta

Charge lisab kuni 20% akut.
RepairBot parandamine lisab teisele robotile kuni 30% akut

## Vol.2 hoidlad ja sündmused

Igal robotitüübil on oma Repository<T> hoidla.
Hoidla kasutab IRepository<T> liidest

Lisamisel tervitavad sama hoidla olemasolevad robotid uut robotit

Eemaldamisel jätavad allesjäänud robotid lahkujaga hüvasti

CrazyAction korral reageerivad sama hoidla teised robotid.
Reaktsioonid kuvatakse logis ja muudavad robotite olekut

Robot ei reageeri iseendale.
Teiste hoidlate robotid ei reageeri.
Eemaldatud robot ei kuula enam hoidla sündmusi

## LINQ

- Where otsib roboteid nime järgi
- OrderBy ja OrderByDescending sorteerivad roboteid
- GroupBy koostab robotite arvu kokkuvõtte tüübi järgi

## Kasutajaliides ja MVVM

MainWindow.xaml kuvab andmeid bindingute kaudu

MainViewModel hoiab valitud robotitüüpi, valitud robotit,
sisendit ja kuvatavaid loendeid

Nupud kasutavad RelayCommand käske.
CanExecute kontrollib tegevuste saadavust

Kuvatavate loendite jaoks kasutatakse ObservableCollection.
Omaduste muutumisest teatatakse INotifyPropertyChanged kaudu

Robotite loogika ja reaktsioonid asuvad Core projektis

WPF-is on kasutatud WrapPanel elementi,
et tegevuste nupud oleksid ühes reas

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
6. Tühi nimi - lisamise nupp pole saadaval
7. Sama tüübi robotid reageerivad lisamisele, eemaldamisele ja CrazyActionile
8. Teise hoidla robotid ja eemaldatud robotid ei reageeri
9. Otsing, sortimine ja robotite arvu kokkuvõte töötavad

## AI kasutamine

Vol.1 puhul kasutasin ChatGPT abi koodi koostamisel ja vigade
parandamisel, sealhulgas laadimise ja parandamise loogikas.
Testisin rakendust Visual Studios ning muutsin kasutajaliidest
ja robotite loogikat

Vol.2 puhul kasutasin ChatGPT abi hoidlate, sündmuste ja MVVM-i
koostamisel ning vigade parandamisel

## GitHub koostöö

### Vol.1

Kaasüliõpilane: Max Valja

Issue: Change CleanerBot action

Pull Request:
https://github.com/tutposhka/CrazyUniverse/pull/2

### Vol.2

Partner lisas MedicBot, ChefBot ja DeliveryBot klassid
ning ühendas need hoidlate, sündmuste ja kasutajaliidesega

Partneri Pull Request:
https://github.com/tutposhka/CrazyUniverse/pull/3

Lõppversioon asub vol2 harus
