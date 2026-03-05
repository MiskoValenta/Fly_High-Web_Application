// app/Dashboard/Matches/Create/page.tsx
"use client";

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { proposeMatch } from '@/lib/matchApi';
import './CreateMatch.css';

export default function CreateMatchPage() {
    const router = useRouter();
    const [formData, setFormData] = useState({
        homeTeamId: '',
        awayTeamId: '',
        scheduledAt: '',
        location: ''
    });
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsLoading(true);
        setError('');

        try {
            // Převedení lokálního data na ISO formát pro backend
            const isoDate = new Date(formData.scheduledAt).toISOString();
            await proposeMatch({
                ...formData,
                scheduledAt: isoDate
            });
            router.push('/Dashboard/Matches');
        } catch (err: any) {
            setError(err.message || 'Nepodařilo se navrhnout zápas.');
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="dashboard-container create-match-container">
            <h1 className="dashboard-heading">Navrhnout Zápas</h1>
            <p className="dashboard-subtext">Vyplňte údaje pro navrhnutí nového volejbalového zápasu.</p>

            <form onSubmit={handleSubmit} className="create-match-form glass-card">
                {error && <div className="error-message">{error}</div>}

                <div className="form-group">
                    <label htmlFor="homeTeamId">ID Domácího Týmu *</label>
                    <input
                        type="text"
                        id="homeTeamId"
                        name="homeTeamId"
                        value={formData.homeTeamId}
                        onChange={handleChange}
                        required
                        placeholder="Zadejte ID vašeho týmu"
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="awayTeamId">ID Hostujícího Týmu *</label>
                    <input
                        type="text"
                        id="awayTeamId"
                        name="awayTeamId"
                        value={formData.awayTeamId}
                        onChange={handleChange}
                        required
                        placeholder="Zadejte ID týmu protihráčů"
                    />
                </div>

                <div className="form-row">
                    <div className="form-group half-width">
                        <label htmlFor="scheduledAt">Datum a Čas *</label>
                        <input
                            type="datetime-local"
                            id="scheduledAt"
                            name="scheduledAt"
                            value={formData.scheduledAt}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group half-width">
                        <label htmlFor="location">Místo konání *</label>
                        <input
                            type="text"
                            id="location"
                            name="location"
                            value={formData.location}
                            onChange={handleChange}
                            required
                            placeholder="Hala, Město..."
                        />
                    </div>
                </div>

                <div className="form-actions">
                    <button type="button" className="btn-secondary" onClick={() => router.push('/Dashboard/Matches')}>
                        Zrušit
                    </button>
                    <button type="submit" className="btn-primary" disabled={isLoading}>
                        {isLoading ? 'Odesílám...' : 'Navrhnout Zápas'}
                    </button>
                </div>
            </form>
        </div>
    );
}