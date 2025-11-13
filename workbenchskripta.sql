-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8mb3 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`telefon`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`telefon` (
  `BrojTelefona` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`BrojTelefona`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`zaposleni`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`zaposleni` (
  `JMB` CHAR(13) NOT NULL,
  `Ime` VARCHAR(45) NOT NULL,
  `Prezime` VARCHAR(45) NOT NULL,
  `KorisnickoIme` VARCHAR(45) NOT NULL,
  `Lozinka` VARCHAR(45) NOT NULL,
  `Uloga` VARCHAR(45) NOT NULL,
  `Aktivnost` INT NOT NULL,
  `Tema` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`JMB`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`ljekar`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`ljekar` (
  `JMB` CHAR(13) NOT NULL,
  `BrojTelefona` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`JMB`),
  INDEX `fk_Ljekar_Telefon1_idx` (`BrojTelefona` ASC) VISIBLE,
  INDEX `fk_Ljekar_Zaposleni1_idx` (`JMB` ASC) VISIBLE,
  CONSTRAINT `fk_Ljekar_Telefon1`
    FOREIGN KEY (`BrojTelefona`)
    REFERENCES `mydb`.`telefon` (`BrojTelefona`),
  CONSTRAINT `fk_Ljekar_Zaposleni1`
    FOREIGN KEY (`JMB`)
    REFERENCES `mydb`.`zaposleni` (`JMB`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`mjesto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`mjesto` (
  `PostanskiBroj` INT NOT NULL,
  `Naziv` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`PostanskiBroj`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`pacijent`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`pacijent` (
  `JMBPacijenta` CHAR(13) NOT NULL,
  `Ime` VARCHAR(45) NOT NULL,
  `Prezime` VARCHAR(45) NOT NULL,
  `MjestoPrebivalista` INT NOT NULL,
  PRIMARY KEY (`JMBPacijenta`),
  INDEX `fk_Pacijent_Mjesto2_idx` (`MjestoPrebivalista` ASC) VISIBLE,
  CONSTRAINT `fk_Pacijent_Mjesto2`
    FOREIGN KEY (`MjestoPrebivalista`)
    REFERENCES `mydb`.`mjesto` (`PostanskiBroj`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`karton`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`karton` (
  `JMBPacijenta` CHAR(13) NOT NULL,
  `ImeRoditelja` VARCHAR(45) NOT NULL,
  `BracnoStanje` VARCHAR(10) NOT NULL,
  `Pol` TINYINT NOT NULL,
  `Osiguranje` TINYINT NOT NULL,
  `JMBLjekara` CHAR(13) NOT NULL,
  PRIMARY KEY (`JMBPacijenta`),
  INDEX `fk_Karton_Pacijent1_idx` (`JMBPacijenta` ASC) VISIBLE,
  INDEX `fk_Karton_Ljekar1_idx` (`JMBLjekara` ASC) VISIBLE,
  CONSTRAINT `fk_Karton_Ljekar1`
    FOREIGN KEY (`JMBLjekara`)
    REFERENCES `mydb`.`ljekar` (`JMB`),
  CONSTRAINT `fk_Karton_Pacijent1`
    FOREIGN KEY (`JMBPacijenta`)
    REFERENCES `mydb`.`pacijent` (`JMBPacijenta`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`sifarnik_bolesti`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`sifarnik_bolesti` (
  `SifraBolesti` INT NOT NULL,
  `NazivBolesti` VARCHAR(45) NOT NULL,
  `Opis` MEDIUMTEXT NOT NULL,
  `DatumAzuriranja` DATE NOT NULL,
  PRIMARY KEY (`SifraBolesti`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`Pregled`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Pregled` (
  `IdPregleda` INT NOT NULL AUTO_INCREMENT,
  `DatumPregleda` DATETIME NOT NULL,
  `OpisPregleda` VARCHAR(45) NULL,
  `JMBPacijenta` CHAR(13) NOT NULL,
  `JMBLjekara` CHAR(13) NOT NULL,
  PRIMARY KEY (`IdPregleda`),
  INDEX `fk_Pregled_pacijent1_idx` (`JMBPacijenta` ASC) VISIBLE,
  INDEX `fk_Pregled_ljekar1_idx` (`JMBLjekara` ASC) VISIBLE,
  -- ADD THIS COMPOSITE UNIQUE INDEX FOR THE FOREIGN KEY
  UNIQUE INDEX `unique_pregled_composite` (`IdPregleda` ASC, `JMBPacijenta` ASC, `JMBLjekara` ASC) VISIBLE,
  CONSTRAINT `fk_Pregled_pacijent1`
    FOREIGN KEY (`JMBPacijenta`)
    REFERENCES `mydb`.`pacijent` (`JMBPacijenta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Pregled_ljekar1`
    FOREIGN KEY (`JMBLjekara`)
    REFERENCES `mydb`.`ljekar` (`JMB`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`dijagnoza`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`dijagnoza` (
  `JMBPacijenta` CHAR(13) NOT NULL,
  `SifraBolesti` INT NOT NULL,
  `Datum` DATE NOT NULL,
  `MisljenjeLjekara` MEDIUMTEXT NULL DEFAULT NULL,
  `IdPregleda` INT NOT NULL,
  `JMBLjekara` CHAR(13) NOT NULL,
  PRIMARY KEY (`JMBPacijenta`, `SifraBolesti`, `Datum`),
  INDEX `fk_Dijagnoza_Sifarnik_bolesti1_idx` (`SifraBolesti` ASC) VISIBLE,
  INDEX `fk_Dijagnoza_Karton1_idx` (`JMBPacijenta` ASC) VISIBLE,
  INDEX `fk_dijagnoza_Pregled1_idx` (`IdPregleda` ASC, `JMBPacijenta` ASC, `JMBLjekara` ASC) VISIBLE,
  CONSTRAINT `fk_Dijagnoza_Karton1`
    FOREIGN KEY (`JMBPacijenta`)
    REFERENCES `mydb`.`karton` (`JMBPacijenta`),
  CONSTRAINT `fk_Dijagnoza_Sifarnik_bolesti1`
    FOREIGN KEY (`SifraBolesti`)
    REFERENCES `mydb`.`sifarnik_bolesti` (`SifraBolesti`),
  CONSTRAINT `fk_dijagnoza_Pregled1`
    FOREIGN KEY (`IdPregleda`, `JMBPacijenta`, `JMBLjekara`)
    REFERENCES `mydb`.`Pregled` (`IdPregleda`, `JMBPacijenta`, `JMBLjekara`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`intervencija`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`intervencija` (
  `IdIntervencije` INT NOT NULL AUTO_INCREMENT,
  `JMBPacijenta` CHAR(13) NOT NULL,
  `Datum` DATE NOT NULL,
  `OpisIntervencije` MEDIUMTEXT NOT NULL,
  PRIMARY KEY (`IdIntervencije`),
  INDEX `fk_Intervencija_Pacijent1_idx` (`JMBPacijenta` ASC) VISIBLE,
  CONSTRAINT `fk_Intervencija_Pacijent1`
    FOREIGN KEY (`JMBPacijenta`)
    REFERENCES `mydb`.`pacijent` (`JMBPacijenta`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`intervencija_has_ljekar`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`intervencija_has_ljekar` (
  `IdIntervencije` INT NOT NULL,
  `JMBLjekara` CHAR(13) NOT NULL,
  PRIMARY KEY (`IdIntervencije`, `JMBLjekara`),
  INDEX `fk_Intervencija_has_Ljekar_Intervencija1_idx` (`IdIntervencije` ASC) VISIBLE,
  INDEX `fk_Intervencija_has_Ljekar_Ljekar1_idx` (`JMBLjekara` ASC) VISIBLE,
  CONSTRAINT `fk_Intervencija_has_Ljekar_Intervencija1`
    FOREIGN KEY (`IdIntervencije`)
    REFERENCES `mydb`.`intervencija` (`IdIntervencije`),
  CONSTRAINT `fk_Intervencija_has_Ljekar_Ljekar1`
    FOREIGN KEY (`JMBLjekara`)
    REFERENCES `mydb`.`ljekar` (`JMB`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`sifarnik_lijekova`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`sifarnik_lijekova` (
  `SifraLijeka` INT NOT NULL,
  `Naziv` VARCHAR(45) NOT NULL,
  `Proizvodjac` VARCHAR(45) NOT NULL,
  `Aktivnost` TINYINT(1) NULL DEFAULT '1',
  PRIMARY KEY (`SifraLijeka`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`inventar_lijekova`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`inventar_lijekova` (
  `SifraLijekaUInventaru` INT NOT NULL,
  `Kolicina` DECIMAL(10,0) NOT NULL,
  PRIMARY KEY (`SifraLijekaUInventaru`),
  CONSTRAINT `fk_inventar_lijekova_sifarnik_lijekova1`
    FOREIGN KEY (`SifraLijekaUInventaru`)
    REFERENCES `mydb`.`sifarnik_lijekova` (`SifraLijeka`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`medicinski_tehnicar`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`medicinski_tehnicar` (
  `JMBTehnicara` CHAR(13) NOT NULL,
  PRIMARY KEY (`JMBTehnicara`),
  INDEX `fk_Medicinski_tehnicar_Zaposleni1_idx` (`JMBTehnicara` ASC) VISIBLE,
  CONSTRAINT `fk_Medicinski_tehnicar_Zaposleni1`
    FOREIGN KEY (`JMBTehnicara`)
    REFERENCES `mydb`.`zaposleni` (`JMB`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`nabavka`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`nabavka` (
  `IdNabavke` INT NOT NULL AUTO_INCREMENT,
  `Kolicina` DECIMAL(10,0) NULL DEFAULT NULL,
  `DatumNabavke` DATE NOT NULL,
  PRIMARY KEY (`IdNabavke`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`terapija`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`terapija` (
  `IdIntervencije` INT NOT NULL,
  `SifraLijeka` INT NOT NULL,
  `Doza` DECIMAL(10,2) NULL DEFAULT NULL,
  PRIMARY KEY (`IdIntervencije`, `SifraLijeka`),
  INDEX `fk_Terapija_Intervencija1_idx` (`IdIntervencije` ASC) VISIBLE,
  INDEX `fk_Terapija_Sifarnik_Lijekova1_idx` (`SifraLijeka` ASC) VISIBLE,
  CONSTRAINT `fk_Terapija_Intervencija1`
    FOREIGN KEY (`IdIntervencije`)
    REFERENCES `mydb`.`intervencija` (`IdIntervencije`),
  CONSTRAINT `fk_Terapija_Sifarnik_Lijekova1`
    FOREIGN KEY (`SifraLijeka`)
    REFERENCES `mydb`.`sifarnik_lijekova` (`SifraLijeka`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`uputnica`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`uputnica` (
  `IdUputnice` INT NOT NULL AUTO_INCREMENT,
  `SifraBolesti` INT NOT NULL,
  `Specijalisti` VARCHAR(45) NOT NULL,
  `JMBLjekara` CHAR(13) NOT NULL,
  `Datum` DATE NOT NULL,
  `IdPregleda` INT NOT NULL,
  PRIMARY KEY (`IdUputnice`),
  INDEX `fk_Uputnica_Sifarnik_bolesti1_idx` (`SifraBolesti` ASC) VISIBLE,
  INDEX `fk_Uputnica_Ljekar1_idx` (`JMBLjekara` ASC) VISIBLE,
  INDEX `fk_uputnica_Pregled1_idx` (`IdPregleda` ASC) VISIBLE,
  CONSTRAINT `fk_Uputnica_Ljekar1`
    FOREIGN KEY (`JMBLjekara`)
    REFERENCES `mydb`.`ljekar` (`JMB`),
  CONSTRAINT `fk_Uputnica_Sifarnik_bolesti1`
    FOREIGN KEY (`SifraBolesti`)
    REFERENCES `mydb`.`sifarnik_bolesti` (`SifraBolesti`),
  CONSTRAINT `fk_uputnica_Pregled1`
    FOREIGN KEY (`IdPregleda`)
    REFERENCES `mydb`.`Pregled` (`IdPregleda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`artikal_lijeka`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`artikal_lijeka` (
  `SifraLijeka` INT NOT NULL,
  `IdNabavke` INT NOT NULL,
  `Kolicina` INT NOT NULL,
  PRIMARY KEY (`SifraLijeka`, `IdNabavke`),
  INDEX `fk_artikal_lijeka_nabavka1_idx` (`IdNabavke` ASC) VISIBLE,
  CONSTRAINT `fk_artikal_lijeka_sifarnik_lijekova1`
    FOREIGN KEY (`SifraLijeka`)
    REFERENCES `mydb`.`sifarnik_lijekova` (`SifraLijeka`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_artikal_lijeka_nabavka1`
    FOREIGN KEY (`IdNabavke`)
    REFERENCES `mydb`.`nabavka` (`IdNabavke`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;