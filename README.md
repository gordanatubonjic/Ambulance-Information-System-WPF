# AmbulanceWPF

AmbulanceWPF je desktop aplikacija razvijena u WPF-u (Windows Presentation Foundation) za upravljanje medicinskim podacima u ambulantnim uslugama. Aplikacija omogućava upravljanje pacijentima, intervencijama, pregledima, dijagnozama i inventarom lijekova. Podržava višejezičnost (engleski i srpski) i teme (svijetla, siva, tamna).

## Funkcionalnosti

- Upravljanje pacijentima: Prikaz pacijenata, pretraga po imenu, te prikaz medicinske istorije pacijenata.
- Intervencije i pregledi: Kreiranje novih intervencija, pregleda i dijagnoza, kao i izdavanje uputnica.
- Upravljanje korisničkim nalogom: Korisnik ima mogućnost mijenjanja svog korisničkog imena, lozinke, imena i prezimena
- Višejezična podrška: Prebacivanje između engleskog i srpskog jezika u realnom vremenu bez ponovnog pokretanja.
- Teme: Svijetla, plava i tamna tema sa dinamičkim promjenama.

## Način korištenja

### Prijava na sistem
- Na sistem se prijavljuje sa posebno pribavljenim korisničkim imenom i lozinkom
  
<img width="530" height="690" alt="LoginDark" src="https://github.com/user-attachments/assets/eef8c52c-4cb5-416f-9347-2647d13b5645" />

- Ukoliko su korisničko ime ili lozinka pogrešni, sistem obavještava korisnika porukom na ekranu

<img width="533" height="691" alt="LoginDarkPass" src="https://github.com/user-attachments/assets/0a120182-2c7c-4671-a077-498b46ab96f1" />

- Početna stranica pri unosu lozinke skriva unesene karaktere, ali klikom na ikonicu "oka" na desnom kraju oni postaju vidljivi/ili skriveni ponovnim klikom
  
<img width="532" height="692" alt="LoginDarkEye" src="https://github.com/user-attachments/assets/1ce11afe-7da6-49b1-9911-7f20fa567cd5" />

- Ukoliko je prijava bila uspješna, korisnik se preusmjerava ka adekvatnom dijelu aplikacije. Po prijavi se učitavaju detalji preferirane teme i jezika.

### Početni prozor ljekara
- Na vrhu ekrana se nalazi zaglavlje sa "Home" dugmetom koje korisnika vraća na ovu stranicu, naslov sistema, i meni za personalizaciju iskustva sa indikatorom trenutnog korisnika sistema
- Sa lijeve strane prozor sadrži Navigacioni meni sa opcijama pregleda pacijenata, intervencija i korisnikovog profila, dok je centralni dio rezervisan za pretragu pacijenata putem search bara, dugmad za kreiranje novog pregleda, intervencije i pregled izvjestaja, kao i uopšteni pregled pacijenata.

<img width="984" height="590" alt="DoctorHomePageSearchDark" src="https://github.com/user-attachments/assets/63dea0e8-2691-46b9-bde6-8cc83ef4db44" />

### Moje Intervencije
- Prikaz svih intervencija na kojima je učetvovao prijavljeni korisnik.
  
<img width="980" height="591" alt="MyInterventionsDark" src="https://github.com/user-attachments/assets/8a305baf-e3dc-4ee8-9069-af4cdeed005d" />

### Moj profil
- Prikaz ličnih podataka i mogućnost njihove izmjene pod određenim ograničenjima. Ime i prezime moraju samo početi velikim slovom i sadržavati samo slova. Korisničko ime mora biti bar 6 karaktera dugo, ne smije biti već upotrijebljeno i smije sadržati mala slova i znak "\.". Loznika se sastoji od velikih, malih slova, brojeva i karaktera "\!", "\#", "\$".

<img width="983" height="590" alt="MyProfileDark" src="https://github.com/user-attachments/assets/32cfb997-1e25-484d-b85f-6b1acf087131" /><img width="981" height="591" alt="MyProfileDarkErr" src="https://github.com/user-attachments/assets/4ed00d42-c079-405f-888f-8b9a9811ca5c" />

