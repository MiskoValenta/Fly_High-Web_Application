"use client";

import React, { useState } from 'react';
import Link from 'next/link';
import { usePathname, useRouter } from 'next/navigation';
import { IoMenu, IoClose, IoHomeOutline, IoPeopleOutline, IoBasketballOutline, IoLogOutOutline } from "react-icons/io5";
import { fetchWithAuth } from '@/lib/apiClient'; // <-- PŘIDANÝ IMPORT
import './SidebarDashboard.css';

export default function SidebarDashboard() {
    const pathname = usePathname();
    const router = useRouter();
    const [isMobileOpen, setIsMobileOpen] = useState(false);

    const handleLogout = async () => {
        try {
            // Zavoláme backend pro smazání cookies
            await fetchWithAuth('/Auth/logout', { method: 'POST' });
        } catch (error) {
            console.error("Logout failed:", error);
        } finally {
            // I kdyby to selhalo, přesměrujeme uživatele pryč
            router.push('/');
        }
    };

    const toggleMobileMenu = () => setIsMobileOpen(!isMobileOpen);

    // ... (zbytek JSX pro renderování sidebaru je stejný jako prve)
    return (
        <>
            <div className="mobile-sidebar-toggle">
                <div className="mobile-logo-text">FlyHigh</div>
                <button onClick={toggleMobileMenu} className="mobile-toggle-btn">
                    {isMobileOpen ? <IoClose size={28} /> : <IoMenu size={28} />}
                </button>
            </div>

            <aside className={`sidebar-dashboard ${isMobileOpen ? 'open' : ''}`}>
                <div className="sidebar-dashboard-logo">
                    <Link href="/Dashboard" onClick={() => setIsMobileOpen(false)}>FlyHigh</Link>
                </div>

                <nav className="sidebar-dashboard-nav">
                    <Link href="/Dashboard" className={pathname === '/Dashboard' ? 'active' : ''} onClick={() => setIsMobileOpen(false)}>
                        <IoHomeOutline className="sidebar-icon" /> Přehled
                    </Link>
                    <Link href="/Dashboard/Teams" className={pathname.startsWith('/Dashboard/Teams') ? 'active' : ''} onClick={() => setIsMobileOpen(false)}>
                        <IoPeopleOutline className="sidebar-icon" /> Týmy
                    </Link>
                    <Link href="/Dashboard/Matches" className={pathname.startsWith('/Dashboard/Matches') ? 'active' : ''} onClick={() => setIsMobileOpen(false)}>
                        <IoBasketballOutline className="sidebar-icon" /> Zápasy
                    </Link>
                </nav>

                <div className="sidebar-dashboard-footer">
                    <button onClick={handleLogout} className="logout-btn">
                        <IoLogOutOutline size={20} /> Odhlásit se
                    </button>
                </div>
            </aside>
            {isMobileOpen && <div className="sidebar-overlay" onClick={() => setIsMobileOpen(false)}></div>}
        </>
    );
}