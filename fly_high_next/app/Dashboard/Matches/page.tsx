// app/Dashboard/Matches/page.tsx
"use client";

import React, { useEffect, useState } from 'react';
import Link from 'next/link';
import { getMatches } from '@/lib/matchApi';
import { Match, MatchStatus } from '@/types/match';
import './Matches.css';

export default function MatchesPage() {
    const [matches, setMatches] = useState<Match[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchMatches = async () => {
            try {
                const data = await getMatches();
                setMatches(data);
            } catch (error) {
                console.error("Failed to load matches", error);
            } finally {
                setLoading(false);
            }
        };
        fetchMatches();
    }, []);

    const getStatusLabel = (status: MatchStatus) => {
        switch (status) {
            case MatchStatus.Proposed: return 'Navrženo';
            case MatchStatus.Scheduled: return 'Naplánováno';
            case MatchStatus.InProgress: return 'Probíhá';
            case MatchStatus.Finished: return 'Ukončeno';
            default: return 'Zrušeno / Zamítnuto';
        }
    };

    return (
        <div className="dashboard-container">
            <div className="matches-header">
                <div>
                    <h1 className="dashboard-heading">Zápasy</h1>
                    <p className="dashboard-subtext">Správa, plánování a výsledky vašich zápasů.</p>
                </div>
                <Link href="/Dashboard/Matches/Create" className="btn-primary">
                    + Nový Zápas
                </Link>
            </div>

            {loading ? (
                <p>Načítám zápasy...</p>
            ) : matches.length === 0 ? (
                <div className="glass-card empty-state">
                    <p>Zatím nemáte naplánované žádné zápasy.</p>
                </div>
            ) : (
                <div className="matches-grid">
                    {matches.map((match) => (
                        <Link href={`/Dashboard/Matches/${match.id}`} key={match.id} className="match-card glass-card">
                            <div className="match-card-header">
                                <span className={`match-status-badge status-${match.status}`}>
                                    {getStatusLabel(match.status)}
                                </span>
                                <span className="match-date">
                                    {new Date(match.scheduledAt).toLocaleDateString('cs-CZ')}
                                </span>
                            </div>
                            <div className="match-teams">
                                <div className="team">Domácí Tým: <strong>{match.homeTeamId.substring(0, 8)}</strong></div>
                                <div className="vs">VS</div>
                                <div className="team">Hostující Tým: <strong>{match.awayTeamId.substring(0, 8)}</strong></div>
                            </div>
                            <div className="match-footer">
                                <span>Místo: {match.location}</span>
                            </div>
                        </Link>
                    ))}
                </div>
            )}
        </div>
    );
}