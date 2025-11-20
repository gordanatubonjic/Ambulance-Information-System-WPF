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
- Sa lijeve strane prozor sadrži Navigacioni meni sa opcijama pregleda pacijenata, intervencija i korisnikovog profila, dok je centralni dio rezervisan za pretragu pacijenata, dugmad za kreiranje novog pregleda, intervencije i pregled izvjestaja, kao i uopšteni pregled pacijenata.

<img width="984" height="590" alt="DoctorHomePageSearchDark" src="https://github.com/user-attachments/assets/63dea0e8-2691-46b9-bde6-8cc83ef4db44" />

### Moje Intervencije
- Prikaz svih intervencija na kojima je učetvovao prijavljeni korisnik.
  
<img width="980" height="591" alt="MyInterventionsDark" src="https://github.com/user-attachments/assets/8a305baf-e3dc-4ee8-9069-af4cdeed005d" />

### Moj profil
-Prikaz ličnih podataka i mogućnost njihove izmjene pod određenim ograničenjima. Ime i prezime moraju samo početi velikim slovom i sadržavati samo slova. Korisničko ime mora biti bar 6 karaktera dugo, ne smije biti već upotrijebljeno i smije sadržati mala slova i znak "\.". Loznika se sastoji od velikih, malih slova, brojeva i karaktera "\!", "\#", "\$".

<img width="983" height="590" alt="MyProfileDark" src="https://github.com/user-attachments/assets/32cfb997-1e25-484d-b85f-6b1acf087131" /><img width="981" height="591" alt="MyProfileDarkErr" src="https://github.com/user-attachments/assets/4ed00d42-c079-405f-888f-8b9a9811ca5c" />


## Instalacija

- Klonirajte repozitorijum:textgit clone https://github.com/yourusername/AmbulanceWPF.git
- Otvorite projekat u Visual Studio (preporučeno 2022 ili novije).
- Instalirajte NuGet pakete:
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Tools
- MaterialDesignThemes
- Kreirajte bazu podataka:   Update-Database
- Pokrenite aplikaciju (F5).
