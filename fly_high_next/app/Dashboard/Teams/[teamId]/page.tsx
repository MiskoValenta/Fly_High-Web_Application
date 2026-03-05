// app/Dashboard/Teams/[teamId]/page.tsx
"use client";

import React, { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import { fetchWithAuth } from '@/lib/apiClient';
import { TeamDetailDto } from '@/types/team';
import './TeamDetail.css';

export default function TeamDetailPage() {
    const { teamId } = useParams();
    const router = useRouter();
    const [team, setTeam] = useState<TeamDetailDto | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchTeam = async () => {
            try {
                const res = await fetchWithAuth(`/Team/${teamId}`);
                if (res.ok) {
                    const data = await res.json();
                    setTeam(data);
                } else {
                    router.push('/Dashboard/Teams');
                }
            } catch (error) {
                console.error("Failed to load team details");
            } finally {
                setLoading(false);
            }
        };
        fetchTeam();
    }, [teamId, router]);

    if (loading) return <div className="dashboard-container"><p>Načítám detail týmu...</p></div>;
    if (!team) return null;

    return (
        <div className="dashboard-container team-detail-container">
            <div className="team-detail-header glass-card">
                <div className="header-info">
                    <h1 className="dashboard-heading">{team.teamName}</h1>
                    <span className="badge-role">{team.myRole}</span>
                </div>
                <p className="team-short-name">Zkratka: {team.shortName}</p>
                {team.description && <p className="team-description">{team.description}</p>}
            </div>

            <div className="team-members-section glass-card">
                <h2>Členové Týmu</h2>
                <div className="members-list">
                    {team.members.length === 0 ? (
                        <p className="empty-text">V týmu zatím nejsou žádní členové.</p>
                    ) : (
                        team.members.map((member) => (
                            <div key={member.id} className="member-item">
                                <div className="member-info">
                                    <span className="member-email">{member.email}</span>
                                    <span className={`member-role role-${member.role.toLowerCase()}`}>{member.role}</span>
                                </div>
                                <span className={`member-status ${member.isActive ? 'active' : 'inactive'}`}>
                                    {member.isActive ? 'Aktivní' : 'Neaktivní'}
                                </span>
                            </div>
                        ))
                    )}
                </div>
            </div>

            <div className="team-actions">
                <button className="btn-secondary" onClick={() => router.push('/Dashboard/Teams')}>
                    Zpět na přehled
                </button>
                {team.myRole === 'Owner' && (
                    <button className="btn-primary" style={{ background: 'var(--destructive)' }}>
                        Smazat Tým
                    </button>
                )}
            </div>
        </div>
    );
}