### Meni za personalizaciju
- Sadrži opcije za izmjenu profila, promjenu teme, jezika i odjavljivanje sa sistema.
  
<img width="990" height="596" alt="ProfileMenuDarkChangeTheme" src="https://github.com/user-attachments/assets/90d7269e-86e4-4ce1-a301-6120e2ebfe41" />
<img width="990" height="596" alt="ProfileMenuDarkChangeLanguage" src="https://github.com/user-attachments/assets/ac9ca98d-aca0-4806-a288-595806f6ca8b" />
<img width="990" height="596" alt="Logout" src="https://github.com/user-attachments/assets/f31b2e63-f5f0-4fa5-bc9b-fb1544dfb1ea" />

### Novi pregled
- Dugme za novi pregled otvara novi prozor. Unosom imena i prezimena u odgovarajuća polja, zatim klikom na dugme "Pronadji Dodaj Pacijenta" sistem učitava pacijenta iz baze, ukoliko je on redovan pacijent ambulante. U slučaju da pacijent nije prijavljeni pacijent ambulante, potrebno je OBAVEZNO popuniti ostatak polja te ponovo kliknuti na dugme da bi se pacijent sačuvao. 
<img width="981" height="728" alt="NewExaminationPart1" src="https://github.com/user-attachments/assets/4673bdb8-2cd3-4a9b-97b7-d0047cc41955" />

#### Pored tekstualnih polja za popunjavanje, postoje dva dugmeta za kreiranje dijagnoze i izdavanje uputnice u sklopu samog pregleda.
  
<img width="886" height="728" alt="NewExaminationPart2" src="https://github.com/user-attachments/assets/510a0791-f4f6-403e-aa36-462d9b463a67" />

##### Klikom na dugme "Dodaj Novu Dijagnozu" otvara se novi prozor.
- U ovom prozoru informacije o pacijentu i ljekaru su automatski popunjene, te se iz padajućeg menija bira dijagnoza i u polje ispod ljekar upisuje mišljenje o stanju i simptomima pacijenta. Na dugme "Potvrdi" se čuva informacija o zadatoj dijagnozi, i ljekar može dodati više dijagnoza u toku jednog pregleda. Na dugme "Otkaži" prekida se proces unosa dijagnoze.
 
<img width="386" height="493" alt="NewExaminationAddDiagnosis" src="https://github.com/user-attachments/assets/5e92815a-dc0a-4bc1-aabe-e7f510c26b83" />

##### Klikom na dugme "Dodaj Uputnicu" otvara se novi prozor. 
- U ovom prozoru informacije o pacijentu i ljekaru su automatski popunjene, te se iz padajućeg menija bira specijalizovana grana medicine gdje se preusmjerava pacijent, u polje ispod ljekar upisuje sumnju o stanju i simptomima pacijenta te zašto ga šalje tom specijalisti, i krajnje bira iz padajućeg menija prvobitnu dijagnozu. Na dugme "Potvrdi" se čuva informacija o izdatoj uputnici, i ljekar može izdati više uputnica u toku jednog pregleda. Na dugme "Otkaži" prekida se proces unosa uputnice.
  
<img width="486" height="593" alt="NewExaminationAddReferral" src="https://github.com/user-attachments/assets/45e4b900-f44e-425f-85ee-67f463e19806" />

### Nova intervencija
- Dugme za novu intervenciju otvara novi prozor. Unosom imena i prezimena u odgovarajuća polja, zatim klikom na dugme "Pronadji Dodaj Pacijenta" sistem učitava pacijenta iz baze, ukoliko je on redovan pacijent ambulante. U slučaju da pacijent nije prijavljeni pacijent ambulante, potrebno je OBAVEZNO popuniti ostatak polja te ponovo kliknuti na dugme da bi se pacijent sačuvao.
  
<img width="886" height="693" alt="InterventionView" src="https://github.com/user-attachments/assets/3c0c5053-10d9-4483-a520-f3596f9e335e" />

- Na jednoj intervenciji može biti zaduženo više ljekara ili medicinskih tehničara, te se u sekciji "Tim stručnjaka" iz jedno padajućeg menija bira zaposleni, a iz drugog njegova uloga u intervenciji. Može biti dodato više članova, te ih je moguće i ukloniti. Ljekar koji dodaje intervenciju je obavezno "Dežurni ljekar"/"Primary Doctor".
<img width="882" height="691" alt="InterventionViewPt2" src="https://github.com/user-attachments/assets/7f732903-c0fc-403e-81a6-555e38923308" />

