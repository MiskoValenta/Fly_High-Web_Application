'use client';
import "./Navbar.css"
import { Button } from "@/components/ui/button"
import Image from "next/image";
import Link from "next/link";
import Logo from "@/public/ballin.svg"

export const Navbar = () => {
    return (
        <nav className="navbar">
            <div className="container">

                {/* Levá část */}
                <div className="navbar-brand">
                    <a href={"/"}><Image className="brand-logo dark:invert dark:hue-rotate-180" src={Logo} alt="Logo" /></a>
                    <Link className="brand-name" href="/">Fly High</Link>
                </div>

                {/* Střední část - styly jsou nyní v CSS */}
                <div className="navbar-menu">
                    <ul>
                        <li><Link href="/">Features</Link></li>
                        <li><Link href="/">About</Link></li>
                        <li><Link href="/">FAQ</Link></li>
                        <li><Link href="/">Contact</Link></li>
                    </ul>
                </div>

                {/* Pravá část */}
                <div className="navbar-login">
                    <Button variant="default">
                        Login
                    </Button>
                </div>

            </div>
        </nav>
    );
}