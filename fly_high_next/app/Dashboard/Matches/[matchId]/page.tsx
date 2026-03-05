// app/Dashboard/Matches/[matchId]/page.tsx
"use client";

import React, { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import { getMatchById, acceptMatch, startMatch } from '@/lib/matchApi';
import { Match, MatchStatus } from '@/types/match';
import './MatchDetail.css';

export default function MatchDetailPage() {
    const { matchId } = useParams();
    const router = useRouter();
    const [match, setMatch] = useState<Match | null>(null);
    const [loading, setLoading] = useState(true);
    const [actionLoading, setActionLoading] = useState(false);

    const loadMatch = async () => {
        try {
            const data = await getMatchById(matchId as string);
            setMatch(data);
        } catch (error) {
            console.error("Failed to load match", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadMatch();
    }, [matchId]);

    const handleAccept = async () => {
        setActionLoading(true);
        try {
            await acceptMatch(matchId as string);
            await loadMatch(); // Znovu načíst data
        } catch (e) {
            console.error(e);
        } finally {
            setActionLoading(false);
        }
    };

    const handleStart = async () => {
        setActionLoading(true);
        try {
            await startMatch(matchId as string);
            await loadMatch();
        } catch (e) {
            console.error(e);
        } finally {
            setActionLoading(false);
        }
    };

    if (loading) return <div className="dashboard-container"><p>Načítám detail zápasu...</p></div>;
    if (!match) return <div className="dashboard-container"><p>Zápas nebyl nalezen.</p></div>;

    const isProposed = match.status === MatchStatus.Proposed;
    const isScheduled = match.status === MatchStatus.Scheduled || match.status === MatchStatus.Accepted;

    return (
        <div className="dashboard-container match-detail-container">
            <div className="match-detail-header glass-card">
                <div className="header-top">
                    <h1 className="dashboard-heading">Detail Zápasu</h1>
                    <span className={`match-status-badge status-${match.status}`}>
                        Stav: {MatchStatus[match.status]}
                    </span>
                </div>

                <div className="scoreboard">
                    <div className="score-team">
                        <h3>Domácí</h3>
                        <p>{match.homeTeamId.substring(0, 8)}</p>
                    </div>
                    <div className="score-center">
                        <div className="vs">VS</div>
                        <p className="match-location">{match.location}</p>
                        <p className="match-date">{new Date(match.scheduledAt).toLocaleString('cs-CZ')}</p>
                    </div>
                    <div className="score-team">
                        <h3>Hosté</h3>
                        <p>{match.awayTeamId.substring(0, 8)}</p>
                    </div>
                </div>

                <div className="match-actions-bar">
                    <button className="btn-secondary" onClick={() => router.push('/Dashboard/Matches')}>
                        Zpět na Zápasy
                    </button>
                    <div className="action-group">
                        {isProposed && (
                            <button className="btn-primary" onClick={handleAccept} disabled={actionLoading}>
                                {actionLoading ? 'Zpracovávám...' : 'Přijmout Zápas'}
                            </button>
                        )}
                        {isScheduled && (
                            <button className="btn-primary" onClick={handleStart} disabled={actionLoading}>
                                {actionLoading ? 'Spouštím...' : 'Zahájit Zápas'}
                            </button>
                        )}
                    </div>
                </div>
            </div>

            {/* Zde by se daly iterovat Sety (match.sets) pokud se zápas hraje */}
            {match.sets && match.sets.length > 0 && (
                <div className="match-sets glass-card">
                    <h2>Odehrané Sety</h2>
                    <div className="sets-list">
                        {match.sets.map(set => (
                            <div key={set.id} className="set-row">
                                <span>Set {set.setNumber}</span>
                                <strong>{set.homeScore} : {set.awayScore}</strong>
                            </div>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
}