- Na dugme "Dodaj Lijek" se otvara novi prozor u kome se popunjavaju podaci o datoj terapiji tokom intervencije.
<img width="886" height="693" alt="InterventionViewPt3" src="https://github.com/user-attachments/assets/20056db2-7b35-4ce6-b7d9-ee2b688f8ada" />

-Prozor za unos terapije sadrži padajući meni za izbog lijeka, i polje za unos količine koja je data pacijentu. Na dugme DODAJ, terapija se dodaje u prethodni prozor. Takođe, moguće je dodati više terapija u toku jedne intervencije. Na dugme Otkaži, prekida se unos terapije i stanje ostaje nepromijenjeno.
<img width="386" height="293" alt="InterventionViewAddTherapy" src="https://github.com/user-attachments/assets/6d98a5d1-0e04-4597-9379-605cf9254b56" />

- Intervencija se sačuva klikom na dugme SAČUVAJ. Dugme Otkaži poništava sve informacije i izlazi iz ovog prozora.
<img width="886" height="693" alt="InterventionViewPt3" src="https://github.com/user-attachments/assets/20056db2-7b35-4ce6-b7d9-ee2b688f8ada" />

### Mjesečni izvještaj
- Pomoću kalendara se bira datum na osnovu kog se generiše izvještaj i prikazuje u ovom prozoru. Izvještaj predstavlja statistiku svakog ljekara, broja pacijenata, broja intervencija i pregleda u periodu od 30 (trideset) dana unazad od izabranog datuma. Klikom na dugme ZATVORI, zatvara se prozor.
<img width="886" height="443" alt="MonthlyReport" src="https://github.com/user-attachments/assets/fc69baae-5db0-44ca-a57b-3de4e1986f39" />

### Medicinska istorija pacijenta (Karton)
- Medicinska istorija sadrži osnovne informacije o pacijentu, poput: imena, prezimena, datuma rodjenja, pola, alergijskim reakcijama, mjesta stanovanja, JMB,  osiguranja, i kratkih sadržaja o svim njegovim pregledima, uputnicama i dijagnozama.
  #### Pregledi
  - Pregledi sadrže informaciju o datumu pregleda, ljekaru koji je obavio pregled, simptomima i tipu pregleda.
<img width="786" height="593" alt="MedicalRecordReferrals" src="https://github.com/user-attachments/assets/3a9f5529-6669-4b64-b3b1-36ab21eecb64" />

#### Uputnice
- Uputnice sadrže informaciju o datumu izdavanja, ljekaru koji ih izdaje, šifru dijagnoze i ljekara specijalizovane medicine.
  
  <img width="786" height="593" alt="MedicalRecordReferrals" src="https://github.com/user-attachments/assets/22c91b16-8647-43c5-9269-f880096fcd74" />

#### Dijagnoze
- Dijagnoze sadrže informaciju o datumu izdavanja, ljekaru koji ih izdaje, mišljenje ljekara, i šifru dijagnoze.
  
<img width="786" height="593" alt="MedicalRecordDiagnosis" src="https://github.com/user-attachments/assets/0b0a6b15-53fb-4e51-93bf-a75f9ef2c41c" />


### Odjava sa sistema
- Odjava se dešava klikom na opciju u meniju profila

  <img width="990" height="596" alt="Logout" src="https://github.com/user-attachments/assets/1b954cbf-b93e-43ad-91fe-bc1ff16d9b41" />


## Instalacija

- Klonirajte repozitorijum:textgit clone https://github.com/yourusername/AmbulanceWPF.git
- Otvorite projekat u Visual Studio (preporučeno 2022 ili novije).
- Instalirajte NuGet pakete:
-- Microsoft.EntityFrameworkCore.Sqlite
-- Microsoft.EntityFrameworkCore.Tools
-- MaterialDesignThemes
- Kreirajte bazu podataka:   Update-Database
- Pokrenite aplikaciju (F5).
