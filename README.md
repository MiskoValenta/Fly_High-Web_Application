<h1>Maturitní projekt "Fly High"</h1>
- Cíl práce: Webová aplikace o zápisech volejbalových zápasů, zapsání hráčů k týmům  ankety k tréninkům a zápasům </br>
- Vedoucí práce: Mgr. Pavlína Mihačová

# Fly_High-Web_Application

## 🚀 Jak spustit projekt lokálně

### Požadavky
- Mít nainstalovaný Git  
- Mít nainstalovaný Node.js a balíčkovacího manažera (npm / yarn) (aspoň verzi doporučenou pro Next.js).  [oai_citation:0‡nextjs.org](https://nextjs.org/docs/app/getting-started/installation?utm_source=chatgpt.com)

---

## 1. Naklonovat repozitář

V terminálu spusť:

```bash
git clone https://github.com/MiskoValenta/Fly_High-Web_Application.git
```

Tím tento repozitář stáhneš do složky `Fly_High-Web_Application`.  [oai_citation:1‡GitHub Docs](https://docs.github.com/articles/cloning-a-repository?utm_source=chatgpt.com)

Pokud chceš rovnou klonovat do aktuální složky:

```bash
git clone https://github.com/MiskoValenta/Fly_High-Web_Application.git .
```

---

## 2. Otevřít projekt ve správné cestě

Přejdi do složky, kam se projekt stáhl:

```bash
cd Fly_High-Web_Application
```

Pokud máš více podsložek (např. `fly_high_next` — podle struktury repozitáře), přejdi do té, která obsahuje Next.js kód. Např.:

```bash
cd fly_high_next
```

---

## 3. Instalace závislostí

V kořenové složce Next.js projektu spusť:

```bash
npm install
```

(nebo `yarn install`, pokud používáš yarn). Tím nainstaluješ všechny závislosti definované v `package.json`.  [oai_citation:2‡Stack Overflow](https://stackoverflow.com/questions/75195648/how-do-i-run-next-js-app-after-cloning-repository?utm_source=chatgpt.com)

Pokud by instalace narazila na chyby s „peer dependencies“, může pomoci:

```bash
npm install --legacy-peer-deps
```

---

## 4. Spustit vývojový server

Po úspěšné instalaci závislostí spusť:

```bash
npm run dev
```

(nebo `yarn dev`) — to spustí Next.js v režimu vývoje. Poté můžeš otevřít prohlížeč a jít na:

```
http://localhost:3000
```

— tam by měla běžet tvoje aplikace.  [oai_citation:3‡Stack Overflow](https://stackoverflow.com/questions/75195648/how-do-i-run-next-js-app-after-cloning-repository?utm_source=chatgpt.com)

---

## 5. Tipy a často se vyskytující chyby

- `node_modules/` obvykle není součástí repozitáře (ignorováno v `.gitignore`), proto je nutné vždy spustit `npm install` po klonování.  [oai_citation:4‡Stack Overflow](https://stackoverflow.com/questions/75195648/how-do-i-run-next-js-app-after-cloning-repository?utm_source=chatgpt.com)  
- Ujisti se, že máš kompatibilní verzi Node.js (nejlépe doporučenou pro Next.js podle dokumentace).  [oai_citation:5‡nextjs.org](https://nextjs.org/docs/app/getting-started/installation?utm_source=chatgpt.com)  
- Pokud přidáš nové závislosti nebo změníš `package.json`, znovu spusť `npm install`, než projekt spustíš.  

---
