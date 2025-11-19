'use client';
import {useState} from "react"
import './Sidebar.css'

const navItems = ["Home", "About", "Contact", "","Login"]

export const Sidebar = () =>{

    const [isOpen, setIsOpen] = useState(false);

    return(
        <aside className={`sidebar ${isOpen ? "open" : ""}`}>
            <div className="inner">
                <header>
                    <button type="button" onClick={() => setIsOpen(!isOpen)}>
                        <span className="material...">
                            {isOpen ? "close" : "menu"}
                        </span>
                    </button>
                    <img/>
                </header>
                    <nav>
                        {navItems.map(item => (
                            <button key={item} type="button">
                                <span className="material...">{item}</span>
                                <p>{item}</p>
                            </button>
                        ))}
                    </nav>
            </div>
        </aside>
    )
}