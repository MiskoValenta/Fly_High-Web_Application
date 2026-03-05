// app/Dashboard/page.tsx
"use client";

import React from 'react';
import './Dashboard.css';

export default function DashboardPage() {
    return (
        <div className="dashboard-container">
            <h1 className="dashboard-heading">Přehled</h1>
            <p className="dashboard-subtext">Vítejte zpět v aplikaci FlyHigh! Zde je tvůj rychlý přehled.</p>

            <div className="stats-grid">
                <div className="stat-card glass-card">
                    <div className="stat-value"></div>
                    <div className="stat-label">Moje Týmy</div>
                </div>
                <div className="stat-card glass-card">
                    <div className="stat-value"></div>
                    <div className="stat-label">Odehrané zápasy</div>
                </div>
                <div className="stat-card glass-card">
                    <div className="stat-value"></div>
                    <div className="stat-label">Nadcházející</div>
                </div>
            </div>

            <div className="dashboard-info glass-card">
                <h2>Rychlá Akce</h2>
                <p>Z levého menu můžete spravovat své týmy, vytvářet nové zápasy a sledovat statistiky.</p>
                <div className="action-buttons">
                    <a href="/Dashboard/CreateTeam" className="btn-primary">Vytvořit Tým</a>
                    <a href="/Dashboard/Matches/Create" className="btn-secondary">Naplánovat Zápas</a>
                </div>
            </div>
        </div>
    );
}