"use client";

import React, { useState } from "react";
import "@/app/globals.css";
import "./Login.css";
import { IoClose } from "react-icons/io5";
import { useRouter } from "next/navigation";

interface LoginModalProps {
    isOpen: boolean;
    onClose: () => void;
}

export default function LoginModal({ isOpen, onClose }: LoginModalProps) {
    const [isRegister, setIsRegister] = useState(false);

    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const router = useRouter();

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setIsLoading(true);

        try {
            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/auth/login`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                credentials: "include",
                body: JSON.stringify({ email, password }),
            });

            if (res.ok) {
                router.push("/Dashboard");
                onClose();
            } else {
                const data = await res.json();
                setError(data.message || "Chyba při přihlášení");
            }
        } catch (err) {
            setError("Chyba serveru. Zkuste to prosím později.");
        } finally {
            setIsLoading(false);
        }
    };

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");

        if (password !== confirmPassword) {
            setError("Hesla se neshodují");
            return;
        }

        setIsLoading(true);

        try {
            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/auth/register`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                credentials: "include",
                body: JSON.stringify({ firstName, lastName, email, password }),
            });

            if (res.ok) {
                router.push("/Dashboard");
                onClose();
            } else {
                const data = await res.json();
                setError(data.message || "Registrace selhala. Tento email už pravděpodobně někdo využívá.");
            }
        } catch (err) {
            setError("Chyba serveru. Zkuste to prosím později.");
        } finally {
            setIsLoading(false);
        }
    };

    if (!isOpen) return null;

    const toggleMode = () => {
        setIsRegister(!isRegister);
        setError("");
    };

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

                {error && (
                    <div className="LoginError">
                        {error}
                    </div>
                )}

                <form className="LoginForm" onSubmit={isRegister ? handleRegister : handleLogin}>

                    {isRegister && (
                        <>
                            <div className="InputGroup">
                                <input
                                    type="text"
                                    placeholder="Křestní jméno"
                                    className="LoginInput"
                                    value={firstName}
                                    onChange={(e) => setFirstName(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="InputGroup">
                                <input
                                    type="text"
                                    placeholder="Příjmení"
                                    className="LoginInput"
                                    value={lastName}
                                    onChange={(e) => setLastName(e.target.value)}
                                    required
                                />
                            </div>
                        </>
                    )}

                    <div className="InputGroup">
                        <input
                            type="email"
                            placeholder="Email"
                            className="LoginInput"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>

                    <div className="InputGroup">
                        <input
                            type="password"
                            placeholder="Heslo"
                            className="LoginInput"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>

                    {isRegister && (
                        <div className="InputGroup">
                            <input
                                type="password"
                                placeholder="Potvrzení hesla"
                                className="LoginInput"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                required
                            />
                        </div>
                    )}

                    <button
                        type="submit"
                        className="LoginSubmitBtn"
                        disabled={isLoading}
                    >
                        {isLoading
                            ? (isRegister ? "Registruji..." : "Přihlašuji...")
                            : (isRegister ? "Zaregistrovat se" : "Přihlásit se")
                        }
                    </button>
                </form>

                <div className="LoginFooter">
                    {!isRegister && (
                        <a href="#" className="ForgotPasswordLink">Zapomněli jste heslo?</a>
                    )}

                    <div className="SwitchMode">
                        <span>{isRegister ? "Máte již účet?" : "Nemáte účet?"}</span>
                        <button type="button" onClick={toggleMode} className="SwitchModeBtn">
                            {isRegister ? "Přihlaste se" : "Registrujte se"}
                        </button>
                    </div>
                </div>

            </div>
        </div>
    );
}