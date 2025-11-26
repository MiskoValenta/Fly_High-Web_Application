import "@/app/globals.css";
import "./About.css";
import { IoStatsChartOutline, IoBookOutline, IoCalendarOutline, IoPeopleOutline } from "react-icons/io5";

export default function About() {
    return (
        <main>

            <div className="section-container">
                <section className="AboutHero">
                    <div className="AboutHero-inner">
                        <div className="AboutHeading">O projektu Fly High</div>
                        <div className="AboutSubHeading">
                            Fly High je moderní volejbalový manažerský systém navržený pro hráče,
                            trenéry i celé týmy. Cílem je odstranit chaos z chatů a tabulek a
                            přenést vše do jednoho přehledného nástroje.
                        </div>

                        <div className="AboutGrid">
                            <div className="AboutCard">
                                <h3>Pro týmy</h3>
                                <p>
                                    Přehledná správa hráčů, docházky a týmové komunikace na jednom místě.
                                </p>
                            </div>

                            <div className="AboutCard">
                                <h3>Pro trenéry</h3>
                                <p>
                                    Jednoduché plánování tréninků, sestav a dlouhodobý rozvoj hráčů.
                                </p>
                            </div>

                            <div className="AboutCard">
                                <h3>Pro hráče</h3>
                                <p>
                                    Vlastní statistiky, cíle a jasný přehled o výkonnosti.
                                </p>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <div className="section-container">
                <div className="AboutCardsRow">
                    <div className="AboutHeading" style={{ textAlign: "center", marginBottom: "3rem" }}>
                        Proč tato aplikace vznikla?
                    </div>

                    <div className="AboutRowGrid">

                        <div className="AboutCard collapsed">
                            <h3>My Story</h3>
                            <p>
                                Volejbal hraji už řadu let, ale vždy mi v týmu chyběl jeden zásadní prvek – jednoduchý nástroj, který by nás spojoval. Místo abychom se soustředili na výkon, ztráceli jsme čas organizací. Chtěl jsem vytvořit místo, kde hráč i trenér najdou vše potřebné: od statistik až po přehled o výkonech.
                            </p>
                        </div>

                        <div className="AboutCard collapsed">
                            <h3>The Problem</h3>
                            <p>
                                Většina amatérských a školních týmů funguje stále stejně jako v roce 2005. Trenéři si píší docházku na papírky, které se ztrácejí. Statistiky ze zápasů buď neexistují, nebo končí v nepřehledných poznámkách. Komunikace probíhá chaoticky přes chatovací aplikace. Chyběla centralizace a data, o která by se dalo opřít při zlepšování hry.
                            </p>
                        </div>

                        <div className="AboutCard collapsed">
                            <h3>Why this project</h3>
                            <p>
                                Fly High není jen splnění školní povinnosti. Je to spojení mé vášně pro sport a IT. Tento projekt mi umožnil aplikovat znalosti moderního vývoje (UX design, automatizace procesů, sportovní analýza) do reálného produktu. Cílem bylo ukázat, že maturitní práce může mít skutečný dopad a nemusí skončit "v šuplíku".
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <div className="section-container">
                <div className="AboutHero">
                    <div className="AboutHeading">What the App Does</div>

                    <div className="AboutCardGrid">
                        <div className="AboutCardGridWindow">
                            <div className="IconCard"><IoPeopleOutline /></div>
                            <p className="AboutGridText">Správa týmů</p>
                            <div className="HoverText">Jednoduchá organizace soupisky.</div>
                        </div>

                        <div className="AboutCardGridWindow">
                            <div className="IconCard"><IoStatsChartOutline /></div>
                            <p className="AboutGridText">Statistiky hráčů</p>
                            <div className="HoverText">Sledování individuálního progresu.</div>
                        </div>

                        <div className="AboutCardGridWindow">
                            <div className="IconCard"><IoBookOutline /></div>
                            <p className="AboutGridText">Zápisy zápasů</p>
                            <div className="HoverText">Digitalizace výsledků v reálném čase.</div>
                        </div>

                        <div className="AboutCardGridWindow">
                            <div className="IconCard"><IoCalendarOutline /></div>
                            <p className="AboutGridText">Přehled tréninků</p>
                            <div className="HoverText">Kdo, kdy a jak trénoval.</div>
                        </div>
                    </div>
                </div>
            </div>

        </main>
    );
}
