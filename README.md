# PVA4 - Programování a vývoj aplikací
## MaturitniApp - Praktická část závěrečné maturitní práce

Tento repozitář je **pracovní prostor pro praktickou část** závěrečné maturitní práce z předmětu Programování a vývoj aplikací.

👉 Nejdůležitější dokument: **`zadani.md`** (musí být vyplněný).

---

## Povinné minimum pro všechny projekty
Každý projekt musí obsahovat:

### Databáze
- Aplikace pracuje s databází (MySQL/MariaDB).
- Databáze obsahuje **minimálně 5 tabulek**.
- Jedna z tabulek je `users` (uživatelské účty).

### Uživatelské účty
- Registrace uživatele (unikátní e-mail/login).
- Přihlášení a odhlášení.
- Hesla jsou ukládána pouze jako **hash** (nikdy ne v prostém textu).

### Funkčnost aplikace
- Aplikace obsahuje **CRUD** nad hlavní doménovou entitou (tu popíšeš v `zadani.md`).
- Aplikace má jasně oddělené části pro nepřihlášeného a přihlášeného uživatele (chráněné stránky/akce).

### Bezpečnost a validace
- Vstupy od uživatele jsou validované (prázdné hodnoty, formát, délky).
- Databázové dotazy jsou realizovány přes **prepared statements** (ochrana proti SQL Injection).
- Výstupy jsou ošetřené proti **XSS** (escape při výpisu do HTML).

### Dokumentace
- Repo obsahuje návod „Jak spustit projekt“ (viz níže).
- Repo obsahuje přihlašovací údaje pro testování (nebo postup, jak je vytvořit).

---

## Pravidla práce
- Pracuj **průběžně**, ne jednorázově.
- Commity musí mít **smysluplné zprávy** a musí být z nich vidět postup.
- Praktická část musí odpovídat tvému zadání v `zadani.md`.

Doporučení:
- Pro každou etapu pracuj v samostatné větvi `etapa-<cislo>-<tema>` a odevzdávej ji přes **Pull Request** do `main`.

---

## Odevzdávání (Git, commity, zákaz uploadu)
Práci odevzdáváš **průběžně přes Git**. Cílem je, aby z historie repozitáře byl jasně vidět postup
(co přibylo, co se opravilo, kdy ses posunul).

### Pravidla odevzdávání
- Po dokončení **každé funkce / logického kroku** proveď commit (ne jeden commit na konci).
- **Minimální očekávání:** alespoň **8–12 smysluplných commitů** rozložených v čase.
- Každý commit musí mít **konkrétní zprávu** (např. `Create users table`, `Implement login`, `Add input validation`, `Add CRUD for orders`).
- Commity dělej tak, aby byl projekt po každém commitu v rozumném stavu (ideálně spustitelný).

### Zákazy
- Je zakázáno odevzdat projekt jako „hotové všechno najednou“ v jednom nebo pár commitech.
- Je zakázáno nahrávat zdrojáky přes webové rozhraní GitHubu („Upload files“, editace souborů v prohlížeči).
  Odevzdání musí probíhat přes Git z počítače (commit + push).
- Pokud bude historie commitů nepoužitelná (např. 1–2 commity na konci nebo upload přes GitHub),
  bude to hodnoceno jako **nesplnění požadavků na odevzdání**.

---

## Etapy (milníky) práce
Práce bude kontrolovaná průběžně po etapách. Každá etapa se odevzdává jako **Pull Request**.

### Etapa 1: Zadání + návrh databáze
Povinné výstupy:
- Vyplněný `zadani.md` (název, cíl, technologie, funkční požadavky).
- Návrh databáze: tabulky a vztahy (doporučeně do `doc/db.md`).
- Seznam minimálně **5 tabulek**, včetně `users`.

### Etapa 2: Spustitelný základ projektu
Povinné výstupy:
- Projekt jde spustit podle Vašeho návodu (XAMPP / lokální server).
- Připojení na databázi je funkční.
- Základní struktura aplikace (složky, routování / stránky).

### Etapa 3: Registrace / přihlášení / odhlášení (společné pro všechny)
Povinné výstupy:
- Registrace uživatele (unikátní e-mail/login).
- Přihlášení a odhlášení.
- Hesla jsou ukládána jako **hash**.
- Validace vstupů a smysluplné chybové hlášky (např. špatné heslo, neplatný e-mail).

### Kde řešíš vlastní scope projektu (tvé zadání)
Etapy 1–3 jsou záměrně **společné pro všechny**, aby měl každý projekt stejný technický základ (návrh DB, spustitelný projekt, registrace/přihlášení).

**Tvoje vlastní funkce podle zadání v `zadani.md` začneš programovat hlavně od Etapy 4 dál:**
- **Etapa 4 (CRUD hlavní entity)** – jádro tvé aplikace: doménová logika a práce s hlavní entitou (např. zakázky, rezervace, skladové položky, objednávky…).
- **Etapa 5 (Oprávnění a ochrana přístupu)** – pravidla a procesy specifické pro tvůj projekt (např. role, stavy, schvalování, omezení akcí).
- **Etapa 6 (Dokumentace a finalizace)** – dokončení individuálních funkcí ze zadání, doladění chyb a příprava projektu tak, aby šel snadno spustit a zkontrolovat.

### Etapa 4: CRUD hlavní entity
Povinné výstupy:
- CRUD nad hlavní entitou dle zadání:
  - vytvoření, seznam, detail, úprava, odstranění
- Vyhledávání nebo filtrování (alespoň 1 parametr).

### Etapa 5: Oprávnění a ochrana přístupu
Povinné výstupy:
- Nepřihlášený uživatel nemá přístup k chráněným částem aplikace.
- Akce měnící data (create/update/delete) jsou chráněné (minimálně přihlášením).
- (Dle zadání) role user/admin nebo ekvivalentní mechanismus oprávnění.

### Etapa 6: Dokumentace a finalizace
Povinné výstupy:
- Dokončená dokumentace spuštění a testování (viz níže).
- Export databáze nebo seed data pro snadné vyzkoušení.
- Vyčištěný repozitář (žádná hesla, žádné dočasné soubory, přehledná struktura).

---

## Návod spuštění projektu (povinná kapitola)
Do `README.md` nebo `doc/` doplň:

- Požadavky (např. PHP 8.x, MySQL/MariaDB, XAMPP).
- Postup instalace/spuštění krok za krokem:
  - kam nakopírovat projekt / jak spustit server
  - jak vytvořit databázi
  - jak importovat strukturu a data (SQL soubor)
- Přihlašovací údaje pro testování (nebo postup, jak vytvořit prvního uživatele).

---

## Zadání
Vyplň soubor **`zadani.md`**.

### Ukázka funkčních požadavků (checklist)
- [ ] Aplikace používá databázi s minimálně 5 tabulkami (včetně `users`).
- [ ] Registrace uživatele pomocí e-mailu a hesla.
- [ ] Přihlášení a odhlášení uživatele.
- [ ] Nepřihlášený uživatel nemá přístup k chráněným částem aplikace.
- [ ] CRUD nad hlavní entitou podle zadání (např. zakázky/rezervace/produkty…).
- [ ] Vstupy jsou validované.
- [ ] Dotazy do databáze jsou přes prepared statements.
- [ ] Výstupy jsou ošetřené proti XSS.

---

## AI (pokud ji použiješ)
Pokud použiješ AI nástroje, veď krátký záznam do `doc/ai-log.md`:
- co jsi zadal (prompt),
- co ti nástroj vrátil,
- co jsi upravil a proč.

Odpovědnost za výsledek je vždy na tobě.
