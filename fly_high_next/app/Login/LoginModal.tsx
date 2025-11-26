"use client";

import React, { useState, useEffect } from "react";
import "@/app/globals.css";
import "./Login.css";
import { IoClose } from "react-icons/io5";

interface LoginModalProps {
    isOpen: boolean;
    onClose: () => void;
}

export default function LoginModal({ isOpen, onClose }: LoginModalProps) {
    const [isRegister, setIsRegister] = useState(false);

    // Pokud je modal zavřený, nic se nevykreslí
    if (!isOpen) return null;

    // Funkce pro přepnutí režimu (Login <-> Register)
    const toggleMode = () => setIsRegister(!isRegister);

    const handleCardClick = (e: React.MouseEvent) => {
        e.stopPropagation();
    };

    return (
        <div className="LoginOverlay" onClick={onClose}>
            <div className="LoginCard" onClick={handleCardClick}>

                <button className="CloseButton" onClick={onClose}>
                    <IoClose />
                </button>

                <h2 className="LoginHeading">
                    {isRegister ? "Registrace" : "Přihlášení"}
                </h2>

                <p className="LoginSubText">
                    {isRegister
                        ? "Vytvořte si účet a spravujte svůj tým efektivně."
                        : "Vítejte zpět! Přihlašte se ke svému účtu."}
                </p>

                <form className="LoginForm">
                    {isRegister && (
                        <div className="InputGroup">
                            <input type="text" placeholder="Jméno a Příjmení" className="LoginInput" />
                        </div>
                    )}

                    <div className="InputGroup">
                        <input type="email" placeholder="Email" className="LoginInput" />
                    </div>

                    <div className="InputGroup">
                        <input type="password" placeholder="Heslo" className="LoginInput" />
                    </div>

                    {isRegister && (
                        <div className="InputGroup">
                            <input type="password" placeholder="Potvrzení hesla" className="LoginInput" />
                        </div>
                    )}

                    <button type="button" className="LoginSubmitBtn">
                        {isRegister ? "Zaregistrovat se" : "Přihlásit se"}
                    </button>
                </form>

                <div className="LoginFooter">
                    {!isRegister && (
                        <a href="#" className="ForgotPasswordLink">Zapomněli jste heslo?</a>
                    )}

                    <div className="SwitchMode">
                        <span>{isRegister ? "Máte již účet?" : "Nemáte účet?"}</span>
                        <button onClick={toggleMode} className="SwitchModeBtn">
                            {isRegister ? "Přihlaste se" : "Registrujte se"}
                        </button>
                    </div>
                </div>

            </div>
        </div>
    );
}