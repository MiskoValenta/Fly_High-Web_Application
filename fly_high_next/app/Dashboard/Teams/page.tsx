// app/Dashboard/Teams/page.tsx
"use client";

import React, { useEffect, useState } from 'react';
import Link from 'next/link';
import { fetchWithAuth } from '@/lib/apiClient';
import { TeamResponseDto } from '@/types/team';
import './Teams.css';

export default function TeamsPage() {
    const [teams, setTeams] = useState<TeamResponseDto[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadTeams = async () => {
            try {
                const res = await fetchWithAuth('/Team/my');
                if (res.ok) {
                    const data = await res.json();
                    setTeams(data);
                }
            } catch (error) {
                console.error("Failed to load teams");
            } finally {
                setLoading(false);
            }
        };
        loadTeams();
    }, []);

    return (
        <div className="dashboard-container">
            <div className="teams-header">
                <div>
                    <h1 className="dashboard-heading">Moje Týmy</h1>
                    <p className="dashboard-subtext">Správa a přehled tvých týmů.</p>
                </div>
                <Link href="/Dashboard/CreateTeam" className="btn-primary">
                    + Vytvořit Tým
                </Link>
            </div>

            {loading ? (
                <p>Načítám týmy...</p>
            ) : teams.length === 0 ? (
                <div className="glass-card empty-state">
                    <p>Zatím nejsi v žádném týmu.</p>
                </div>
            ) : (
                <div className="teams-grid">
                    {teams.map((team) => (
                        <Link href={`/Dashboard/Teams/${team.id}`} key={team.id} className="team-card glass-card">
                            <div className="team-card-header">
                                <h3>{team.teamName}</h3>
                                <span className={`status-badge status-${team.status.toLowerCase()}`}>
                                    {team.status}
                                </span>
                            </div>
                            <p className="team-short-name">Zkratka: {team.shortName}</p>
                            <div className="team-role">
                                Role: <strong>{team.role}</strong>
                            </div>
                        </Link>
                    ))}
                </div>
            )}
        </div>
    );
}